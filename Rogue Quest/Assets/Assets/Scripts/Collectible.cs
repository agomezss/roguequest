using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectible : MonoBehaviour
{
#if UNITY_EDITOR
    public string Name;
#endif

    public string UID;
    public CollectibleType Type;
    public EquipableType EquipType;
    public UniqueType Unique;
    public EquipableRarity Rarity;
    public WeaponType Weapon;
    public GameObject WeaponProjectile;
    public int WeaponProjectileQuantity; // -1 for infinite
    public float WeaponProjectileSpeed = 10f;
    public int WeaponQuantity = 1;
    public float WeaponDurability = -1f;
    public float WeaponDamage = 10f;
    public bool HideWhenShoot;
    public EffectType EffectType;
    public EffectStat EffectStat;
    public float WorthPoints;
    public float WorthPercentage;
    public float WorthDuration;

    public KeyType SpecificKeyType;

    private Sprite Graphic;
    private GameObject Owner;
    private bool Collected;
    private bool BeingUsed;
    private SpriteRenderer Renderer;
    private BoxCollider2D col;

    public void Collect(GameObject owner)
    {
        col.enabled = false;

        Owner = owner;

        var inventory = owner.GetComponent<Inventory>();

        if (inventory)
        {
            inventory.Add(this);
        }

        Color restoredColor = Color.white;
        restoredColor.a = 1f;
        Renderer.color = restoredColor;
        Renderer.enabled = false;

        Collected = true;
    }

    // Attack, defend, drink
    public void Use()
    {
        BeingUsed = true;
    }

    public void UnUse()
    {
        BeingUsed = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        UID = Guid.NewGuid().ToString();
        Renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (Renderer && Renderer.sprite)
            Graphic = Renderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") ||
            other.transform.CompareTag("Enemy"))
        {
            if (!Collected)
            {
                Collect(other.gameObject);
            }
            else if (other.gameObject != Owner)
            {
                ApplyDamage(other.gameObject);
            }
        }
    }

    void ApplyDamage(GameObject obj)
    {
        var enemyStats = obj.GetComponent<Stats>();

        if (enemyStats)
            enemyStats.GetDamage(WeaponDamage);
    }

    public void Shoot()
    {
        var instance = Instantiate(WeaponProjectile, transform.position, Quaternion.identity);
        var projectile = instance.GetComponent<Projectile>();
        projectile.Shooter = gameObject;
        projectile.Damage = WeaponDamage;

        instance.GetComponent<Rigidbody2D>().AddForce(transform.right * WeaponProjectileSpeed);
        Physics2D.IgnoreCollision(instance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Update()
    {
        if (!Collected)
        {
            SendMessage("BlinkColor", SpecialFX.GetDefaultOptionsBlink(Renderer), SendMessageOptions.DontRequireReceiver);
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
                var weaponYOffset = .15f;
                transform.position = new Vector2(Owner.transform.position.x + (Owner.transform.localScale.x / 1.5f), Owner.transform.position.y + weaponYOffset);
                transform.localScale = new Vector2(Owner.transform.localScale.x < 0 ? -1f *  Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x),transform.localScale.y);
            }
        }
    }
}
