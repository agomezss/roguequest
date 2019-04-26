using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(1, 10)]
    public int MoveFrequency = 2;

    [Range(0, 10)]
    public int MoveStepMaxRange = 3;

    [Range(0, 10)]
    public int MoveRandomness = 5;

    private float MoveLastTime = 0f;
    private bool IsMoving;


    public float JumpFrequency = 0f;

    [Range(0, 10)]
    public int JumpRandomness = 5;
    private float JumpLastTime = 0f;

    public bool AvoidFall = true;
    public bool AvoidWalls = true;


    [Range(0, 10)]
    public int AttackWhenSeenTargetRandomness = 0;
    [Range(1f, 10f)]
    public float AttackTargetSightDistance = 2f;
    private float AttackWhenSeenTargetLastTime = 0f;


    [Range(0f, 10f)]
    public float LongDistanceSight = 2f;
    public string LongDistanceTargetSeen;
    private float LongDistanceTargetSeenTime = 0f;


    public float RandomAttackFrequency = 2f;

    [Range(0, 10)]
    public int RandomAttackRandomness = 5;
    private float RandomAttackLastTime = 0f;


    public float DefendFrequency = 0f;

    [Range(0, 10)]
    public int DefendRandomness = 5;
    private float DefendLastTime = 0f;


    public float DefendProjectilesFrequency = 0f;

    [Range(0f, 10f)]
    public float DefendProjectilesRandonmess = 5f;
    private float DefendProjectilesLastTime = 0f;


    private BehaviourState state;
    private Inventory inventory;
    private BoxCollider2D col;

    private float horizontal;
    private float vertical;
    private bool jump;
    private bool fire1;
    private bool fire2;

    void Awake()
    {
        state = GetComponent<BehaviourState>();
        inventory = GetComponent<Inventory>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        InputSimulation();
        InputDetection();
    }

    private void InputSimulation()
    {
        Jump();
        Move();
        Look();
        Attack();
        Defend();
    }

    private void Look()
    {
        if (Time.time - LongDistanceTargetSeenTime < 0.1f) return;

        int mask = 1 << LayerMask.NameToLayer("WALL");
        mask = mask | (1 << LayerMask.NameToLayer("PLAYER"));

        LongDistanceTargetSeen = Helper.RaycastHorizontal(transform, col, mask, LongDistanceSight);
    }

    private bool Randomness(int favorable)
    {
        var multiplier = 1;
        var rand = UnityEngine.Random.Range(0, 10 * multiplier);
        return rand >= favorable * multiplier;
    }

    private void Move()
    {
        if (Time.time - MoveLastTime < MoveFrequency || IsMoving) return;
        StartCoroutine(MoveAsync());
    }

    IEnumerator MoveAsync()
    {
        MoveLastTime = Time.time;

        if (!Randomness(MoveRandomness)) yield break;

        var hrand = UnityEngine.Random.Range(0, 1000) < 500 ? -1 : 1;

        var totalSteps = UnityEngine.Random.Range(1, MoveStepMaxRange);

        for (int i = 0; i < totalSteps; i++)
        {
            if (CheckNotFall(hrand) && CheckNotBounceWall())
            {
                horizontal = hrand;
            }
            else
            {
                horizontal = 0;
            }

            yield return new WaitForSeconds(0.5f);
        }

        horizontal = 0;
        IsMoving = false;
    }

    private bool CheckNotBounceWall()
    {
        if (AvoidWalls) return true;
        return state.LookingObject != "GroundWall";
    }

    private bool CheckNotFall(int h)
    {
        if (!AvoidFall) return true;
        return Helper.RaycastDiagonal(transform, col, h) == "GroundWall";
    }

    private void Jump()
    {
        if (Time.time - JumpLastTime < JumpFrequency || jump || JumpFrequency == 0) return;
        StartCoroutine(JumpAsync());
    }

    IEnumerator JumpAsync()
    {
        JumpLastTime = Time.time;

        if (!Randomness(JumpRandomness)) yield break;

        jump = true;

        yield return new WaitForSeconds(1f);

        jump = false;
    }


    private void Attack()
    {
        if (Time.time - RandomAttackLastTime < RandomAttackFrequency || RandomAttackFrequency == 0) return;
        StartCoroutine(AttackAsync());
    }

    IEnumerator AttackAsync()
    {
        RandomAttackLastTime = Time.time;

        if (!Randomness(RandomAttackRandomness)) yield break;

        fire1 = true;

        yield return new WaitForSeconds(0.5f);

        fire1 = false;
    }

    private void Defend()
    {
        if (Time.time - DefendLastTime < DefendFrequency || RandomAttackFrequency == 0) return;

        // TODO: Check if being attacked
        // TODO: Prevent Projectiles

        //StartCoroutine(DefendAsync());
    }

     IEnumerator DefendAsync()
    {
        DefendLastTime = Time.time;

        if (!Randomness(DefendRandomness)) yield break;

        fire2 = true;

        yield return new WaitForSeconds(0.5f);

        fire2 = false;
    }

    private void InputDetection()
    {
        if (horizontal < 0f)
        {
            state.HideShield();
            state.MoveLeft();
        }
        else if (horizontal > 0f)
        {
            state.HideShield();
            state.MoveRight();
        }
        else if (horizontal == 0f)
        {
            state.GetIdle();
        }

        if (jump || vertical > 0f)
        {
            state.MoveUp();
            jump = false;
        }
        else if (vertical < 0f)
        {
            state.MoveDown();
        }

        if (fire1)
        {
            state.HideShield();
            state.UseWeapon();
            fire1 = false;
        }
        else
        {
            if (fire2)
            {
                state.UseShield();
            }
            else if (!fire2)
            {
                state.HideShield();
            }
        }
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.transform.CompareTag("Object"))
    //     {
    //         // if (!DoorCheck(other))
    //         // {
    //         //     ChestCheck(other);
    //         // }
    //     }
    // }

    // private bool DoorCheck(Collision2D other)
    // {
    //     var door = other.gameObject.GetComponent<Door>();
    //     if (door)
    //     {
    //         var simpleLock = door.TryOpen();
    //         if (simpleLock) return true;

    //         var requiredKey = inventory.SearchKey(door.RequiredTypedKey, door.SpecificKeyName);
    //         if (requiredKey != null)
    //         {
    //             return door.TryOpen(requiredKey);
    //         }
    //     }

    //     return false;
    // }
}
