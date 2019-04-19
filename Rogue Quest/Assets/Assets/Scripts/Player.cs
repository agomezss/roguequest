using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Stats Stats;
    public Inventory Inventory;
    public AnimedTile anims;
    public Rigidbody2D rb;
    public BoxCollider2D col;
    public BehaviourState state;

    // Start is called before the first frame update
    void Awake()
    {
        Stats = GetComponent<Stats>();
        anims = GetComponent<AnimedTile>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        state = GetComponent<BehaviourState>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDetection();
    }

    private void InputDetection()
    {
        var horizontal = SimpleInput.GetAxis("Horizontal");

        if (horizontal < 0f)
        {
            state.MoveLeft();
        }
        else if (horizontal > 0f)
        {
            state.MoveRight();
        }
        else if (horizontal == 0f)
        {
            state.GetIdle();
        }

        var vertical = SimpleInput.GetAxis("Vertical");

        if (SimpleInput.GetButton("Jump") || vertical > 0f)
        {
            state.MoveUp();
        }
    }
}
