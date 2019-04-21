using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimedTile : MonoBehaviour
{
    private float BaseFps = 60f;
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
    public float IdleSpeed = 1f;
    public bool IdleRepeat = true;

    public Sprite[] Walking;
    public float WalkingSpeed = 1f;
    public bool WalkingRepeat = true;

    public Sprite[] Attacking;
    public float AttackingSpeed = 1f;
    public bool AttackingRepeat = true;

    public Sprite[] Jumping;
    public float JumpingSpeed = 1f;
    public bool JumpingRepeat = false;

    public Sprite[] Climbing;
    public float ClimbingSpeed = 1f;
    public bool ClimbingRepeat = true;

    public Sprite[] Dead;
    public float DeadSpeed = 1f;
    public bool DeadRepeat = true;

    public Sprite[] Aware;
    public float AwareSpeed = 1f;
    public bool AwareRepeat = true;

    public Sprite[] Defending;
    public float DefendingSpeed = 1f;
    public bool DefendingRepeat = true;

    public string currentAnimation;
    private string nextAnimation;
    private int currentFrame;
    private float currentSpeed;
    private Sprite[] currentSprites;
    private int currentSpritePlaying;
    private int currentSpriteFrame;
    private bool currentRepeat;
    private float nextAnimationTime = 0f;
    private float nextAnimationTimePeriod = 0.01f;
    private float totalFramesPerSprite;

    public void Play(string animation, string animationFinish = null)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            nextAnimation = animationFinish;

            switch (animation.ToLower())
            {
                case "walk":
                    if (!canWalk) return;
                    Animate(Walking, WalkingSpeed, WalkingRepeat);
                    break;
                case "attack":
                    if (!canAttack) return;
                    Animate(Attacking, AttackingSpeed, AttackingRepeat);
                    break;
                case "jump":
                    if (!canJump) return;
                    Animate(Jumping, JumpingSpeed, JumpingRepeat);
                    break;
                case "climb":
                    if (!canClimb) return;
                    Animate(Climbing, ClimbingSpeed, ClimbingRepeat);
                    break;
                case "dead":
                    if (!canDead) return;
                    Animate(Dead, DeadSpeed, DeadRepeat);
                    break;
                case "aware":
                    if (!canAware) return;
                    Animate(Aware, AwareSpeed, AwareRepeat);
                    break;
                case "defend":
                    if (!canDefend) return;
                    Animate(Defending, DefendingSpeed, DefendingRepeat);
                    break;
                case "idle":
                default:
                    if (!canIdle) return;
                    Animate(Idle, IdleSpeed, IdleRepeat);
                    break;
            }
        }
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Animate(Sprite[] sprites, float speed, bool repeat)
    {
        currentFrame = 0;
        currentSpritePlaying = 0;
        currentSprites = sprites;
        currentSpeed = speed;
        currentRepeat = repeat;
    }

    void Update()
    {
        if (Time.time < nextAnimationTime || currentSprites == null) return;
        nextAnimationTime = Time.time + nextAnimationTimePeriod;

        if (currentFrame >= BaseFps)
        {
            if (!currentRepeat && !string.IsNullOrEmpty(nextAnimation))
            {
                Play(nextAnimation, null);
                return;
            }
            else
            {
                currentFrame = 0;
            }
        }

        totalFramesPerSprite = (BaseFps / currentSprites.Length) * (1 / (currentSpeed == 0 ? 1f : currentSpeed));

        if (currentSpriteFrame > totalFramesPerSprite)
        {
            currentSpriteFrame = 0;

            if(currentSprites.Length < 2) return;

            currentSpritePlaying++;

            if (currentSpritePlaying + 1 > currentSprites.Length)
                currentSpritePlaying = 0;
        }

        if(currentSprites != null && currentSprites.Length >= currentSpritePlaying+1)
        _renderer.sprite = currentSprites[currentSpritePlaying];

        currentSpriteFrame++;
        currentFrame++;
    }
}
