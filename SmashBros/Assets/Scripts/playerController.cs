using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private character c;
    private bool jump;

    void Start()
    {
        c = GetComponent<character>();
    }
    public void onMove(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if(context.performed)
        {
            c.move(inputValue);
        }
        else if (context.canceled)
        {
            c.stopMove();
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            c.Jump();
        }
        else if (context.canceled)
        {
            c.stopJump();
        }
    }

    public void onWestButton(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            c.simpleAttack();
        }
    }

    public void onEastButton(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            c.bigAttack();
        }
    }

      
    public void onNorthButton(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            c.swapPosition();
        }
    }
}
