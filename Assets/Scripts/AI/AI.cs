using System;
using UnityEngine;
using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Types;
using JetBrains.Annotations;


public class EnemyAI : MonoBehaviour 
{

    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject TargetHead;
    [SerializeField] private GameObject TargetBody;
    [SerializeField] private GameObject TargetLeg;
    private Vector3 targetPosition;
    public event Action OnAttackComplete;
    [SerializeField] private Rigidbody arrowPrefab;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private float launchSpeed = 20f;

    public CharacterMoving characterMoving;
    public CharacterState characterState;
    private Player targetPlayer;
    private EquipmentManager equipmentManager;
    public GameObject gameObject;
    public Transform playerTransform;
    private CharacterMovingButtons characterMovingButtons;
    public EquipmentSlot defecencedSlot;
    public Attributes attributes;

    public int arrowCount = 10;
    public int mana = 10;
    public bool turn = false;

    private Node root;

    public float distanceToPlayer;
    private float arenaRadius = 20f;
    
    private string playerDefenseZone;
    private string enemyDefenseZone;

    private IAnimatorController animatorController;
    
    private EnemyStats enemyStats;

    [SerializeField] private string enemyClass;


    HitController hitController;


    void Start()
    {
        enemyClass = "Archer"; 
        equipmentManager = gameObject.GetComponent<EquipmentManager>() ;
        enemyStats = FindAnyObjectByType<EnemyStats>();
        hitController = GetComponent<HitController>();

        characterMovingButtons =FindAnyObjectByType<CharacterMovingButtons>();
        
        if(characterMovingButtons==null){
            Debug.Log($"null");

        }
        animatorController = new AnimatorController(GetComponent<Animator>());
        
        if (attributes != null)
        {
            attributes.Name = "Enemy";
            attributes._class = enemyClass;
            attributes.race = "";
            attributes.currentHealth = 100;
            attributes.currentStamina = 40;

            Debug.Log($"Enemy Initialized: Name={attributes.Name}, Class={attributes._class}");
        }
        if (targetPlayer != null)
        {
            ChooseBestTargetArea();
        }
        
        
        
       

        
        
        
       

        root = CreateBehaviorTree();

    }

    void Update()
    {
        // Ölüm kontrolü (bir kereye mahsus çalışmalı)
        if (attributes != null && hitController.attributes.isDead)
        {
            animatorController.SetDie();
            return; // Karakter öldüyse geri dön
        }

        // Aşağısı yalnızca yaşayan karakterler için çalışır
        Vector3 fixedPosition = transform.position;
        fixedPosition.y = 0f;
        fixedPosition.z = 0f;
        transform.position = fixedPosition;

        if (!attributes.isDead && turn == false && characterMoving.currentState == CharacterState.Attacking)
        {
            turn = true;

            if (playerTransform != null)
                distanceToPlayer = Mathf.Abs(transform.position.x - playerTransform.position.x);

            PlayerSelectDefense();
            EnemySelectDefense();
            StartCoroutine(delay());
        }
        else if (characterMoving.currentState == CharacterState.Idle)
        {
            turn = false;
        }

        if (attributes.isDead)
        {
            characterMoving.animatorController.SetDie();
        }
    }
    public IEnumerator delay(){
    yield return new WaitForSeconds(2f);
    root.Evaluate();

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
                new ConditionNode(() => distanceToPlayer < 3),
                new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
                }),


                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.7f), 
                    new ActionNode(() => Attack())
                }),
                /*
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.15f && mana > 0), 
                    new ActionNode(() => CastSpell())
                }),
                */
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.10f && arrowCount > 0), 
                    new ActionNode(() => UseRangedAttack())  
                }),
                
                new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
                
            })
        }),

        
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 15f ),  
            new ConditionNode(() => UnityEngine.Random.value <= 0.6f),  
            new ActionNode(() => StartCoroutine(MoveTowardsPlayer())) 
        }),

       
        new Sequence(new List<Node> {
            new ConditionNode(() => distanceToPlayer > 10f && arrowCount > 0 && UnityEngine.Random.value <= 0.3f), 
            new ActionNode(() => UseRangedAttack())  
        }),

        /*
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.5f && mana > 0 ),
            new ActionNode(() => CastSpell())  
        }),

       */
         
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


                new Sequence(new List<Node> {
                new ConditionNode(() => distanceToPlayer < 3),
                new ActionNode(() => StartCoroutine(MoveAwayFromPlayer()))
                }),


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
                /*
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.1f && arrowCount > 0 && mana > 0),
                    new ActionNode(() => CastSpell())
                }),
                */
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
                /*
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount == 0 && mana > 0),
                    new ActionNode(() => CastSpell())
                }),
                */
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
            new ConditionNode(() => UnityEngine.Random.value <= 0.7f && arrowCount > 0 && mana > 0),
            new ActionNode(() => UseRangedAttack())  
        }),
        /*
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount > 0 && mana > 0),
            new ActionNode(() => CastSpell())
        }),
        */
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




