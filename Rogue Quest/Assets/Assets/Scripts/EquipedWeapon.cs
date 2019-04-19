using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedWeapon : MonoBehaviour
{
    public Collectible Item;
    public GameObject Owner;

    // In case of weapon attack, shield defend
    public void Use()
    {

    }

    // Turns into a collectible
    public void Drop()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
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
