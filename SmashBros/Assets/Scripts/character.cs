using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class character : MonoBehaviour
{
    private expulsionPercentage _expulsionPercentage;
    private Rigidbody2D rb;
    private int powerOffset = 1;

   // private bool checkIfGrounded;
    private int remainingJumps;
    private bool isJumping = false;
    private float currentJumpTime = 0;
    private bool isMoving = false;
    private Vector3 moveDirection = new Vector3();
    private float elapsedTime = 0f;

    private Vector2 lastMoveValue;

    public BoxCollider2D leftArm;
    public BoxCollider2D rightArm;

    public float simpleAttackDuration = 0.3f;
    public float bigAttackDuration = 0.7f;

    private bool isSimpleAttack = false;
    private bool isBigAttack = false;
    private bool isJumpAttack = false;
    private float lastHitTime = -2f;

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
        if(ennemieHitted != null)
        {
        ennemieHitted.hitReceveid(powerOffset, hitDirection);
        }

    }

    public void hitReceveid(int _powerOffset, Vector3 hitDirection)
    {
        float currentTimer = Time.time;
        if (currentTimer - lastHitTime >= 0.1f)
        {
            float hitPercentage = 0;
            switch (_powerOffset)
            {
                default:
                hitPercentage = UnityEngine.Random.Range(0.5f,1f);
                break;

                case 1:
                hitPercentage = UnityEngine.Random.Range(0.5f,1f);
                break;

                case 2:
                hitPercentage = UnityEngine.Random.Range(1.5f,3f);
                break;
            }


            _expulsionPercentage.addCharacterExpulsionPercentage(hitPercentage);
            addForceByhit(_powerOffset, hitDirection);
            lastHitTime = currentTimer;
        }
    }

    private void addForceByhit(int _powerOffset, Vector3 hitDirection)
    {
        rb.AddForce(hitDirection * (((_powerOffset * (_expulsionPercentage.getCharacterExpulsionPercentage()*8)))), ForceMode2D.Force);
    }

    public void move(Vector2 inputAxis)
    {
        moveDirection = Vector2.right;
        lastMoveValue = inputAxis + new Vector2(inputAxis.x * 5,1 + inputAxis.y * 5);
        if(inputAxis.x > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector2(0,0);
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector2(0,180);        
        }
        isMoving = true;
    }

    public void stopMove()
    {
        isMoving = false;
    }

    private void FixedUpdate()
    {
        if(isJumping)
        {
            if(currentJumpTime < 0.5f)
            {
            currentJumpTime += Time.deltaTime;
            rb.AddForce(Vector2.up * 500);
            }
        }
        
  
        if((remainingJumps == 0))
        {
            if (checkIfGrounded())
            {
                isJumping = false;
                resetJump();
            }
        }


        if(isMoving)
        {
            transform.Translate((moveDirection * Time.deltaTime * 10));
        }
        
        if(isSimpleAttack)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < simpleAttackDuration)
            {
                moveArms();
                checkArmsCollisions();
            }
            else
            {
                endAttack();
            }
        }

        if(isBigAttack)
        {
            elapsedTime += Time.deltaTime;
             if (elapsedTime < bigAttackDuration)
            {
                moveArms();
                checkArmsCollisions();
            }
            else
            {
                endAttack();
            }
        }

        if(isJumpAttack)
        {
            if(checkIfGrounded())
            {
                isJumpAttack = false;
               // bodyCollider.enabled = false;
            }
            else
            {
                rb.AddForce(Vector2.down * 500);
                Collider2D leftHit = Physics2D.OverlapBox(this.transform.position, new Vector2(4,4), LayerMask.GetMask("Player"));
                if (leftHit.transform.gameObject.GetComponent<character>() != null)
                {
                    if(leftHit.transform.gameObject != this.gameObject)
                    {
                       isJumpAttack = false;
                       sendHit(leftHit.transform.gameObject.GetComponent<character>(), Vector2.up * 5);
                    }
                }
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

   public void swapPosition()
{
    if (!isSimpleAttack && !isBigAttack && !isJumpAttack)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            GameObject closestPlayer = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject player in players)
            {
                if (player != this.gameObject)
                {
                    float distance = Vector2.Distance( this.gameObject.transform.position, player.transform.position);

                    if (distance < closestDistance)
                    {
                        closestPlayer = player;
                        closestDistance = distance;
                    }
                }
            }

            if (closestPlayer != null)
            {
                Vector2 positionBuffer =  this.gameObject.transform.position;
                this.gameObject.transform.position = closestPlayer.transform.position;
                closestPlayer.transform.position = positionBuffer;
                Debug.Log("Téléportation effectuée");
            }
            else
            {
                //Debug.LogWarning("pas assez de joueurs");
            }
        }
        else
        {
           //Debug.LogWarning("pas assez de joueurs");
        }
    }
}

    private bool checkIfGrounded()
    {
        float raycastDistance = 1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }


    private void resetJump()
    {
        currentJumpTime = 0;
        remainingJumps = 2;
    }

    void checkArmsCollisions()
    {
        Collider2D leftHit = Physics2D.OverlapBox(leftArm.transform.position, leftArm.size, LayerMask.GetMask("Player"));    
        Collider2D rightHit = Physics2D.OverlapBox(rightArm.transform.position, rightArm.size, LayerMask.GetMask("Player"));
        if (leftHit != null || rightHit != null)
        {
            if(leftHit.gameObject != this.gameObject)
            {
                sendHit(leftHit.gameObject.GetComponent<character>(), lastMoveValue);
            }
            else if(rightHit.gameObject != this.gameObject)
            {
                sendHit(rightHit.gameObject.GetComponent<character>(), lastMoveValue);
            }
            
        }
    }

    void resetArmPositions()
    {
        //faudra reset la position des bras si on garde ce systeme
       // leftArm.position = 
        //rightArm.position = 
    }
    void endAttack()
    {
        isSimpleAttack = false;
        isBigAttack = false;
        isJumpAttack = false;
        elapsedTime = 0f;
        flipFlopArmsColliders(false);
    }
    public void simpleAttack()
    {
             if(!isSimpleAttack && !isBigAttack && !isJumpAttack)
            {
                isSimpleAttack = true;
                elapsedTime = 0f;
                powerOffset = 1;
                flipFlopArmsColliders(true);
            }
    }

    public void bigAttack()
    {
        if(checkIfGrounded())
        {
             if(!isSimpleAttack && !isBigAttack && !isJumpAttack)
            {
                isBigAttack = true;
                elapsedTime = 0f;
                powerOffset = 2;
                flipFlopArmsColliders(true);
            }
        }
        else
        {
            if(!isSimpleAttack && !isBigAttack && !isJumpAttack)
            {
                jumpAttack();
            }
        }
    }

    public void jumpAttack()
    {
        isJumpAttack = true;
        elapsedTime = 0f;
        powerOffset = 2;
        rb.AddForce(Vector2.down * 1500, ForceMode2D.Impulse);
    }


    void moveArms()
    {
        //faire animation
    }

     void flipFlopArmsColliders(bool activate)
    {
        leftArm.enabled = activate;
        rightArm.enabled = activate;
    }

     void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(4,4, 0f));
    }

}
