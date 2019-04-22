using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool DestroyOnOpen;
    public bool IsOpen;
    public Sprite ClosedGraphic;
    public Sprite OpenGraphic;
    public GameObject Treasure;

    public KeyType RequiredTypedKey;
    public string SpecificKeyName;

    public bool IsFake;
    public GameObject[] Enemies;
    public float damage;

    private SpriteRenderer renderer;
    private BoxCollider2D col;


    public bool TryOpen(Key key = null)
    {
        if (IsOpen) return false;
        if (key && RequiredTypedKey != key.Type) return false;
        if (key && !string.IsNullOrEmpty(SpecificKeyName) && key.Name != SpecificKeyName) return false;

        Open();
        return true;
    }

    public void Close()
    {
        IsOpen = false;

        if (ClosedGraphic)
            renderer.sprite = ClosedGraphic;

        col.enabled = true;
    }

    public void Open()
    {
        IsOpen = true;

        col.enabled = false;

        if (OpenGraphic)
            renderer.sprite = OpenGraphic;

        if (DestroyOnOpen)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
}
