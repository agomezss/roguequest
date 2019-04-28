using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : MonoBehaviour
{
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

        if (item.EquipType == EquipableType.Weapon && MainWeapon == null)
        {
            MainWeapon = item;
        }
        else if (item.EquipType == EquipableType.Shield && MainShield == null)
        {
            MainShield = item;
        }
        else if (item.EquipType == EquipableType.Ring && MainRing == null)
        {
            MainRing = item;
        }
        else if (item.EquipType == EquipableType.Clothe && MainClothes == null)
        {
            MainClothes = item;
        }
        if (item.Type == CollectibleType.Unique && item.Unique == UniqueType.Gold)
        {
            Gold += (int)item.WorthPoints;
        }
        else if (StoredItems.Count < StoreCapacity)
        {
            StoredItems.Add(item);
        }

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
        if (item == null) return;

        // Debug.Log($"Instanciating: {item.Name}, transform: {transform}");

        // var inst = Instantiate(item, transform);
        item.Deattach(transform);

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

    public Collectible SearchByName(string name)
    {
        return StoredItems.FirstOrDefault(a => a.Name == name);
    }

    public Collectible SearchKey(KeyType requiredTypedKey, string specificKeyName)
    {
        var byName = SearchByName(specificKeyName);

        if (byName != null) return byName;

        var byType = StoredItems.FirstOrDefault(a => a.SpecificKeyType == requiredTypedKey);

        return byType;
    }

    public void DropAllInventory()
    {
        foreach (var item in StoredItems)
        {
            Drop(item);
        }

        Drop(MainWeapon);
        Drop(MainShield);
        Drop(MainRing);
        Drop(MainClothes);
    }

    void AttachAll()
    {
        var newToBeAdded = new List<Collectible>();
        foreach (var item in StoredItems)
        {
            newToBeAdded.Add(Attach(item));
        }

        StoredItems.Clear();
        StoredItems.AddRange(newToBeAdded);

        if (MainWeapon)
            MainWeapon = Attach(MainWeapon);

        if (MainShield)
            MainShield = Attach(MainShield);

        if (MainRing)
            MainRing = Attach(MainRing);

        if (MainClothes)
            MainClothes = Attach(MainClothes);
    }

    Collectible Attach(Collectible item)
    {
        if (item == null) return null;

        var instance = Instantiate(item, transform);
        instance.transform.SetParent(transform);
        instance.GetComponent<Collectible>().Attach(gameObject);
        return instance;
    }

    void Awake()
    {
        AttachAll();
    }
}
