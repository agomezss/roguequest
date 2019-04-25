using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BehaviourState state;
    private Inventory inventory;

    void Awake()
    {
        state = GetComponent<BehaviourState>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        InputDetection();
    }

    private void InputDetection()
    {
        var horizontal = SimpleInput.GetAxis("Horizontal");

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
            state.HideShield();
            state.UseWeapon();
        }
        else
        {
            if (SimpleInput.GetButtonDown("Fire2"))
            {
                state.UseShield();
            }
            else if (SimpleInput.GetButtonUp("Fire2"))
            {
                state.HideShield();
            }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Object"))
        {
            if (!DoorCheck(other))
            {
                ChestCheck(other);
            }
        }
    }

    private bool ChestCheck(Collision2D other)
    {
        var chest = other.gameObject.GetComponent<Chest>();
        if (chest)
        {
            var simpleLock = chest.TryOpen(null, gameObject);
            if (simpleLock) return true;

            var requiredKey = inventory.SearchKey(chest.RequiredTypedKey, chest.SpecificKeyName);
            if (requiredKey != null)
            {
                chest.TryOpen(requiredKey, gameObject);
            }
        }

        return false;
    }

    private bool DoorCheck(Collision2D other)
    {
        var door = other.gameObject.GetComponent<Door>();
        if (door)
        {
            var simpleLock = door.TryOpen();
            if (simpleLock) return true;

            var requiredKey = inventory.SearchKey(door.RequiredTypedKey, door.SpecificKeyName);
            if (requiredKey != null)
            {
                return door.TryOpen(requiredKey);
            }
        }

        return false;
    }
}
