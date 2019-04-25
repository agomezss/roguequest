using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool DestroyOnOpen;

    private SpriteRenderer rend;
    private BoxCollider2D col;

    public Sprite ClosedGraphic;
    public Sprite OpenGraphic;

    public bool HideRoomContent;

    public bool IsOpen;
    public KeyType RequiredTypedKey;
    public string SpecificKeyName;

    public bool TryOpen(Collectible key = null)
    {
        if (RequiredTypedKey != KeyType.None)
        {
            if (IsOpen || key == null) return false;
            if (RequiredTypedKey != key.SpecificKeyType) return false;
            if (!string.IsNullOrEmpty(SpecificKeyName) && key.Name != SpecificKeyName) return false;
        }

        Open();
        return true;
    }

    public void Close()
    {
        IsOpen = false;

        if (ClosedGraphic)
            rend.sprite = ClosedGraphic;

        col.enabled = true;
    }

    public void Open()
    {
        IsOpen = true;

        col.enabled = false;

        if (OpenGraphic)
            rend.sprite = OpenGraphic;

        if (HideRoomContent)
            UnhideHidedContent();

        if (DestroyOnOpen)
            Destroy(gameObject);
    }

    private void UnhideHidedContent()
    {
        var mask = GetComponentsInChildren<Masked>();
        foreach (var item in mask)
        {
            Destroy(item.gameObject);
        }
    }

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
}
