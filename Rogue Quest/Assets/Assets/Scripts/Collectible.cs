using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType
{
    Treasure = 0,
    Potion = 1,
    Food = 2,
    Equipable = 3,
    Unique = 5,
}

public enum EquipableType
{
    Weapon = 1,
    Shield = 2,
    Rune = 3,
    Ring = 4,
    Clothe = 5,
    Other = 6
}

public enum WeaponType
{
    Shield = 0,
    Sword,
    Axe,
    Bow,
    Arrow,
    Crossbow,
    Hammer,
    Whip,
    Spear,
    Dagger,
    Nunchaku,
    Shuriken,
    Staff,
}

public enum EquipableRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public enum EquipableDurability
{
    Eternal,
    Brokable
}

public enum UniqueType
{
    Key,
    Trasure
}

public enum PotionType
{
    LifeCure,
    ManaCure,
    WaterImprovement,
    LavaResistance,
    PoisonResistance,
    SpeedUp,
    AttackUp,
    DefenseUp,
    InfiniteArrows,
    WeaponAttackUp,
    JumpUp,
    EternalMana,
    Imortal,
    TimeLower,
}

public class Collectible : MonoBehaviour
{
    public string ResourceName;
    
    public bool IsUnique;
    public UniqueType Unique;

    public CollectibleType Type;
    public EquipableType EquipType;
    public EquipableRarity Rarity;
    public EquipableDurability Durability;

    public WeaponType Weapon;
    public int WeaponQuantity;
    
    public float WeaponDurability;
    public float WeaponDamage;

    public PotionType Potion;
    public float WorthPoints;
    public float WorthPercentage;
    public float WorthDuration;

    public GameObject WorthManipulation;

    public void Collect()
    {

    }

    // Put back on the inventory
    public void Unequip()
    {

    }

    // Set main equip to owner
    public void Equip()
    {

    }

    // In case of potion
    public void Use()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