/*
        //*OK MEVCUT DEĞİL,MANA VAR
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.3f && arrowCount == 0 && mana > 0),
            new ActionNode(() => CastSpell())  
        }),
        */
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.2f && arrowCount == 0 && mana > 0),
            new ActionNode(() =>StartCoroutine(MoveAwayFromPlayer()))
        }),
        //OK MEVCUT DEĞİL,MANA VAR*



        //*OK YOK,MANA YOK
        
        
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.8f && distanceToPlayer<=15),
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
                /*
                new Sequence(new List<Node> {
                    new ConditionNode(() => UnityEngine.Random.value <= 0.3f && mana > 0),
                    new ActionNode(() => CastSpell())
                }),
                */
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
        /*
        new Sequence(new List<Node> {
            new ConditionNode(() => UnityEngine.Random.value <= 0.4f && mana > 0 && arrowCount > 0),
            new ActionNode(() => CastSpell())
        }),
        */
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
            new ConditionNode(() => UnityEngine.Random.value <= 0.8f && distanceToPlayer<15),
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
    Equipment head = enemyStats.enemyEquipments.Find(e => e.equipSlot == EquipmentSlot.Head);
    Equipment body = enemyStats.enemyEquipments.Find(e => e.equipSlot == EquipmentSlot.Body);
    Equipment legs = enemyStats.enemyEquipments.Find(e => e.equipSlot == EquipmentSlot.Legs);
    Equipment feet = enemyStats.enemyEquipments.Find(e => e.equipSlot == EquipmentSlot.Feet);

    List<Equipment> armorPieces = new List<Equipment> { head, body, legs, feet };
    armorPieces.Sort((a, b) => a.armorModifier.CompareTo(b.armorModifier));

    Equipment selectedArmor;

    float randomValue = UnityEngine.Random.value; 
    if (randomValue < 0.7f)
    {
        selectedArmor = armorPieces[0]; 
    }
    else
    {
        List<Equipment> remainingArmors = armorPieces.GetRange(1, armorPieces.Count - 1);
        selectedArmor = remainingArmors[UnityEngine.Random.Range(0, remainingArmors.Count)];
    }

    switch (selectedArmor.equipSlot)
    {
        case EquipmentSlot.Head:
            enemyDefenseZone = "head";
            break;
        case EquipmentSlot.Body:
            enemyDefenseZone = "body";
            break;
        case EquipmentSlot.Legs:
            enemyDefenseZone = "leg";
            break;
        default:
            enemyDefenseZone = "head";
            break;
    }

    StartCoroutine(PlayDefenseAnimation(enemyDefenseZone,1f)); 
    defecencedSlot = selectedArmor.equipSlot;
    Debug.Log($"Enemy is defending the {enemyDefenseZone} zone (selected armor: {selectedArmor.name}, armor: {selectedArmor.armorModifier})");
}
private IEnumerator PlayDefenseAnimation(string zone, float duration)
{
        characterState = CharacterState.Defending;


        if (zone == "head")
        animatorController.SetDefence1(true);
    else if (zone == "body")
        animatorController.SetDefence2(true);
    else if (zone == "leg")
        animatorController.SetDefence3(true);
    

    yield return new WaitForSeconds(duration);

    
    if (zone == "head")
        animatorController.SetDefence1(false);
    else if (zone == "body")
        {
            if(attributes.isDead==false)
            {
                characterState = CharacterState.Idle;

            }
            animatorController.SetDefence2(false);
        }
        
    else if (zone == "leg")
        animatorController.SetDefence3(false);

    animatorController.SetIdle(true);

        LookAt(playerTransform.position);
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




    //            HAREKET İŞLEMLERİ
    private IEnumerator MoveTowardsPlayer()
    {
        // Düşman oyuncuya yaklaşırken, koşma animasyonunu başlat
        animatorController.StepForward();
        

        
        Vector3 direction = (playerTransform.position - transform.position).normalized;  
        LookAt(direction);
        
        transform.position = transform.position + direction * 1f;

       
        

        
        yield return new WaitForSeconds(0.1f);

        
        
        animatorController.SetIdle(true);

        Debug.Log("Enemy approached the player");
    }




    private IEnumerator MoveAwayFromPlayer()
    {
        
        animatorController.StepBackward();
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
            var target = ChooseBestTargetArea(); 
            Equipment targetedEquipment = GetTargetedEquipment(target); 
            Equipment weapon = enemyStats.GetEquippedWeapon(2);

            if (targetedEquipment != null)
            {
                int finalDamage = CalculateFinalDamage(weapon, targetedEquipment);
                Transform target1 = TargetHead.transform;
                if (target1 == null) return;

                targetPosition = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
                LookAt(targetPosition);
                animatorController.SetArrowAttack1();
                StartCoroutine(ArrowAttackRoutine(target1.position, OnAttackComplete));
                if (target == playerDefenseZone)
                {
                    Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone but the player defended!");
                }
                else
                {
                    Debug.Log($"Enemy attacked with a {weapon.damageType} weapon at the {target} zone and hit!");
                    
                    attributes.UpdateHealth(attributes.currentHealth - finalDamage);


                    
                    

                                   
                }
            }
            else
            {
                Debug.Log("Target zone is empty, no armor equipped.");
            }
        }
    }
    private IEnumerator ArrowAttackRoutine(Vector3 targetPos, Action onComplete)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Bekledi 3sn");

            CreateArrow(targetPos);

            yield return new WaitForSeconds(1f);
            characterState = CharacterState.Idle;
            onComplete?.Invoke();
        }
    private void CreateArrow(Vector3 targetPosition)
    {
        Rigidbody arrow = Instantiate(arrowPrefab, shootPoint.transform.position, Quaternion.identity);

        // Hedef yönünü hesapla
        Vector3 direction = (targetPosition - shootPoint.transform.position).normalized;

        // Oku hızla fırlat
        arrow.linearVelocity = direction * launchSpeed;

        // Okun yönünü hız vektörüne çevir
        arrow.transform.rotation = Quaternion.LookRotation(direction);
    }
