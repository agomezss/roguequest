using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool DestroyOnOpen;
    public bool IsOpen;
    public Sprite ClosedGraphic;
    public Sprite OpenGraphic;
    public GameObject[] Treasures;

    public KeyType RequiredTypedKey;
    public string SpecificKeyName;

    public GameObject[] Enemies;
    public float damage;

    private SpriteRenderer rend;
    private BoxCollider2D col;

    private float lastHighlightTime = 0f;
    private float HighlightFrequency = 2f;


    public bool TryOpen(Collectible key = null, GameObject opener = null)
    {
        if (RequiredTypedKey != KeyType.None)
        {
            if (IsOpen || key == null) return false;
            if (RequiredTypedKey != key.SpecificKeyType) return false;
            if (!string.IsNullOrEmpty(SpecificKeyName) && key.Name != SpecificKeyName) return false;
        }

        Open(opener);
        return true;
    }

    public void Close()
    {
        IsOpen = false;

        if (ClosedGraphic)
            rend.sprite = ClosedGraphic;

        col.enabled = true;
    }

    public void Open(GameObject opener = null)
    {
        IsOpen = true;

        col.enabled = false;

        if (OpenGraphic)
        {
            rend.sprite = OpenGraphic;
            var color = rend.material.color;
            color.a = 0.25f;
            rend.material.color = color;
        }

        RevealTreasures(opener);

        if (DestroyOnOpen)
            Destroy(gameObject);
    }

    void CreateTreasures()
    {
        foreach (var item in Treasures)
        {
            var position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            Instantiate(item, position, Quaternion.identity);
        }
    }

    void CreateEnemies()
    {
        foreach (var item in Enemies)
        {
            var position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            Instantiate(item, position, Quaternion.identity);
        }
    }

    void RevealTreasures(GameObject opener = null)
    {
        if (opener && damage != 0f)
        {
            var stats = opener.GetComponent<Stats>();

            if (stats)
            {
                stats.GetDamage(damage);
            }
        }

        CreateEnemies();
        CreateTreasures();
    }

    // Start is called before the first frame update
    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!IsOpen)
        {
            if (Time.time - lastHighlightTime > HighlightFrequency)
            {
                lastHighlightTime = Time.time;
                var fadeOptions = new BlinkColorOptions();
                fadeOptions.Color1 = rend.color;
                fadeOptions.Color2 = Color.gray;
                SendMessage("BlinkColor", fadeOptions, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
