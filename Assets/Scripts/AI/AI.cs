using System;
using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform; // Oyuncunun konumunu almak için
    public string playerClass;
    public string enemyClass;
    private Node root;
    [SerializeField] private float distanceToPlayer;
    private bool isPlayerTurn = true; // Oyuncunun sýrasý
    private bool isEnemyTurn = false; // Düþmanýn sýrasý

    void Start()
    {
        root = CreateBehaviorTree();
        StartCoroutine(GameTurn());
    }

    IEnumerator GameTurn()
    {
        while (true) // Sürekli oyun sýrasýný kontrol eder
        {
            if (isPlayerTurn)
            {
                yield return StartCoroutine(PlayerTurn());
            }
            else if (isEnemyTurn)
            {
                yield return StartCoroutine(EnemyTurn());
            }

            yield return null; // Bir sonraki frame'e geçmeden önce bekler
        }
    }

    // Oyuncunun sýrasý
    private IEnumerator PlayerTurn()
    {
        // Buraya oyuncunun yapacaðý aksiyonlarý ekleyebilirsiniz
        // Örneðin oyuncu hareket edebilir veya saldýrabilir
        Debug.Log("Oyuncu sýrasý.");

        // Oyuncu sýrasý bittiðinde düþmana geçiyoruz
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        SwitchToEnemyTurn();
    }

    // Düþmanýn sýrasý
    private IEnumerator EnemyTurn()
    {
        Debug.Log("Düþman sýra.");

        if (playerTransform != null)
        {
            distanceToPlayer = Mathf.RoundToInt(Vector3.Distance(transform.position, playerTransform.position));
        }

        // Eðer mesafe 1'den büyükse, düþman 1 adým hareket eder
        if (distanceToPlayer > 1)
        {
            MoveTowardsPlayer();
        }
        // Mesafe 1 olduðunda, düþman saldýrýr
        else if (distanceToPlayer == 1)
        {
            root.Evaluate(); // Düþman yakýnsa saldýrý yapar
        }

        // Düþman sýrasý bittiðinde tekrar oyuncuya geçiyoruz
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        SwitchToPlayerTurn();
    }

    // Sýralarý deðiþtiren yardýmcý fonksiyonlar
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
                new ConditionNode(() => distanceToPlayer == 1),
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
        string[] attackAreas = { "Kafa", "Göðüs", "Bacak" };
        string attackChoice = attackAreas[UnityEngine.Random.Range(0, attackAreas.Length)];
        Debug.Log($"Düþman {attackChoice} bölgesine saldýrýyor!");
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 1f);
        Debug.Log("Düþman oyuncuya yaklaþýyor!");
    }

    private void UseRangedAttack()
    {
        if (enemyClass == "Mage")
        {
            Debug.Log("Düþman büyü yapýyor!");
        }
        else if (enemyClass == "Archer")
        {
            Debug.Log("Düþman ok atýyor!");
        }
    }
}