/*
    private void CastSpell()
    {
        if (mana > 0)
        {
            var (target, score) = ChooseBestTargetArea(); // Yeni method: hedef ve score döner
            Equipment targetedEquipment = GetTargetedEquipment(target); // Hedef zırhı alıyoruz
            Equipment weapon = enemyStats.GetEquippedWeapon(1); // enemyStats üzerinden silahı alıyoruz

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
                    attributes.UpdateHealth(finalDamage);
                }
            }
            else
            {
                Debug.Log("Target zone is empty, no armor equipped.");
            }
        }
        
    }
*/
    private void PlayAttackAnimation(string target)
    {
        switch (target.ToLower())
        {
            case "head":
                animatorController.SetAttacking1();
                break;
            case "body":
                animatorController.SetAttacking2();
                break;
            case "legs":
            case "feet":
                animatorController.SetAttacking3();
                break;
            default:
                animatorController.SetAttacking1(); 
                break;
        }

        Debug.Log($"[Animation] Enemy attacks {target} with correct animation.");
        LookAt(playerTransform.position);
        animatorController.SetIdle(true);

    }

    private void Attack()
    {
        var target = ChooseBestTargetArea(); // Yeni method: hedef ve score döner
        PlayAttackAnimation(target);
        Equipment targetedEquipment = GetTargetedEquipment(target); // Hedef zırhı alıyoruz
        Equipment weapon = enemyStats.GetEquippedWeapon(2); // enemyStats üzerinden silahı alıyoruz

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
                    attributes.UpdateHealth(attributes.currentHealth - finalDamage);
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
                return equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Head) ? equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Head) : null;
            case "body":
                return equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Body) ? equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Body) : null;
            case "legs":
                return equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Legs) ? equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Legs) : null;
    //        case "feet": 
//                return equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Feet) ? equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Feet) : null;
            default:
                return null;
        }
    }



    
    private int CalculateFinalDamage(Equipment weapon, Equipment targetedEquipment)
    {
        
        int damage = weapon.damageModifier - targetedEquipment.armorModifier;

       
        if (weapon.damageType == targetedEquipment.damageType)
        {
            damage = Mathf.CeilToInt(damage * 1.5f); 
            Debug.Log("Damage type matches, applying 1.5x multiplier!");
        }

        
        return Mathf.Max(0, damage);
    }


    //              SALDIRI KISMI*


//değişicek
    private string ChooseBestTargetArea()
{
    var head = equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Head);
    var body = equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Body);
    var legs = equipmentManager.currentEquipment.Find(x => x.equipSlot == EquipmentSlot.Legs);

    Dictionary<string, int> zoneWeights = new Dictionary<string, int>();

    zoneWeights["head"] = head == null ? 10 : Math.Max(1, 20 - head.armorModifier);
    zoneWeights["body"] = body == null ? 10 : Math.Max(1, 20 - body.armorModifier);
    zoneWeights["legs"] = legs == null ? 10 : Math.Max(1, 20 - legs.armorModifier);

    List<string> weightedTargets = new List<string>();
    foreach (var pair in zoneWeights)
    {
        for (int i = 0; i < pair.Value; i++)
        {
            weightedTargets.Add(pair.Key);
        }
    }

    int index = UnityEngine.Random.Range(0, weightedTargets.Count);
    return weightedTargets[index];
}


    public void LookAt(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }
}