using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string enemyClass; // Düþman sýnýfý (Warrior, Mage, Archer)
    public string playerClass; // Oyuncunun sýnýfý
    public float health = 100f;
    public float attackPower = 10f;
    public float defense = 5f;
    public float agility = 3f;
    public float mana = 50f;
    [SerializeField] public float levelThreshold = 5f;
    [SerializeField] private bool canUpgrade = false;

    // Geliþim miktarlarý
    public float attackUpgradeAmount = 5f;
    public float defenseUpgradeAmount = 2f;
    public float manaUpgradeAmount = 10f;
    public float agilityUpgradeAmount = 1f;

    [SerializeField] public float playerLevel = 1f; // Oyuncu seviyesi
    private float lastUpgradeLevel = 0f; // En son yükseltme yapýlan seviye

    public float upgradeChanceForArcher = 0.7f;
    public float upgradeChanceForMage = 0.5f;
    public float upgradeChanceForWarrior = 0.6f;

    void Start()
    {
        AssignInitialStats(); // Baþlangýç istatistiklerini belirle
    }

    void Update()
    {
        CheckPlayerLevelAndUpgrade(); // Oyuncu seviyesini sürekli kontrol et
    }

    private void AssignInitialStats()
    {
        switch (enemyClass)
        {
            case "Archer":
                attackPower = 12f;
                agility = 7f;
                break;
            case "Mage":
                mana = 80f;
                defense = 3f;
                break;
            case "Warrior":
                attackPower = 15f;
                defense = 8f;
                break;
            default:
                Debug.Log("Bilinmeyen düþman sýnýfý!");
                break;
        }
    }

    private void CheckPlayerLevelAndUpgrade()
    {
        if (playerLevel >= lastUpgradeLevel + levelThreshold)
        {
            canUpgrade = true;
            UpgradeEnemyStats();
        }
    }

    private void UpgradeEnemyStats()
    {
        if (!canUpgrade) return;

        float upgradeChance = playerClass switch
        {
            "Archer" => upgradeChanceForArcher,
            "Mage" => upgradeChanceForMage,
            "Warrior" => upgradeChanceForWarrior,
            _ => 0f
        };

        if (UnityEngine.Random.value <= upgradeChance)
        {
            switch (enemyClass)
            {
                case "Archer":
                    AgilityUpgrade();
                    AttackUpgrade();
                    break;
                case "Mage":
                    ManaUpgrade();
                    DefenseUpgrade();
                    break;
                case "Warrior":
                    AttackUpgrade();
                    DefenseUpgrade();
                    break;
            }
            lastUpgradeLevel = playerLevel; // Son yükseltme seviyesini kaydet
            Debug.Log($"Düþman {enemyClass} yükseltildi! Yeni seviye: {playerLevel}");
        }
        else
        {
            Debug.Log("Yükseltme baþarýsýz oldu.");
        }

        canUpgrade = false;
    }

    private void AttackUpgrade() => attackPower += attackUpgradeAmount;
    private void DefenseUpgrade() => defense += defenseUpgradeAmount;
    private void ManaUpgrade() => mana += manaUpgradeAmount;
    private void AgilityUpgrade() => agility += agilityUpgradeAmount;

    public int GetAttackPower() => Mathf.RoundToInt(attackPower);
    public int GetSpellPower() => Mathf.RoundToInt(mana * 0.3f);
    public int GetArrowPower() => Mathf.RoundToInt(attackPower * 1.2f);
}
