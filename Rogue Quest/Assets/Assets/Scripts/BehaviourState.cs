using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourState : MonoBehaviour
{
    private AnimedTile anims;
    private Stats Stats;
    private Inventory Inventory;
    private Rigidbody2D rb;

    public bool IsIdle;
    public bool IsWalking;
    public bool IsAttacking;
    public bool IsDefending;
    public bool IsAware;
    public bool IsJumping;
    public bool IsClimbing;
    public bool IsFlying;
    public bool IsSwimming;

    public bool IsDead;
    private bool IsGrounded;
    private bool IsShielded;
    private bool IsUsingWeapon;
    private float LastWeaponUsedTime = 0f;
    private float RestrictWeaponUsagePerSecond = 0.4f;

    private bool IsLaddered;
    private bool IsUnderLiquid;


    // Start is called before the first frame update
    void Awake()
    {
        anims = GetComponent<AnimedTile>();
        Stats = GetComponent<Stats>();
        Inventory = GetComponent<Inventory>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetIdle()
    {
        IsIdle = true;
        IsWalking = false;
        IsAttacking = false;
        IsClimbing = false;
        IsJumping = false;
        IsSwimming = false;
    }
    public void GetAware()
    {
        IsAware = true;
    }
    public void GetDead()
    {
        IsDead = true;
    }

    public void FlyUp()
    {
        IsWalking = true;
    }

    public void FlyDown()
    {
        IsWalking = true;
    }

    public void MoveLeft()
    {
        IsWalking = true;
    }

    public void RightLeft()
    {
        IsWalking = true;
    }

    // Could mean climb ladder or jump
    public void MoveUp()
    {
        // Detect collision with ladder and decide if just move up or jump
        // If climb then unshield
        IsShielded = false;

        // If Grounded Jump
        IsJumping = true;
    }

    public void UseWeapon()
    {
        if (Time.time - LastWeaponUsedTime < RestrictWeaponUsagePerSecond) return;
        if (Inventory == null || Inventory.MainWeapon == null || Inventory.MainWeapon.WeaponQuantity == 0) return;

        IsShielded = false;
        var lastWeaponUsedTime = Time.time;
        var weapon = Inventory.MainWeapon;
    }

    // Could mean climb ladder or jump
    public void UseShield()
    {
        if (IsShielded || Inventory == null ||
           Inventory.MainShield == null || Inventory.MainShield.WeaponDurability == 0) return;

        IsShielded = true;
        var shield = Inventory.MainShield;
    }

    // Update is called once per frame
    void Update()
    {

    }


    // If Collides with floor and jumpforce = 0 then IsGrounded = true
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "GroundWall" && rb.velocity.y == 0f)
        {
            IsGrounded = true;
        }

        if (other.transform.tag == "Ladder")
        {
            IsLaddered = true;
        }
        else
        {
            IsLaddered = false;
        }

        if (other.transform.tag == "Water")
        {
            IsUnderLiquid = true;
        }
        else
        {
            IsUnderLiquid = false;
        }
    }

}