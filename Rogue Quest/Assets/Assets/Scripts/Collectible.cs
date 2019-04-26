﻿using System.Collections;
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
    public GameObject WeaponProjectile;
    public int WeaponProjectileQuantity; // -1 for infinite
    public float WeaponProjectileUsePerSecond = 0.5f;
    public float WeaponProjectileLastUsedTime = 0f;
    public float WeaponProjectileSpeed = 10f;
    public int WeaponQuantity = 1; // -1 for infinite
    public float WeaponDurability = -1f; // -1 for infinite
    public float WeaponDamage = 10f;
    public bool HideWhenShoot;
    public EffectType EffectType;
    public EffectStat EffectStat;
    public float WorthPoints;
    public float WorthPercentage;
    public float WorthDuration;
    public float WeaponYOffsetEquiped = .15f;
    public float WeaponXScaleEquiped = 1.5f;
    public KeyType SpecificKeyType;

    private Sprite Graphic;
    private GameObject Owner;
    private bool Collected;
    private bool BeingUsed;
    private SpriteRenderer Renderer;
    private BoxCollider2D col;

    public void Attach(GameObject owner)
    {
        if (col) col.enabled = false;
        Owner = owner;
        Collected = true;
    }

    public void Collect(GameObject owner)
    {
        var inventory = owner.GetComponent<Inventory>();

        if (inventory)
        {
            inventory.Add(this);
        }

        if (Renderer)
        {
            Color restoredColor = Color.white;
            restoredColor.a = 1f;
            Renderer.color = restoredColor;
            Renderer.enabled = false;
        }

        Attach(owner);
    }

    public void Use(GameObject user)
    {
        BeingUsed = true;

        if (WeaponProjectileQuantity > 0 &&
            Time.time - WeaponProjectileLastUsedTime > WeaponProjectileUsePerSecond)
        {
            WeaponProjectileLastUsedTime = Time.time;
            WeaponProjectileQuantity--;
            ShootProjectile(user);
        }
    }

    public void UnUse()
    {
        BeingUsed = false;
    }

    public void ShootProjectile(GameObject shooter)
    {
        var direction = Vector2.right;
        var sign = Mathf.Sign(transform.localScale.x);
        if (sign < 0) direction = Vector2.left;

        var instance = Instantiate(WeaponProjectile, transform.position, Quaternion.identity);
        instance.transform.localScale = new Vector2(transform.localScale.x < 0 ? -1f * Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x), transform.localScale.y);

        var projectile = instance.GetComponent<Projectile>();
        projectile.Shooter = shooter;
        projectile.Damage = WeaponDamage;

        instance.GetComponent<Rigidbody2D>().AddForce(direction * WeaponProjectileSpeed);
        Physics2D.IgnoreCollision(instance.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Destroy(instance, 2f);
    }

    void ApplyDamage(GameObject obj)
    {
        var enemyStats = obj.GetComponent<Stats>();

        if (enemyStats)
            enemyStats.GetDamage(WeaponDamage);
    }

    void Awake()
    {
        UID = Guid.NewGuid().ToString();
        Renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (Renderer && Renderer.sprite)
            Graphic = Renderer.sprite;
    }

    void Update()
    {
        if (!Collected)
        {
            SendMessage("BlinkColor", SpecialFX.GetDefaultOptionsBlink(Renderer), SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Renderer.enabled = BeingUsed && !HideWhenShoot &&
                                (EquipType == EquipableType.Weapon ||
                                    EquipType == EquipableType.Shield);

            if (Owner && Owner.transform)
            {
                transform.position = new Vector2(Owner.transform.position.x + (Owner.transform.localScale.x / WeaponXScaleEquiped), Owner.transform.position.y + WeaponYOffsetEquiped);
                transform.localScale = new Vector2(Owner.transform.localScale.x < 0 ? -1f * Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x), transform.localScale.y);

                if (transform.rotation.eulerAngles.z != 0f)
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Sign(Owner.transform.localScale.x) < 0f ? 45f : -45f));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
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
}
