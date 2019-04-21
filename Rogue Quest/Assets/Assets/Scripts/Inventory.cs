using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public GameObject Owner;

    public List<Collectible> StoredItems = new List<Collectible>();
    public int StoreCapacity = 10;
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

    public void Add(Collectible item)
    {
        if (StoredItems.Count < StoreCapacity)
            StoredItems.Add(item);
    }

    public void Equip(Collectible item)
    {

    }

    public void UnEquip(Collectible item)
    {

    }

    public void Drop(Collectible item)
    {

    }

    public void Destroy(Collectible item)
    {

    }

    public void DropAll()
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
