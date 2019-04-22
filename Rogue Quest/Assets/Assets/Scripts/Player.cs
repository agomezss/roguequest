using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BehaviourState state;

    // Start is called before the first frame update
    void Awake()
    {
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
        else if (vertical < 0f)
        {
            state.MoveDown();
        }

        if (SimpleInput.GetButton("Fire1"))
        {
            state.UseWeapon();
        }
        else if (SimpleInput.GetButton("Fire2"))
        {
            state.UseShield();
        }
        
        if (SimpleInput.GetButton("Jump"))
        {
            GameManager.S.PauseUnpause();
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.S.PauseUnpause();
            }
        }
    }
}
