using System;
using UnityEngine;
using BehaviorTree;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public string playerClass;
    public string enemyClass;
    public int enemyDEX;
    public int arrowCount=10;  // Ok sayısı
    private Node root;
    private float distanceToPlayer;
    private bool isPlayerTurn = true;
    private bool isEnemyTurn = false;
    private EnemyStats enemyStats;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        if (enemyStats == null)
        {
            Debug.LogError("EnemyStats bileşeni eksik!");
            return;
        }

        root = CreateBehaviorTree();
        StartCoroutine(GameTurn());
    }

    IEnumerator GameTurn()
    {
        while (true)
        {
            if (isPlayerTurn)
            {
                yield return StartCoroutine(PlayerTurn());
            }
            else if (isEnemyTurn)
            {
                yield return StartCoroutine(EnemyTurn());
            }
            yield return null;
        }
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Oyuncu sırası.");
        yield return new WaitForSeconds(1f);
        SwitchToEnemyTurn();
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Düşman sırası.");

        if (playerTransform != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        }

        root.Evaluate();

        yield return new WaitForSeconds(1f);
        SwitchToPlayerTurn();
    }

    private void SwitchToPlayerTurn()
    {
        isPlayerTurn = true;
        isEnemyTurn = false;
    }

    private void SwitchToEnemyTurn()
    {
        isPlayerTurn = false;
        isEnemyTurn = true;
    }

    private Node CreateBehaviorTree()
    {
        Node combatStrategy = new Selector(new List<Node>
        {
            // Yakın dövüşçüler mesafe kapatana kadar saldırmaz
            new Sequence(new List<Node> {
                new ConditionNode(() => enemyClass == "Warrior" && distanceToPlayer > 1),
                new ActionNode(() => MoveTowardsPlayer())
            }),

            // Uzak dövüşçülerin (Archer/Mage) Ranged Attack kararı
            new Sequence(new List<Node> {
                new ConditionNode(() => enemyClass == "Archer" || enemyClass == "Mage"),
                new ConditionNode(() => ShouldUseRangedAttack()), 
                new ActionNode(() => UseRangedAttack())
            }),

            // Yakın mesafedeyse saldır
            new Sequence(new List<Node> {
                new ConditionNode(() => distanceToPlayer <= 1),
                new ActionNode(() => Attack())
            }),

            // Mesafe kapat
            new Sequence(new List<Node> {
                new ConditionNode(() => distanceToPlayer > 1),
                new ActionNode(() => MoveTowardsPlayer())
            })
        });

        return combatStrategy;
    }

    private void Attack()
    {
        string[] attackAreas = { "Kafa", "Göğüs", "Bacak" };
        string attackChoice = attackAreas[UnityEngine.Random.Range(0, attackAreas.Length)];

        if (enemyStats != null)
        {
            int attackPower = enemyStats.GetAttackPower();
            Debug.Log($"Düşman {attackChoice} bölgesine {attackPower} gücünde saldırıyor!");
        }
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 1f);
        Debug.Log("Düşman oyuncuya yaklaşıyor!");
    }

    private void UseRangedAttack()
    {
        if (enemyStats == null) return;
        if (arrowCount <= 0) return; // Ok bitmişse saldırma

        float hitChance = CalculateHitChance();

        if (UnityEngine.Random.value <= hitChance)
        {
            if (enemyClass == "Mage")
            {
                int spellPower = enemyStats.GetSpellPower();
                Debug.Log($"Düşman büyü yapıyor! Gücü: {spellPower}");
            }
            else if (enemyClass == "Archer")
            {
                int arrowPower = enemyStats.GetArrowPower();
                Debug.Log($"Düşman ok atıyor! Gücü: {arrowPower}");
                arrowCount--; // Ok azalıyor
            }
        }
        else
        {
            Debug.Log("Düşmanın saldırısı başarısız oldu!");
        }
    }

    private bool ShouldUseRangedAttack()
    {
        if (enemyClass == "Archer" && arrowCount > 0)
        {
            Debug.Log("heyeeyyy ok saldırısı yaptı");
            return enemyDEX >= 10 || UnityEngine.Random.value <= 0.7f;
        }
        else if (enemyClass == "Mage")
        {
            return true;
        }
        return false;
    }

    private float CalculateHitChance()
    {
        float baseChance = (enemyClass == "Archer") ? 0.4f : 0.2f;
        float distanceFactor = Mathf.Clamp(distanceToPlayer * 0.1f, 0.1f, 1f);
        float dexFactor = (enemyDEX / 10f);

        return Mathf.Clamp(baseChance + dexFactor - distanceFactor, 0.1f, 1f);
    }
}
