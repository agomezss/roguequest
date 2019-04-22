
public enum CollectibleType
{
    Usable = 0,
    Equipable = 1,
    Unique = 2,
}

public enum EquipableType
{
    Weapon = 1,
    Shield = 2,
    Ring = 3,
    Clothe = 4,
    Other = 5
}

public enum WeaponType
{
    NoWeapon,
    Shield,
    Weapon, // ie: sword, dagger, knife, staff
    WeaponWithProjectile, // ie: bow
    SelfProjectile, // ie: shuriken, magic
    ThrowDistanceThanBack // ie: boomerang
}

public enum EquipableRarity
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5
}
public enum UniqueType
{
    None = 0,
    Key = 1,
    Trasure = 2,
    QuestKey = 3
}

public enum UsableEffect
{
    None,
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
