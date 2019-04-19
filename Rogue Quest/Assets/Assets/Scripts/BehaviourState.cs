using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourState : MonoBehaviour
{
    private AnimedTile anims;
    private Stats Stats;
    private Inventory Inventory;
    private Rigidbody2D rb2d;

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
    private bool IsShielded;
    private bool IsUsingWeapon;
    private float LastWeaponUsedTime = 0f;
    private float RestrictWeaponUsagePerSecond = 0.4f;

    private bool IsLaddered;
    private bool IsUnderLiquid;
    public bool IsFalling;


    // Start is called before the first frame update
    void Awake()
    {
        anims = GetComponent<AnimedTile>();
        Stats = GetComponent<Stats>();
        Inventory = GetComponent<Inventory>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void GetIdle()
    {
        rb2d.velocity = Vector2.Lerp(rb2d.velocity, new Vector2(0f, rb2d.velocity.y), 0.1f * Time.deltaTime);

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
        IsIdle = false;

        var h = 1;
        var speed = Stats.GetClimbSpeed();

        rb2d.velocity = new Vector2(rb2d.velocity.x, h * speed);

        // if (h * rb2d.velocity.x < speed)
        //     rb2d.AddForce(Vector2.up * h * speed);
        IsWalking = true;
    }

    public void FlyDown()
    {
        IsIdle = false;

        var h = -1;
        var speed = Stats.GetClimbSpeed();

        rb2d.velocity = new Vector2(rb2d.velocity.x, h * speed);

        // if (h * rb2d.velocity.x < speed)
        //     rb2d.AddForce(Vector2.up * h * speed);

        IsWalking = true;
    }

    public void MoveLeft()
    {
        var h = -1;
        var speed = Stats.GetSpeed();

        //rb2d.velocity = h * speed * Vector2.right;
        if (h * Mathf.Abs(rb2d.velocity.x) < speed) // MaxSpeed
            rb2d.AddForce(Vector2.right * h * speed); // MoveForce

        if (Mathf.Abs(rb2d.velocity.x) > speed) // MaxSpeed
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * speed, rb2d.velocity.y); // MaxSpeed

        IsWalking = true;
        IsIdle = false;
    }

    public void MoveRight()
    {
        var h = 1;
        var speed = Stats.GetSpeed();

        //rb2d.velocity = h * speed * Vector2.right;
        if (h * Mathf.Abs(rb2d.velocity.x) < speed)
            rb2d.AddForce(Vector2.right * h * speed);

        if (Mathf.Abs(rb2d.velocity.x) > speed) // MaxSpeed
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * speed, rb2d.velocity.y); // MaxSpeed

        IsWalking = true;
        IsIdle = false;
    }

    // Could mean climb ladder or jump
    public void MoveUp()
    {
        if (IsLaddered)
        {
            // Detect collision with ladder and decide if just move up or jump
            // If climb then unshield
            IsShielded = false;

            var h = 1;
            var speed = Stats.GetClimbSpeed();

            rb2d.velocity = new Vector2(rb2d.velocity.x, h * speed);

            // if (h * rb2d.velocity.x < speed)
            //     rb2d.AddForce(Vector2.up * h * speed);
        }
        else
        {
            // If Grounded and not jumping then jump
            if (!IsGrounded || IsJumping) return;

            var jumpSpeed = Stats.GetJumpSpeed();
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            IsJumping = true;
            IsGrounded = false;
        }

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
        CheckGrounded();
    }

    void CheckGrounded()
    {
        //Debug.DrawLine(new Vector2(transform.position.x, transform.position.y - 0.51f), new Vector2(transform.position.x, transform.position.y - 0.55f));
        var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.51f), new Vector2(transform.position.x, transform.position.y - 0.55f));

        if (hit != null && hit.transform != null)
        {
            if (hit.transform.tag == "GroundWall" && rb2d.velocity.y == 0f)
            {
                IsGrounded = true;
                IsJumping = false;
                IsFalling = false;
            }
            else
            {
                IsFalling = true;
            }
        }
    }

    void ClampSpeed()
    {
        var speed = Stats.GetClimbSpeed();

        if (Mathf.Abs(rb2d.velocity.x) > speed) // MaxSpeed
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * speed, rb2d.velocity.y); // MaxSpeed
    }


    // If Collides with floor and jumpforce = 0 then IsGrounded = true
    void OnCollisionStay2D(Collision2D other)
    {
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