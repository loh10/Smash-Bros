using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;


public class character : MonoBehaviour
{
    private expulsionPercentage _expulsionPercentage;
    private Rigidbody2D rb;
    private int powerOffset = 1;

   // private bool checkIfGrounded;
    private int remainingJumps;
    private bool isJumping = false;
    private float currentJumpTime = 0;

    private enum characterState
{
    Void,
    Jumping,
    Attacking,
    Stun,
    Ejection
}

    void Start()
    {
        _expulsionPercentage = GetComponent<expulsionPercentage>();
        remainingJumps = 2;
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void sendHit(character ennemieHitted, Vector3 hitDirection)
    {
        ennemieHitted.hitReceveid(powerOffset, hitDirection);
    }

    public void hitReceveid(int _powerOffset, Vector3 hitDirection)
    {
        int hitPercentage = 0;
        switch (_powerOffset)
        {
            default:
            hitPercentage = Random.Range(1,3);
            break;

            case 1:
            hitPercentage = Random.Range(1,3);
            break;

            case 2:
            hitPercentage = Random.Range(4,7);
            break;
        }


        _expulsionPercentage.addCharacterExpulsionPercentage(hitPercentage);
        addForceByhit(_powerOffset, hitDirection);
    }

    private void addForceByhit(int _powerOffset, Vector3 hitDirection)
    {
        rb.AddForce(hitDirection * (((_powerOffset * _expulsionPercentage.getCharacterExpulsionPercentage()))), ForceMode2D.Force);
        Debug.Log(((_powerOffset * _expulsionPercentage.getCharacterExpulsionPercentage()) / 10));
    }

    public void move()
    {
         Debug.Log("Move");
    }

    private void Update()
    {
        if(isJumping)
        {
            if(currentJumpTime < 0.5f)
            {
            currentJumpTime += Time.deltaTime;
            rb.AddForce(Vector2.up * 500, ForceMode2D.Force);
            }
        }

        if((rb.velocity.y != 0 && currentJumpTime > 0.15f) || (remainingJumps == 0))
        {
            if (checkIfGrounded())
            {
                isJumping = false;
                resetJump();
            }
        }
    }

   public void Jump()
{
    if (remainingJumps > 0)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * 500, ForceMode2D.Impulse);
        remainingJumps--;
        isJumping = true;
    }
}

    public void stopJump()
    {
       isJumping = false;
       currentJumpTime = 0;
    }

    private bool checkIfGrounded()
    {
        float raycastDistance = 1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }


    private void resetJump()
    {
            Debug.Log("reset jump");
            currentJumpTime = 0;
            remainingJumps = 2;
    }
}
