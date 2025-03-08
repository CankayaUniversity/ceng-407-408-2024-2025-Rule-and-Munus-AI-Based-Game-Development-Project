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
    private Node root;
    private float distanceToPlayer;
    private bool isPlayerTurn = true;
    private bool isEnemyTurn = false;
    private EnemyStats enemyStats;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>(); // D�zeltme: GetComponent ile ba�land�
        if (enemyStats == null)
        {
            Debug.LogError("EnemyStats bile�eni eksik!");
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
        Debug.Log("Oyuncu s�ras�.");
        yield return new WaitForSeconds(1f);
        SwitchToEnemyTurn();
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("D��man s�ras�.");

        if (playerTransform != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        }

        if (distanceToPlayer > 1)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= 1)
        {
            root.Evaluate();
        }

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
            new Sequence(new List<Node> {
                new ConditionNode(() => distanceToPlayer <= 1),
                new ActionNode(() => Attack())
            }),
            new Sequence(new List<Node> {
                new ConditionNode(() => distanceToPlayer > 1 && playerClass == "Archer"),
                new Selector(new List<Node> {
                    new ProbabilityNode(0.7f, new ActionNode(() => MoveTowardsPlayer())),
                    new ProbabilityNode(0.3f, new ActionNode(() => UseRangedAttack()))
                })
            }),
            new Sequence(new List<Node> {
                new ConditionNode(() => distanceToPlayer > 1),
                new ActionNode(() => MoveTowardsPlayer())
            })
        });

        return combatStrategy;
    }

    private void Attack()
    {
        string[] attackAreas = { "Kafa", "G���s", "Bacak" };
        string attackChoice = attackAreas[UnityEngine.Random.Range(0, attackAreas.Length)];

        if (enemyStats != null)
        {
            int attackPower = enemyStats.GetAttackPower();
            Debug.Log($"D��man {attackChoice} b�lgesine {attackPower} g�c�nde sald�r�yor!");
        }
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 1f);
        Debug.Log("D��man oyuncuya yakla��yor!");
    }

    private void UseRangedAttack()
    {
        if (enemyStats == null) return;

        if (enemyClass == "Mage")
        {
            int spellPower = enemyStats.GetSpellPower();
            Debug.Log($"D��man b�y� yap�yor! G�c�: {spellPower}");
        }
        else if (enemyClass == "Archer")
        {
            int arrowPower = enemyStats.GetArrowPower();
            Debug.Log($"D��man ok at�yor! G�c�: {arrowPower}");
        }
    }
}
