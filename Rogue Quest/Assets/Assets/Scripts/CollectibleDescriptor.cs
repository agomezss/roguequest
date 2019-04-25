
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
    None,
    RegularWeapon, // ie: sword, dagger, knife, staff
    WithProjectile, // ie: bow
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
    QuestKey = 3,
    Gold = 4,
}

public enum EffectStat
{
    None,
    Life,
    Mana,
    Speed,
    Jump,
    Climb,
    Attack,
    Defense,
    Intelligence,
    IntelligenceResistance,
    WaterResistance,
    PoisonResistance,
    LavaResistence,
    Time,
}

public enum EffectType
{
    None,
    Recover,
    Increase,
    TemporaryImprovement,
    TemporatyInfinity
}
