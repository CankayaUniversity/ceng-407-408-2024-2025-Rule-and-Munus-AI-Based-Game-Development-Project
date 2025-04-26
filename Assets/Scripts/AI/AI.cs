using System;
using UnityEngine;
using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Types;

public class EnemyAI : MonoBehaviour
{

    public Transform playerTransform;
    public string enemyClass;
    public int enemyDEX;
    public int arrowCount = 10;
    public int mana = 10;
    private Node root;
    public float distanceToPlayer;
    
    private float arenaRadius = 10f;
    

    private string[] bodyParts = { "head", "body", "leg" };
    private string playerDefenseZone;
    private string enemyDefenseZone;

    CharacterMovingButtons characterMovingButtons;

    private Equipment headEquipment;
    private Equipment bodyEquipment;
    private Equipment legEquipment;
    // Equipment headEquipment = new Equipment(EquipmentSlot.Head, Rarity.Common, 5, 3, null, null);
    // Equipment bodyEquipment = new Equipment(EquipmentSlot.Body, Rarity.Common, 5, 3, null, null);
    // Equipment legEquipment = new Equipment(EquipmentSlot.Legs, Rarity.Common, 5, 3, null, null);
    CharacterHealthController characterHealthController;


    private IAnimatorController animatorController;

    int score = 0;

