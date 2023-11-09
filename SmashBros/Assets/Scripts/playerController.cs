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
        c.move();
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
}
