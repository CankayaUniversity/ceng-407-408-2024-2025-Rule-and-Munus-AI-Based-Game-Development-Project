using System;
using UnityEngine;
using BehaviorTree;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public string enemyClass;
    public int enemyDEX;
    public int arrowCount = 10;
    private Node root;
    private float distanceToPlayer;
    private EnemyStats enemyStats;
    private float arenaRadius = 10f; // Arena sınırları

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        if (enemyStats == null)
        {
            Debug.LogError("EnemyStats bileşeni eksik!");
            return;
        }

        root = CreateBehaviorTree();
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        while (true)
        {
            if (playerTransform != null)
            {
                distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            }
            root.Evaluate();
            yield return new WaitForSeconds(1f);
        }
    }

    private Node CreateBehaviorTree()
    {
        switch (enemyClass)
        {
            case "Warrior":
                return CreateWarriorTree();
            case "Archer":
                return CreateArcherTree();
            case "Mage":
                return CreateMageTree();
            default:
                Debug.LogError("Geçersiz düşman sınıfı!");
                return null;
        }
    }

    private Node CreateWarriorTree()
    {
        return new Selector(new List<Node>
    {
        // Kılıç Saldırısı - Oyuncuya çok yakınsa
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer <= 1),  // Oyuncuya çok yakın mesafe
            new ActionNode(() => Attack())  // Kılıç saldırısı
        }),

        // Oyuncuya yaklaşma - Bu hareketin olasılığı olacak (%50 olasılıkla)
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 1f ),  // Yaklaşma için mesafe kontrolü
            new ConditionNode(() => UnityEngine.Random.value <= 0.5f),  // %50 olasılıkla yaklaş
            new ActionNode(() => MoveTowardsPlayer()) // Oyuncuya yaklaş
        }),

        // Eğer ok veya büyü varsa, ok atabilir veya büyü yapabilir
        new Sequence(new List<Node> {
            new ConditionNode(() => arrowCount > 0 || UnityEngine.Random.value <= 0.2f), // Ok veya büyü varsa
            new ActionNode(() => UseRangedAttack())  // Ok saldırısı
        }),

        // Büyü Saldırısı - %20 olasılıkla
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f),
            new ActionNode(() => CastSpell())  // Büyü saldırısı
        }),

        // Uzaklaşma - Eğer oyuncu çok uzaksa ve ok/büyü yoksa
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 5f && arrowCount <= 0),
            new ActionNode(() => MoveAwayFromPlayer()) // Uzaklaşmak
        })
    });
    }
    private Node CreateArcherTree()
    {
        return new Selector(new List<Node>
    {
        // Ok Saldırısı (Ok ve büyü olduğu sürece)
        new Sequence(new List<Node> {
            new ConditionNode(() => arrowCount > 0),
            new ActionNode(() => UseRangedAttack())  // Ok saldırısı
        }),
        // Büyü Saldırısı (Büyü var ise)
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.5f),
            new ActionNode(() => CastSpell())  // Büyü saldırısı
        }),
        // Kılıç Saldırısı (Ok ve büyü yoksa, oyuncuya yakınsa)
        new Sequence(new List<Node> {
            new ConditionNode(() => (arrowCount <= 0 && UnityEngine.Random.value <= 0.5f)), // Ok ve büyü yoksa
            new ConditionNode(() => distanceToPlayer <= 1),  // Yakın mesafe
            new ActionNode(() => Attack())  // Kılıç saldırısı
        }),
        // Uzaklaşma - Ok ve büyü yoksa ve oyuncu uzaksa
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 5f && arrowCount <= 0),
            new ActionNode(() => MoveAwayFromPlayer()) // Uzaklaşmak
        }),
        // Oyuncuya yaklaşma - Kılıç saldırısı için
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 1f && distanceToPlayer <= 5f),
            new ActionNode(() => MoveTowardsPlayer()) // Yaklaşmak
        })
    });
    }

    private Node CreateMageTree()
    {
        return new Selector(new List<Node>
    {
        // Ok Saldırısı (Ok varsa)
        new Sequence(new List<Node> {
            new ConditionNode(() => arrowCount > 0),
            new ActionNode(() => UseRangedAttack())  // Ok saldırısı
        }),
        // Büyü Saldırısı (Büyü var ise)
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.6f),
            new ActionNode(() => CastSpell())  // Büyü saldırısı
        }),
        // Kılıç Saldırısı (Ok ve büyü yoksa, oyuncuya yakınsa)
        new Sequence(new List<Node> {
            new ConditionNode(() => (arrowCount <= 0 && UnityEngine.Random.value <= 0.5f)), // Ok ve büyü yoksa
            new ConditionNode(() => distanceToPlayer <= 1),  // Yakın mesafe
            new ActionNode(() => Attack())  // Kılıç saldırısı
        }),
        // Uzaklaşma - Ok ve büyü yoksa ve oyuncu uzaksa
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 5f && arrowCount <= 0),
            new ActionNode(() => MoveAwayFromPlayer()) // Uzaklaşmak
        }),
        // Oyuncuya yaklaşma - Kılıç saldırısı için
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 1f && distanceToPlayer <= 5f),
            new ActionNode(() => MoveTowardsPlayer()) // Yaklaşmak
        })
    });
    }
    private void Attack()
    {
        Debug.Log("Düşman yakın dövüş saldırısı yapıyor!");
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 1f);
        Debug.Log("Düşman oyuncuya yaklaşıyor!");
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 direction = (transform.position - playerTransform.position).normalized;
        Vector3 newPosition = transform.position + direction * 1f;

        // Eğer yeni pozisyon arena dışına çıkıyorsa, maksimum sınırda kal
        if (newPosition.magnitude > arenaRadius)
        {
            newPosition = newPosition.normalized * arenaRadius;
        }
        transform.position = newPosition;

        Debug.Log("Düşman oyuncudan uzaklaşıyor!");
    }

    private void UseRangedAttack()
    {
        if (arrowCount > 0)
        {
            Debug.Log("Düşman ok atıyor!laaaan");
            arrowCount--;
        }
    }

    private void CastSpell()
    {
        Debug.Log("Düşman büyü yapıyor!");
    }
}
