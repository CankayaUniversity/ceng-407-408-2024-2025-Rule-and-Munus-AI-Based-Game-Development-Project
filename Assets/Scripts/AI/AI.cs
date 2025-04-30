using System;
using UnityEngine;
using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Types;

public class EnemyAI : MonoBehaviour
{
    private Player targetPlayer;
    private Attributes attributes;
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
    CharacterHealthController characterHealthController;


    private IAnimatorController animatorController;

    int score = 0;
    private EnemyStats enemyStats;
    void Start()
    {
        enemyStats = FindObjectOfType<EnemyStats>();
        targetPlayer = FindObjectOfType<Player>();
        attributes = targetPlayer.GetComponent<Attributes>();
        if (targetPlayer != null)
        {
            ChooseBestTargetArea();
        }
        animatorController = new AnimatorController(GetComponent<Animator>());
        if (characterHealthController == null)
        {
            characterHealthController = FindObjectOfType<CharacterHealthController>();

            if (characterHealthController == null)
            {
                Debug.LogError("CharacterHealthController not found in the scene!");
            }
        }
        
        // İstersen burada başka değerler de atarsın, mesela attackPower, defensePower gibi.

        

        
       

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
        Debug.Log($"Enemy is defending the {enemyDefenseZone} zone!");
        //enemyDefenseZone player tarafından çağrılıcak ve ona göre hasar alıp almaması karar verlicek 
    }


    private void PlayerSelectDefense()
    {
        // Eğer playerDefenseZone veya characterMovingButtons geçerli değilse (null veya başka bir kontrol) işlem yapma
        if (characterMovingButtons == null || characterMovingButtons.defenceIndex < 1 || characterMovingButtons.defenceIndex > 3)
        {
            Debug.LogError("Defense zone not selected or invalid defense index!");
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
            Debug.LogError("Invalid defense index!");
            return;
        }

        Debug.Log($"Player is defending the {playerDefenseZone} zone!");
    }



