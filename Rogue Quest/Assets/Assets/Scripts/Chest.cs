using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool IsOpen;
    public Sprite Closed;
    public Sprite Open;
    public GameObject Treasure;

    public KeyType RequiredTypedKey;
    public GameObject SpecificKeyObject;

    public bool IsFake;
    public GameObject[] Enemies;
    public float damage;

    public void TryOpen(Key requiredKey = null)
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
