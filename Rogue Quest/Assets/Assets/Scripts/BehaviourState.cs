using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourState : MonoBehaviour
{
    private AnimedTile anims;
    private Stats Stats;
    private Inventory Inventory;
    private Rigidbody2D rb2d;
    private BoxCollider2D col;

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
    public bool IsGrounded;
    public string LookingObject;
    public bool IsShielded;
    public bool IsUsingWeapon;
    public bool IsLaddered;
    public bool IsUnderLiquid;

    private float LastWeaponUsedTime = 0f;
    private float LastJump = 0f;
    private float RestrictWeaponUsagePerSecond = 0.4f;

    void Awake()
    {
        anims = GetComponent<AnimedTile>();
        Stats = GetComponent<Stats>();
        Inventory = GetComponent<Inventory>();
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    public void GetIdle()
    {
        rb2d.velocity = Vector2.Lerp(rb2d.velocity, new Vector2(0f, rb2d.velocity.y), 0.5f);

        IsIdle = true;
        IsWalking = false;
        IsAttacking = false;
        IsClimbing = false;
        IsJumping = false;
        IsSwimming = false;

        if (Time.time - LastJump > 1f)
            anims.Play("idle");
    }

    public void GetAware()
    {
        IsAware = true;
        anims.Play("aware");
    }

    public void GetDead()
    {
        IsDead = true;
        anims.Play("dead");
    }

    public void FlyUp()
    {
        rb2d.gravityScale = 0.2f;
        IsIdle = false;

        var h = 1;
        var speed = Stats.GetClimbSpeed();

        rb2d.velocity = new Vector2(rb2d.velocity.x, h * speed);

        IsFlying = true;
        IsGrounded = false;
    }

    public void FlyDown()
    {
        rb2d.gravityScale = 0.2f;
        IsIdle = false;

        var h = -1;
        var speed = Stats.GetClimbSpeed();

        rb2d.velocity = new Vector2(rb2d.velocity.x, h * speed);

        IsFlying = true;
        IsGrounded = false;
    }

    public void MoveLeft()
    {
        transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        var h = -1;
        var speed = Stats.GetSpeed();

        if (h * Mathf.Abs(rb2d.velocity.x) < speed)
            rb2d.AddForce(Vector2.right * h * speed);

        if (Mathf.Abs(rb2d.velocity.x) > speed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * speed, rb2d.velocity.y);

        IsWalking = true;
        IsIdle = false;



        if (IsGrounded || IsUnderLiquid)
            anims.Play("walk");
    }

    public void MoveRight()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        var h = 1;
        var speed = Stats.GetSpeed();

        if (h * Mathf.Abs(rb2d.velocity.x) < speed)
            rb2d.AddForce(Vector2.right * h * speed);

        if (Mathf.Abs(rb2d.velocity.x) > speed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * speed, rb2d.velocity.y);

        IsWalking = true;
        IsIdle = false;

        if (IsGrounded || IsUnderLiquid)
            anims.Play("walk");
    }

    public void MoveUp()
    {
        // Detect collision with ladder and decide if just move up or jump
        if (IsLaddered)
        {
            IsShielded = false;

            var h = 1;
            var speed = Stats.GetClimbSpeed();
            rb2d.velocity = Vector2.zero;
            rb2d.MovePosition(new Vector2(rb2d.position.x, rb2d.position.y + (h * speed)));
            anims.Play("climb");
        }
        else
        {
            if (!IsGrounded || IsJumping || Mathf.Abs(rb2d.velocity.y) > 0.1f) return;

            LastJump = Time.time;

            var jumpSpeed = Stats.GetJumpSpeed();
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            IsJumping = true;
            IsGrounded = false;
            anims.Play("jump");
        }

    }

    public void MoveDown()
    {
        if (IsLaddered)
        {
            IsShielded = false;

            var h = -1;
            var speed = Stats.GetClimbSpeed();
            rb2d.velocity = Vector2.zero;
            rb2d.MovePosition(new Vector2(rb2d.position.x, rb2d.position.y + (h * speed)));
            anims.Play("climb");
        }
    }

    public void UseWeapon()
    {
        if (Time.time - LastWeaponUsedTime < RestrictWeaponUsagePerSecond) return;
        if (Inventory == null || Inventory.MainWeapon == null || Inventory.MainWeapon.WeaponQuantity == 0) return;

        IsShielded = false;
        var lastWeaponUsedTime = Time.time;
        var weapon = Inventory.MainWeapon;

        if (weapon) weapon.Use(gameObject);

        anims.Play("attack");
    }

    // Could mean climb ladder or jump
    public void UseShield()
    {
        if (IsShielded || Inventory == null ||
           Inventory.MainShield == null || Inventory.MainShield.WeaponDurability == 0) return;

        IsShielded = true;

        var shield = Inventory.MainShield;
        if (shield) shield.Use(gameObject);

        anims.Play("defend");
    }

    void Update()
    {
        if (IsDead) return;
        UpdateLookingObject();
        CheckGrounded();
    }

    void UpdateLookingObject()
    {
        int mask = 1 << LayerMask.NameToLayer("WALL");

        var isEnemy = GetComponent<Enemy>() != null;
        if (isEnemy) mask = mask | (1 << LayerMask.NameToLayer("PLAYER"));
        else mask = mask | (1 << LayerMask.NameToLayer("OBJ"));

        LookingObject = Helper.RaycastHorizontal(transform, col, mask);
    }

    void CheckGrounded()
    {
        var groundTag = Helper.RaycastDown(transform, col);
        if (groundTag == "GroundWall")
        {
            IsGrounded = true;
            IsLaddered = false;
            IsJumping = false;
            IsFlying = false;
            rb2d.gravityScale = 1f;
        }
        else if (groundTag == "Ladder")
        {
            rb2d.gravityScale = 0f;
        }
        else
        {
            rb2d.gravityScale = 1f;
            IsGrounded = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other == null || other.gameObject.layer == LayerMask.NameToLayer("WALL")) return;
        IsLaddered = other.transform.CompareTag("Ladder");
        IsUnderLiquid = other.transform.CompareTag("Water");

        if (IsLaddered || IsUnderLiquid)
        {
            rb2d.gravityScale = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsLaddered) IsLaddered = false;
        if (IsUnderLiquid) IsUnderLiquid = false;
    }

}