    private void PlayerAttack()
    {
        string[] bodyParts = { "head", "body", "leg" };
        string target = bodyParts[UnityEngine.Random.Range(0, bodyParts.Length)];

        if (target == enemyDefenseZone)
        {
            Debug.Log($"Player attacked the {target} zone but the enemy defended!");
        }
        else
        {
            Debug.Log($"Player attacked the {target} zone and hit!");
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

        Debug.Log("Enemy approached the player");
    }




    private IEnumerator MoveAwayFromPlayer()
    {
        
        animatorController.SetBackwarding(true);
        animatorController.SetIdle(false);

        
        Vector3 direction = (transform.position - playerTransform.position).normalized;
        Vector3 newPosition = transform.position + direction * 1f; 

        
        if (newPosition.magnitude > arenaRadius)
        {
            newPosition = newPosition.normalized * arenaRadius; 
        }

        
        transform.position = newPosition;

        // Kısa bir gecikme ekleyerek animasyonun düzgün görünmesini sağla
        yield return new WaitForSeconds(0.5f);

        // Animasyonu durdur ve idle animasyonuna geç
        animatorController.SetBackwarding(false);
        animatorController.SetIdle(true);

        Debug.Log("Enemy is moving away from the player and switching to idle animation!");
    }
    //            HAREKET İŞLEMLERİ





    //             * SALDIRI KISMI
    private void UseRangedAttack()
    {
        if (arrowCount > 0)
        {
            var (target, score) = ChooseBestTargetArea(); // Yeni method: hedef ve score döner
            Equipment targetedEquipment = GetTargetedEquipment(target); // Hedef zırhı alıyoruz
            Equipment weapon = enemyStats.GetEquippedWeapon(); // enemyStats üzerinden silahı alıyoruz

            if (targetedEquipment != null)
            {
                int finalDamage = CalculateFinalDamage(weapon, targetedEquipment);

                // Hedef savunma bölgesine vuruluyor mu?
                if (target == playerDefenseZone)
                {
                    Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone but the player defended!");
                }
                else
                {
                    Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone and hit!");
                    characterHealthController.currentHealth -= finalDamage; // Sonuç olarak hesaplanmış hasar
                }
            }
            else
            {
                Debug.Log("Target zone is empty, no armor equipped.");
            }
        }
    }


    private void CastSpell()
    {
        if (mana > 0)
        {
            var (target, score) = ChooseBestTargetArea(); // Yeni method: hedef ve score döner
            Equipment targetedEquipment = GetTargetedEquipment(target); // Hedef zırhı alıyoruz
            Equipment weapon = enemyStats.GetEquippedWeapon(); // enemyStats üzerinden silahı alıyoruz

            if (targetedEquipment != null)
            {
                int finalDamage = CalculateFinalDamage(weapon, targetedEquipment);

                // Hedef savunma bölgesine vuruluyor mu?
                if (target == playerDefenseZone)
                {
                    Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone but the player defended!");
                }
                else
                {
                    Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone and hit!");
                    characterHealthController.currentHealth -= finalDamage; // Sonuç olarak hesaplanmış hasar
                }
            }
            else
            {
                Debug.Log("Target zone is empty, no armor equipped.");
            }
        }
        
    }

    private void Attack()
    {
        var (target, score) = ChooseBestTargetArea(); // Yeni method: hedef ve score döner
        Equipment targetedEquipment = GetTargetedEquipment(target); // Hedef zırhı alıyoruz
        Equipment weapon = enemyStats.GetEquippedWeapon(); // enemyStats üzerinden silahı alıyoruz

        if (targetedEquipment != null)
        {
            int finalDamage = CalculateFinalDamage(weapon, targetedEquipment);

            // Hedef savunma bölgesine vuruluyor mu?
            if (target == playerDefenseZone)
            {
                Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone but the player defended!");
            }
            else
            {
                Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone and hit!");
                characterHealthController.currentHealth -= finalDamage; // Sonuç olarak hesaplanmış hasar
            }
        }
        else
        {
            Debug.Log("Target zone is empty, no armor equipped.");
        }
    }

    // Hedef bölgedeki zırhı almak için
    private Equipment GetTargetedEquipment(string target)
    {
        switch (target.ToLower())
        {
            case "head":
                return targetPlayer.equippedItems.ContainsKey(EquipmentSlot.Head) ? targetPlayer.equippedItems[EquipmentSlot.Head] : null;
            case "body":
                return targetPlayer.equippedItems.ContainsKey(EquipmentSlot.Body) ? targetPlayer.equippedItems[EquipmentSlot.Body] : null;
            case "legs":
                return targetPlayer.equippedItems.ContainsKey(EquipmentSlot.Legs) ? targetPlayer.equippedItems[EquipmentSlot.Legs] : null;
            default:
                return null;
        }
    }

    

    // Silah ve zırh türüne göre hasar hesaplama
    private int CalculateFinalDamage(Equipment weapon, Equipment targetedEquipment)
    {
        // Başlangıç hasarı, silahın hasar modifikasyonu - zırhın savunma modifikasyonu
        int damage = weapon.damageModifier - targetedEquipment.armorModifier;

        // Eğer silah ve zırhın hasar türü uyuyorsa (hasar türü eşleşiyorsa)
        if (weapon.damageType == targetedEquipment.damageType)
        {
            damage = Mathf.CeilToInt(damage * 1.5f); // %50 daha fazla hasar
            Debug.Log("Damage type matches, applying 1.5x multiplier!");
        }

        // Eğer hasar negatifse 0 olarak ayarla
        return Mathf.Max(0, damage);
    }


    //              SALDIRI KISMI*


    
    private (string target, int score) ChooseBestTargetArea()
    {
        EquipmentSlot[] targetSlots = { EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Legs };
        string selectedTarget = "";
        int lowestScore = int.MaxValue;

        foreach (var slot in targetSlots)
        {
            if (targetPlayer.equippedItems.ContainsKey(slot))
            {
                Equipment equipment = targetPlayer.equippedItems[slot];
                int score = CalculateEquipmentScore(equipment);

                Debug.Log($"Slot: {slot}, Score: {score}");

                if (score < lowestScore)
                {
                    lowestScore = score;
                    selectedTarget = slot.ToString().ToLower(); 
                }
            }
            else
            {
                Debug.Log($"Slot: {slot} is EMPTY. Prioritizing this.");
                lowestScore = 10; // boş koruma = düşük savunma
                selectedTarget = slot.ToString().ToLower(); 
                break;
            }
        }

        return (selectedTarget, lowestScore);
    }


    private int CalculateEquipmentScore(Equipment equipment)
    {
        return equipment.armorModifier - equipment.damageModifier;
    }
}