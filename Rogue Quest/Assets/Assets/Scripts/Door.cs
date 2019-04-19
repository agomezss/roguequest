using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    public Sprite ClosedGraphic;
    public Sprite OpenGraphic;
    
    public bool HideRoomContent;
    public GameObject Room;

    public bool IsOpen;
    public KeyType RequiredTypedKey;
    public GameObject SpecificKeyObject;

    void TryOpen(Key key)
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
