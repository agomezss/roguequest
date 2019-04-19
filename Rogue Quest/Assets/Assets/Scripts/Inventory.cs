using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject Owner;

    public Collectible[] StoredItems;
    public int StoreCapacity;
    public int Gold;

    public Collectible MainWeapon;
    public Collectible MainShield;
    public Collectible MainRing;
    public Collectible MainClothes;

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
