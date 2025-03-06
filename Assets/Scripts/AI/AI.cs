using System;
using UnityEngine;
using BehaviorTree;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform; // Oyuncunun konumunu almak i�in
    public string playerClass;
    public string enemyClass;
    private Node root;
    [SerializeField] private float distanceToPlayer;
    private bool isPlayerTurn = true; // Oyuncunun s�ras�
    private bool isEnemyTurn = false; // D��man�n s�ras�

    void Start()
    {
        root = CreateBehaviorTree();
        StartCoroutine(GameTurn());
    }

    IEnumerator GameTurn()
    {
        while (true) // S�rekli oyun s�ras�n� kontrol eder
        {
            if (isPlayerTurn)
            {
                yield return StartCoroutine(PlayerTurn());
            }
            else if (isEnemyTurn)
            {
                yield return StartCoroutine(EnemyTurn());
            }

            yield return null; // Bir sonraki frame'e ge�meden �nce bekler
        }
    }

    // Oyuncunun s�ras�
    private IEnumerator PlayerTurn()
    {
        // Buraya oyuncunun yapaca�� aksiyonlar� ekleyebilirsiniz
        // �rne�in oyuncu hareket edebilir veya sald�rabilir
        Debug.Log("Oyuncu s�ras�.");

        // Oyuncu s�ras� bitti�inde d��mana ge�iyoruz
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        SwitchToEnemyTurn();
    }

    // D��man�n s�ras�
    private IEnumerator EnemyTurn()
    {
        Debug.Log("D��man s�ra.");

        if (playerTransform != null)
        {
            distanceToPlayer = Mathf.RoundToInt(Vector3.Distance(transform.position, playerTransform.position));
        }

        // E�er mesafe 1'den b�y�kse, d��man 1 ad�m hareket eder
        if (distanceToPlayer > 1)
        {
            MoveTowardsPlayer();
        }
        // Mesafe 1 oldu�unda, d��man sald�r�r
        else if (distanceToPlayer == 1)
        {
            root.Evaluate(); // D��man yak�nsa sald�r� yapar
        }

        // D��man s�ras� bitti�inde tekrar oyuncuya ge�iyoruz
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        SwitchToPlayerTurn();
    }

    // S�ralar� de�i�tiren yard�mc� fonksiyonlar
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
        string[] attackAreas = { "Kafa", "G���s", "Bacak" };
        string attackChoice = attackAreas[UnityEngine.Random.Range(0, attackAreas.Length)];
        Debug.Log($"D��man {attackChoice} b�lgesine sald�r�yor!");
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 1f);
        Debug.Log("D��man oyuncuya yakla��yor!");
    }

    private void UseRangedAttack()
    {
        if (enemyClass == "Mage")
        {
            Debug.Log("D��man b�y� yap�yor!");
        }
        else if (enemyClass == "Archer")
        {
            Debug.Log("D��man ok at�yor!");
        }
    }
}