    void Start()
    {
        animatorController = new AnimatorController(GetComponent<Animator>());
        if (characterHealthController == null)
        {
            characterHealthController = FindObjectOfType<CharacterHealthController>();

            if (characterHealthController == null)
            {
                Debug.LogError("Sahnede CharacterHealthController bulunamadı!");
            }
        }
        headEquipment = ScriptableObject.CreateInstance<Equipment>();
        headEquipment.equipSlot = EquipmentSlot.Head;
        headEquipment.rarirty = Rarity.Common;
        // İstersen burada başka değerler de atarsın, mesela attackPower, defensePower gibi.

        bodyEquipment = ScriptableObject.CreateInstance<Equipment>();
        bodyEquipment.equipSlot = EquipmentSlot.Body;
        bodyEquipment.rarirty = Rarity.Common;

        legEquipment = ScriptableObject.CreateInstance<Equipment>();
        legEquipment.equipSlot = EquipmentSlot.Legs;
        legEquipment.rarirty = Rarity.Common;

       

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
            new ConditionNode(() => distanceToPlayer <= 10),
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
                
                new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
                
            })
        }),

        
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 10f ),  
            new ConditionNode(() => UnityEngine.Random.value <= 0.6f),  
            new ActionNode(() => StartCoroutine(MoveTowardsPlayer())) 
        }),

       
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 10f && arrowCount > 0 && UnityEngine.Random.value <= 0.3f), 
            new ActionNode(() => UseRangedAttack())  
        }),

        
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.5f && mana > 0 ),
            new ActionNode(() => CastSpell())  
        }),

       
         
         new ActionNode(() => StartCoroutine(MoveAwayFromPlayer())) 
        
    });
    }
    private Node CreateArcherTree()
    {
        return new Selector(new List<Node>
    {   //*YAKIN MESAFE İŞLEMLERİ
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer <=10),
            new Selector(new List<Node> {

                //*OK MEVCUT
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount > 0),
                    new ActionNode(() =>  StartCoroutine(MoveAwayFromPlayer()))
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
                    new ActionNode(() =>  StartCoroutine(MoveAwayFromPlayer()))
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
                    new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
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
            new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
        }),
       //OK VAR,MANA VAR*



        //*OK VAR,MANA YOK
       new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount > 0 && mana == 0),
            new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
        }),
       //OK VAR,MANA YOK*





        //*OK MEVCUT DEĞİL,MANA VAR
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount == 0 && mana > 0),
            new ActionNode(() => CastSpell())  
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount == 0 && mana > 0),
            new ActionNode(() =>StartCoroutine(MoveAwayFromPlayer()))
        }),
        //OK MEVCUT DEĞİL,MANA VAR*



        //*OK YOK,MANA YOK
        
        
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.8f ),
            new ActionNode(() => StartCoroutine(MoveTowardsPlayer())) 
        }),
       
        new Sequence(new List<Node> {
            
            new ActionNode(() => StartCoroutine(MoveAwayFromPlayer())) 
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
            new ConditionNode(() => distanceToPlayer <= 10),
            new Selector(new List<Node> {

                //*MANA MEVCUT
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana > 0),
                    new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
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
                    new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
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
                    new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
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
            new ActionNode(() =>StartCoroutine(MoveAwayFromPlayer()))
        }),
        //MANA VAR, OK VAR*


        //*MANA VAR, OK YOK
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && mana > 0 && arrowCount == 0),
            new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
        }),
        //MANA VAR, OK YOK*


        //*MANA MEVCUT DEĞİL, OK VAR
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana == 0 && arrowCount > 0),
            new ActionNode(() => UseRangedAttack())
        }),
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && mana == 0 && arrowCount > 0),
            new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
        }),
        //MANA MEVCUT DEĞİL, OK VAR*


        //*MANA YOK, OK YOK
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.8f ),
            new ActionNode(() => StartCoroutine(MoveTowardsPlayer()))
        }),
        new Sequence(new List<Node> {
            new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
        })
        //*MANA YOK, OK YOK*

        //UZAK MESAFE İŞLEMLERİ*
    });
    }


    private void EnemySelectDefense()
    {
        string[] bodyParts = { "head", "body", "leg" };
        enemyDefenseZone = bodyParts[UnityEngine.Random.Range(0, bodyParts.Length)];
        Debug.Log($"Düşman {enemyDefenseZone} bölgesini savunuyor!");
    }


    private void PlayerSelectDefense()
    {
        // Eğer playerDefenseZone veya characterMovingButtons geçerli değilse (null veya başka bir kontrol) işlem yapma
        if (characterMovingButtons == null || characterMovingButtons.defenceIndex < 1 || characterMovingButtons.defenceIndex > 3)
        {
            Debug.LogError("Savunma bölgesi seçilmedi veya geçersiz savunma indeksi!");
            return;
        }

        if (characterMovingButtons.defenceIndex == 1)
        {
            playerDefenseZone = "head";
        }
        else if (characterMovingButtons.defenceIndex == 2)
        {
            playerDefenseZone = "body";
        }
        else if (characterMovingButtons.defenceIndex == 3)
        {
            playerDefenseZone = "leg";
        }
        else
        {
            playerDefenseZone = "unknown";
            Debug.LogError("Geçersiz savunma indeksi!");
            return;
        }

        Debug.Log($"Oyuncu {playerDefenseZone} bölgesini savunuyor!");
    }



    private void PlayerAttack()
    {
        string[] bodyParts = { "head", "body", "leg" };
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
    private IEnumerator MoveTowardsPlayer()
    {
        // Düşman oyuncuya yaklaşırken, koşma animasyonunu başlat
        animatorController.SetRunning(true);
        animatorController.SetIdle(false);

        
        Vector3 direction = (playerTransform.position - transform.position).normalized;  

        
        transform.position = transform.position + direction * 1f;

       
        

        
        yield return new WaitForSeconds(0.1f);

        
        animatorController.SetRunning(false);
        animatorController.SetIdle(true);

        Debug.Log("Düşman oyuncuya yaklaştı");
    }




    private IEnumerator MoveAwayFromPlayer()
    {
        // Düşman oyuncudan uzaklaşırken, geri hareket animasyonunu başlat
        animatorController.SetBackwarding(true);
        animatorController.SetIdle(false);

        // Düşman, oyuncudan uzaklaşırken
        Vector3 direction = (transform.position - playerTransform.position).normalized;
        Vector3 newPosition = transform.position + direction * 3f; // 3 birim uzaklaşmak

        
        if (newPosition.magnitude > arenaRadius)
        {
            newPosition = newPosition.normalized * arenaRadius; 
        }

        // Yeni pozisyona git
        transform.position = newPosition;

        // Kısa bir gecikme ekleyerek animasyonun düzgün görünmesini sağla
        yield return new WaitForSeconds(0.1f);

        // Animasyonu durdur ve idle animasyonuna geç
        animatorController.SetBackwarding(false);
        animatorController.SetIdle(true);

        Debug.Log("Düşman oyuncudan uzaklaşıyor ve idle animasyonuna geçiyor!");
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
                characterHealthController.currentHealth = characterHealthController.currentHealth - (score / 10);
                
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
            characterHealthController.currentHealth = characterHealthController.currentHealth - (score / 10);
            
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
            characterHealthController.currentHealth = characterHealthController.currentHealth - (score / 2);
        }
    }
    //              SALDIRI KISMI*



    public int CalculateEquipmentScore(Equipment equipment)
    {
        score = 0;

        // Slot'a göre temel puan
        Dictionary<EquipmentSlot, int> slotBaseScores = new Dictionary<EquipmentSlot, int>()
    {
        { EquipmentSlot.Head, 10 },
        { EquipmentSlot.Body, 15 },
        { EquipmentSlot.Legs, 20 },
        { EquipmentSlot.Weapon, 25 },
        { EquipmentSlot.Secondary, 20 },
        { EquipmentSlot.Feet, 10 },
        { EquipmentSlot.Accessoire, 15 },
        { EquipmentSlot.Default, 0 }
    };

        // Rarity'e göre çarpan
        Dictionary<Rarity, float> rarityMultipliers = new Dictionary<Rarity, float>()
    {
        { Rarity.Common, 1f },
        { Rarity.Uncommon, 1.2f },
        { Rarity.Advenced, 1.4f },
        { Rarity.Rare, 1.6f },
        { Rarity.Epic, 2f },
        { Rarity.Legendary, 3f },
        { Rarity.Default, 1f }
    };

        if (slotBaseScores.ContainsKey(equipment.equipSlot) && rarityMultipliers.ContainsKey(equipment.rarirty))
        {
            score = Mathf.RoundToInt(slotBaseScores[equipment.equipSlot] * rarityMultipliers[equipment.rarirty]);
        }

        return score;
    }


    private string ChooseTarget()
    {
        
        int headScore = CalculateEquipmentScore(headEquipment);
        
        int bodyScore = CalculateEquipmentScore(bodyEquipment);
        
        int legScore = CalculateEquipmentScore(legEquipment);

        int totalScore = headScore + bodyScore + legScore;

        
        if (totalScore == 0)
        {
            return bodyParts[UnityEngine.Random.Range(0, bodyParts.Length)];
        }

        float headChance = (float)headScore / totalScore;
        float bodyChance = (float)bodyScore / totalScore;
        float legChance = (float)legScore / totalScore;

        float randomValue = UnityEngine.Random.Range(0f, 1f);

        if (randomValue < headChance)
            return "head";
        else if (randomValue < headChance + bodyChance)
            return "body";
        else
            return "leg";
    }
}