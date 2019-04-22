using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HintType
{
    FollowText = 0,
    WarningPlate = 1,
    CameraText = 2
}

public class Hint : MonoBehaviour
{
    public HintType Type;
    public string Text;
    public List<string> Texts;
    public int Size;
    public Color Color;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
