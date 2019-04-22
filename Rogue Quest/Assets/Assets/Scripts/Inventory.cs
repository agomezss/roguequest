using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public GameObject Owner;

    public List<Collectible> StoredItems = new List<Collectible>();
    public int StoreCapacity = 20;
    public int Gold;

    public Collectible MainWeapon;
    public Collectible MainShield;
    public Collectible MainRing;
    public Collectible MainClothes;

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public bool HasGold(int amount)
    {
        return Gold >= amount;
    }

    public void PayGold(int amount)
    {
        if (Gold >= amount)
            Gold -= amount;
    }

    public bool Add(Collectible item)
    {
        if (!item) return false;

        if (StoredItems.Count < StoreCapacity)
            StoredItems.Add(item);

        return true;
    }

    public void Equip(Collectible item)
    {
        if (!item) return;

        if (item.EquipType == EquipableType.Weapon)
        {
            UnEquip(MainWeapon);
            MainWeapon = item;
        }

        if (item.EquipType == EquipableType.Shield)
        {
            UnEquip(MainShield);
            MainShield = item;
        }

        if (item.EquipType == EquipableType.Ring)
        {
            UnEquip(MainRing);
            MainRing = item;
        }

        if (item.EquipType == EquipableType.Clothe)
        {
            UnEquip(MainClothes);
            MainClothes = item;
        }
    }

    public void UnEquip(Collectible item)
    {
        if (!item) return;

        if (!StoredItems.Any(a => a.UID == item.UID))
            StoredItems.Add(item);
    }

    public void Drop(Collectible item)
    {
        if (!item) return;
        // TODO: Show
    }

    public void DropGold()
    {
        if (Gold <= 0) return;
        // TODO: Instantiate gold equivalent to gold
    }

    public void Destroy(Collectible item)
    {
        Destroy(item.gameObject);
    }

    public Collectible SearchKey(KeyType requiredTypedKey, string specificKeyName)
    {
        var byName = StoredItems.FirstOrDefault(a=>a.Name == specificKeyName);

        if(byName!=null) return byName;

        var byType = StoredItems.FirstOrDefault(a=>a.SpecificKeyType == requiredTypedKey);

        return byType;
    }

    public void DropAll()
    {
        foreach (var item in StoredItems)
        {
            Drop(item);
        }

        Drop(MainWeapon);
        Drop(MainShield);
        Drop(MainRing);
        Drop(MainClothes);
        DropGold();
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
