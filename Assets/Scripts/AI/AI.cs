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
    public int mana = 10;
    private Node root;
    private float distanceToPlayer;
    private EnemyStats enemyStats;
    private float arenaRadius = 10f;


    private string[] bodyParts = { "kafa", "göğüs", "ayak" };
    private string playerDefenseZone;
    private string enemyDefenseZone;




    // Playerın gelişim varsayımlarıdır********
    public int kafaXP = 50;
    public int gogusXP = 30;
    public int ayakXP = 20;



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
            PlayerSelectDefense();
            EnemySelectDefense();

            PlayerAttack();
            root.Evaluate();
            yield return new WaitForSeconds(5f);
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
            new ConditionNode(() => distanceToPlayer <= 1),
            new Selector(new List<Node> {
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.7f), 
                    new ActionNode(() => Attack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.15f && mana > 0), 
                    new ActionNode(() => CastSpell())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.10f && arrowCount > 0), 
                    new ActionNode(() => UseRangedAttack())  
                }),
                
                new ActionNode(() => MoveAwayFromPlayer())
                
            })
        }),

        
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 1f ),  
            new ConditionNode(() => UnityEngine.Random.value <= 0.6f),  
            new ActionNode(() => MoveTowardsPlayer()) 
        }),

       
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 1f && arrowCount > 0 && UnityEngine.Random.value <= 0.3f), 
            new ActionNode(() => UseRangedAttack())  
        }),

        
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.5f && mana > 0 ),
            new ActionNode(() => CastSpell())  
        }),

       
         
         new ActionNode(() => MoveAwayFromPlayer()) 
        
    });
    }
    private Node CreateArcherTree()
    {
        return new Selector(new List<Node>
    {   //*YAKIN MESAFE İŞLEMLERİ
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer <= 1),
            new Selector(new List<Node> {

                //*OK MEVCUT
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount > 0),
                    new ActionNode(() =>  MoveAwayFromPlayer())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount > 0),
                    new ActionNode(() => UseRangedAttack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount > 0),
                    new ActionNode(() => Attack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.1f && arrowCount > 0 && mana > 0),
                    new ActionNode(() => CastSpell())
                }),
                //OK MEVCUT*

                //*OK MEVCUT DEĞİL,MANA VAR
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.4f && arrowCount == 0 && mana > 0),
                    new ActionNode(() => Attack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount == 0 && mana > 0),
                    new ActionNode(() =>  MoveAwayFromPlayer())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount == 0 && mana > 0),
                    new ActionNode(() => CastSpell())
                }),

                //OK MEVCUT DEĞİL,MANA VAR*


                //*OK MEVCUT DEĞİL,MANA MEVCUT DEĞİL
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.6f && arrowCount == 0 && mana == 0),
                    new ActionNode(() => Attack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.4f && arrowCount == 0 && mana == 0),
                    new ActionNode(() => MoveAwayFromPlayer())
                })
                //OK MEVCUT DEĞİL,MANA MEVCUT DEĞİL*
            })
        }),
        //YAKIN MESAFE İŞLEMLERİ*





        //*UZAK MESAFE İŞLEMLERİ


        //*OK VAR,MANA VAR
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.4f && arrowCount > 0 && mana > 0),
            new ActionNode(() => UseRangedAttack())  
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount > 0 && mana > 0),
            new ActionNode(() => CastSpell())
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount > 0 && mana > 0),
            new ActionNode(() => MoveAwayFromPlayer())
        }),
       //OK VAR,MANA VAR*



        //*OK VAR,MANA YOK
       new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount > 0 && mana == 0),
            new ActionNode(() => MoveAwayFromPlayer())
        }),
       //OK VAR,MANA YOK*





        //*OK MEVCUT DEĞİL,MANA VAR
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount == 0 && mana > 0),
            new ActionNode(() => CastSpell())  
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount == 0 && mana > 0),
            new ActionNode(() => MoveAwayFromPlayer())
        }),
        //OK MEVCUT DEĞİL,MANA VAR*



        //*OK YOK,MANA YOK
        
        
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.8f ),
            new ActionNode(() => MoveTowardsPlayer()) 
        }),
       
        new Sequence(new List<Node> {
            
            new ActionNode(() => MoveAwayFromPlayer()) 
        })


          //*OK YOK,MANA YOK


        //UZAK MESAFE İŞLEMLERİ*
    });
    }

    private Node CreateMageTree()
    {
        return new Selector(new List<Node>
    {   
        //*YAKIN MESAFE İŞLEMLERİ
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer <= 1),
            new Selector(new List<Node> {

                //*MANA MEVCUT
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana > 0),
                    new ActionNode(() => MoveAwayFromPlayer())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana > 0),
                    new ActionNode(() => CastSpell())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana > 0),
                    new ActionNode(() => Attack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.1f && mana > 0 && arrowCount > 0),
                    new ActionNode(() => UseRangedAttack())
                }),
                //MANA MEVCUT*

                //*MANA MEVCUT DEĞİL, OK VAR
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.4f && mana == 0 && arrowCount > 0),
                    new ActionNode(() => Attack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana == 0 && arrowCount > 0),
                    new ActionNode(() => MoveAwayFromPlayer())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana == 0 && arrowCount > 0),
                    new ActionNode(() => UseRangedAttack())
                }),
                //MANA MEVCUT DEĞİL, OK VAR*

                //*MANA MEVCUT DEĞİL, OK MEVCUT DEĞİL
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.6f && mana == 0 && arrowCount == 0),
                    new ActionNode(() => Attack())
                }),
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.4f && mana == 0 && arrowCount == 0),
                    new ActionNode(() => MoveAwayFromPlayer())
                })
                //MANA MEVCUT DEĞİL, OK MEVCUT DEĞİL*
            })
        }),
        //YAKIN MESAFE İŞLEMLERİ*


        //*UZAK MESAFE İŞLEMLERİ

        //*MANA VAR, OK VAR
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.4f && mana > 0 && arrowCount > 0),
            new ActionNode(() => CastSpell())
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && mana > 0 && arrowCount > 0),
            new ActionNode(() => UseRangedAttack())
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && mana > 0 && arrowCount > 0),
            new ActionNode(() => MoveAwayFromPlayer())
        }),
        //MANA VAR, OK VAR*


        //*MANA VAR, OK YOK
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && mana > 0 && arrowCount == 0),
            new ActionNode(() => MoveAwayFromPlayer())
        }),
        //MANA VAR, OK YOK*


        //*MANA MEVCUT DEĞİL, OK VAR
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana == 0 && arrowCount > 0),
            new ActionNode(() => UseRangedAttack())
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && mana == 0 && arrowCount > 0),
            new ActionNode(() => MoveAwayFromPlayer())
        }),
        //MANA MEVCUT DEĞİL, OK VAR*


        //*MANA YOK, OK YOK
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.8f ),
            new ActionNode(() => MoveTowardsPlayer())
        }),
        new Sequence(new List<Node> {
            new ActionNode(() => MoveAwayFromPlayer())
        })
        //*MANA YOK, OK YOK*

        //UZAK MESAFE İŞLEMLERİ*
    });
    }


    private void EnemySelectDefense()
    {
        string[] bodyParts = { "kafa", "göğüs", "ayak" };
        enemyDefenseZone = bodyParts[UnityEngine.Random.Range(0, bodyParts.Length)];
        Debug.Log($"Düşman {enemyDefenseZone} bölgesini savunuyor!");
    }


    private void PlayerSelectDefense()
    {
        string[] bodyParts = { "kafa", "göğüs", "ayak" };
        playerDefenseZone = bodyParts[UnityEngine.Random.Range(0, bodyParts.Length)];
        Debug.Log($"Oyuncu {playerDefenseZone} bölgesini savunuyor!");
    }


    private void PlayerAttack()
    {
        string[] bodyParts = { "kafa", "göğüs", "ayak" };
        string target = bodyParts[UnityEngine.Random.Range(0, bodyParts.Length)];

        if (target == enemyDefenseZone)
        {
            Debug.Log($"Oyuncu {target} bölgesine saldırdı ama düşman savundu!");
        }
        else
        {
            Debug.Log($"Oyuncu {target} bölgesine saldırdı ve isabet ettirdi!");
        }
    }

    



    //            HAREKET İŞLEMLERİ
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
    //            HAREKET İŞLEMLERİ





    //             * SALDIRI KISMI
    private void UseRangedAttack()
    {
        if (arrowCount > 0)
        {
            string target = ChooseTarget();

            if (target == playerDefenseZone)
            {
                Debug.Log($"Düşman {target} bölgesine ok attı ancak oyuncu savundu!");
                arrowCount--;
            }
            else
            {
                Debug.Log($"Düşman {target} bölgesine ok attı ve vurdu!");
                arrowCount--;
            }
        }
    }


    private void CastSpell()
    {
        string target = ChooseTarget();

        if (target == playerDefenseZone)
        {
            Debug.Log($"Düşman {target} bölgesine büyü yaptı ancak oyuncu savundu!");
            mana--;
        }
        else
        {
            Debug.Log($"Düşman {target} bölgesine büyü yaptı ve vurdu!");
            mana--;
        }
    }


    private void Attack()
    {
        string target = ChooseTarget();

        if (target == playerDefenseZone)
        {
            Debug.Log($"Düşman {target} bölgesine KILIÇ İLE saldırdı ancak oyuncu savundu!");
        }
        else
        {
            Debug.Log($"Düşman {target} bölgesine KILIÇ İLE saldırdı ve vurdu!");
        }
    }
    //              SALDIRI KISMI*




    // Playerın varsayılan ilerlemesine karşılık AI ın seçtiği bölgeler
    private string ChooseTarget()
    {
        
        int totalXP = kafaXP + gogusXP + ayakXP;

        
        if (totalXP == 0)
        {
            return bodyParts[UnityEngine.Random.Range(0, bodyParts.Length)];
        }

        
        double kafaChance = (double)kafaXP / totalXP;
        double gogusChance = (double)gogusXP / totalXP;
        double ayakChance = (double)ayakXP / totalXP;

        
        double randomValue = UnityEngine.Random.Range(0f, 1f);

        if (randomValue < kafaChance) return "kafa";
        else if (randomValue < kafaChance + gogusChance) return "göğüs";
        else return "ayak";
    }
}
