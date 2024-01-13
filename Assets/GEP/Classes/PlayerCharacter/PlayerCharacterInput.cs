using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterInput : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool invOpen;
    public GameObject inventory;
    public GameObject Slots;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnToggleInventory(InputValue value)
    {
        if (inventory.activeInHierarchy)
        {
            inventory.SetActive(false);

            Cursor.lockState = false ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            inventory.SetActive(true);

            Cursor.lockState = true ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = true;
        }

    }

    private void OnDropOne()
    {
        if(inventory.activeInHierarchy) 
        { 
            for(int i = 0; i < Slots.transform.childCount; i++) 
            {
                Debug.Log(i);
                if (Slots.transform.GetChild(i).GetComponent<Slot>().returnHovered())
                {                 

                    Slots.transform.GetChild(i).GetComponent<Slot>().Drop(false);
                }
            }
        }
    }

    private void OnDropStack()
    {

        if (inventory.activeInHierarchy)
        {
            for (int i = 0; i < Slots.transform.childCount; i++)
            {
                if (Slots.transform.GetChild(i).GetComponent<Slot>().returnHovered())
                {
                    Slots.transform.GetChild(i).GetComponent<Slot>().Drop(true);
                }
            }
        }
    }


    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
