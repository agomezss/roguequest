using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyType
{
    None,
    Any,
    Gold,
    Silver,
    Skull,
    Specific
}

public class Key : MonoBehaviour
{
    public KeyType Type;
    public string Name;
    
}
