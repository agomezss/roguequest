using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimedTile : MonoBehaviour
{
    public float BaseFps = 60f;
    private SpriteRenderer _renderer;

    public bool canIdle;
    public bool canWalk;
    public bool canAttack;
    public bool canJump;
    public bool canClimb;
    public bool canDead;
    public bool canAware;
    public bool canDefend;

    public Sprite[] Idle;
    public float IdleSpeed;
    public bool IdleRepeat = true;
    private float IdleCurrentFrame;

    public Sprite[] Walking;
    public float WalkingSpeed;
    public bool WalkingRepeat = true;
    private float WalkingCurrentFrame;

    public Sprite[] Attacking;
    public float AttackingSpeed;
    public bool AttackingRepeat = true;
    private float AttackingCurrentFrame;

    public Sprite[] Jumping;
    public float JumpingSpeed;
    private float JumpingCurrentFrame;

    public Sprite[] Climbing;
    public float ClimbingSpeed;
    private float ClimbingCurrentFrame;

    public Sprite Dead;
    public float DeadDuration;

    public Sprite[] Aware;
    public float AwareSpeed;

    public Sprite Defending;
    public float DefendingDuration;      


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
