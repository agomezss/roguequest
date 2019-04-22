using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Collectible : MonoBehaviour
{
#if UNITY_EDITOR
    public string Name;
#endif

    public Guid UID = Guid.NewGuid();
    public Sprite Graphic;

    public bool IsUnique;
    public UniqueType Unique;
    public CollectibleType Type;
    public EquipableType EquipType;
    public EquipableRarity Rarity;
    public EquipableDurability Durability;
    public WeaponType Weapon;
    public Sprite WeaponProjectile;
    public int WeaponQuantity;
    public float WeaponDurability;
    public float WeaponDamage;
    public bool HideWhenShootProjectile;

    public PotionType Potion;
    public float WorthPoints;
    public float WorthPercentage;
    public float WorthDuration;
    private GameObject Owner;

    private bool Collected;
    private bool BeingUsed;
    private SpriteRenderer Renderer;
    private BoxCollider2D col;

    private float lastHighlightTime = 0f;
    private float HighlightFrequency = 2f;

    public void Collect(GameObject owner)
    {
        col.enabled = false;

        Owner = owner;

        var inventory = owner.GetComponent<Inventory>();

        if (inventory)
        {
            inventory.Add(this);
        }

        Renderer.enabled = false;
        Collected = true;


        Color restoredColor = Color.white;
        restoredColor.a = 1f;
        Renderer.color = restoredColor;
    }

    // Attack, defend, drink
    public void Use(GameObject owner)
    {
        Owner = owner;
        BeingUsed = true;
    }

    public void UnUse()
    {
        BeingUsed = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (Renderer && Renderer.sprite)
            Graphic = Renderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Collected) return;

        if (other.transform.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Collected)
        {
            if (Time.time - lastHighlightTime > HighlightFrequency)
            {
                lastHighlightTime = Time.time;
                var fadeOptions = new BlinkColorOptions();
                fadeOptions.Color1 = Renderer.color;
                fadeOptions.Color2 = Color.gray;
                SendMessage("BlinkColor", fadeOptions, SendMessageOptions.DontRequireReceiver);
                // var fadeOptions = new FadeOptions();
                // fadeOptions.newColor = Color.yellow;
                // SendMessage("Fade", fadeOptions, SendMessageOptions.DontRequireReceiver);
            }
        }
        else if (Collected && !BeingUsed)
        {
            Renderer.enabled = false;
        }
        else if (BeingUsed &&
           (EquipType == EquipableType.Weapon || EquipType == EquipableType.Shield))
        {
            Renderer.enabled = true;

            // If us being used update position relative to owner
            if (Owner && Owner.transform)
            {
                transform.position = new Vector2(Owner.transform.position.x + (Owner.transform.localScale.x / 1.5f), Owner.transform.position.y);
            }
        }
    }
}
