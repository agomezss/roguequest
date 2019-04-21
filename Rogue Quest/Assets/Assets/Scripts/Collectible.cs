using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject Owner;

    public void Collect(GameObject owner)
    {
        Owner = owner;
    }

    // Put back on the inventory
    public void Unequip()
    {

    }

    // Set main equip to owner
    public void Equip()
    {

    }

    // Attack, defend, drink
    public void Use(GameObject owner)
    {
        Owner = owner;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If us being used update position relative to owner
        if(Owner && Owner.transform)
        {
            transform.position = new Vector2(Owner.transform.position.x + (transform.localScale.x), Owner.transform.position.y);
        }
    }
}
