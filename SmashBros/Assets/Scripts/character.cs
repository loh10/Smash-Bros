using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class character : MonoBehaviour
{
    Animator anim;
    private string currentState;

    private expulsionPercentage _expulsionPercentage;
    private Rigidbody2D rb;
    private int powerOffset = 0;

    private bool isGrounded = false;
    private int remainingJumps;
    private bool isJumping = false;
    private float currentJumpTime = 0;
    private bool isMoving = false;
    private Vector3 moveDirection = new Vector3();

    private Vector2 lastMoveValue;
    public float raycastDistance;

    public float simpleAttackDuration = 0.3f;
    public float bigAttackDuration = 0.7f;

    GameObject playerTouch;
    bool canattack;

    private bool isSimpleAttack = false;
    private bool isBigAttack = false;
    private bool isJumpAttack = false;
    private float lastHitTime = -2f;
    [Header("pourcentage perso")]
    int index;
    Text percent;
    ViePlayer vp;
    [SerializeField] Collider2D coll;
    private enum characterState
{
    Void,
    Jumping,
    Attacking,
    Stun,
    Ejection
}
    
    
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();
        vp = GetComponent<ViePlayer>();
        index = GameObject.Find("EventManager").GetComponent<MultiplayerManager>().playerIndex;
        _expulsionPercentage = GetComponent<expulsionPercentage>();
        percent = GameObject.Find("P" + index + " %").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        this.gameObject.name = "player "+index.ToString();
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
        int hitPercentage = 0;
        switch (_powerOffset)
        {
            default:
                hitPercentage = 25;
                break;

            case 1:
                hitPercentage = 50;
                break;

            case 2:
                hitPercentage = 13*5;
                break;
        }
        _expulsionPercentage.addCharacterExpulsionPercentage(hitPercentage);
        DisplayPercent(_expulsionPercentage.getCharacterExpulsionPercentage()/5);
        addForceByhit(hitPercentage, hitDirection);
        anim.SetTrigger("Damaged");
        
    }
    public void DisplayPercent(float Addnb)
    {
        percent.text = ((int)(Addnb)).ToString() + " %";
    }
    private void addForceByhit(int _powerOffset, Vector3 hitDirection)
    {
        rb.AddForce(hitDirection * ((_powerOffset * _expulsionPercentage.getCharacterExpulsionPercentage())/2), ForceMode2D.Force);
    }

    public void move(Vector2 inputAxis)
    {
        moveDirection = Vector2.right;
        lastMoveValue = inputAxis + new Vector2(inputAxis.x * 10f,1 + inputAxis.y * 10f);
        if(inputAxis.x > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector2(0,0);
        }
        else
        {
            this.gameObject.transform.eulerAngles = new Vector2(0,180);        
        }
        isMoving = true;
        anim.SetBool ("Run", true);
    }

    public void stopMove()
    {
        isMoving = false;
        anim.SetBool ("Run", false);
    }


    private void Update()
    {
        if(vp.ejected)
        {
            DisplayPercent(0);
            _expulsionPercentage.setCharacterExpulsionPercentage(0);
            vp.ejected = false;
        }
        if (isJumping)
        {
            if (currentJumpTime < 0.5f)
            {
                currentJumpTime += Time.deltaTime;
                rb.AddForce(Vector2.up * 200);

            }


        }
            
        if ((remainingJumps == 0))
        {
                if (checkIfGrounded())
                {
                    isJumping = false;
                    resetJump();
                }
        }
        
        
        if (isMoving)
        {
                transform.Translate((moveDirection * Time.deltaTime * 10));
        }

            
        if (isSimpleAttack)   
        {
                checkArmsCollisions();
                endAttack();
        }

            
        if (isBigAttack)
        {
                moveArms();
                checkArmsCollisions();
                endAttack();
        }

           
        if (isJumpAttack)
            {
                if (checkIfGrounded())
                {
                    isJumpAttack = false;
                    // bodyCollider.enabled = false;
                }
                else
                {
                    rb.AddForce(Vector2.down * 2000);
                    Collider2D leftHit = Physics2D.OverlapBox(this.transform.position, new Vector2(4, 4), LayerMask.GetMask("Player"));
                    if (leftHit.transform.gameObject.GetComponent<character>() != null)
                    {
                        if (leftHit.transform.gameObject != this.gameObject)
                        {
                            isJumpAttack = false;
                            sendHit(leftHit.transform.gameObject.GetComponent<character>(), Vector2.up * 5);
                        }
                    }
                }
            
        }
        animationInJump();
    }
   public void Jump()
   {
    if (remainingJumps > 0)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * 2000, ForceMode2D.Impulse);
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
    if (!isSimpleAttack && !isBigAttack && !isJumpAttack && checkIfGrounded())
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
                    anim.SetTrigger("TP");
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

    public bool checkIfGrounded()
    {
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
        if (canattack)
        {
            sendHit(playerTouch.GetComponent<character>(), lastMoveValue);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canattack = true;
            playerTouch = collision.gameObject;
        }
        else
        {
            canattack = false;
            playerTouch = null;
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
    }
    public void simpleAttack()
    {
             if(!isSimpleAttack && !isBigAttack && !isJumpAttack)
             {
                isSimpleAttack = true;
                powerOffset = 0;
                anim.SetTrigger("Light_Attack");
             }

    }

    public void bigAttack()
    {
        if(checkIfGrounded())
        {
            if (!isSimpleAttack && !isBigAttack && !isJumpAttack)
            {
                isBigAttack = true;
                powerOffset = 1;
                anim.SetTrigger("Big_Attack");
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
        powerOffset = 2;
        rb.AddForce(Vector2.down * 1500, ForceMode2D.Impulse);
        anim.SetBool("Down_Charge", true);
    }


    void moveArms()
    {
        //faire animation
    }


    private void animationInJump()
    {
        if (!coll.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Down_Charge", false);
            return;
        }
    }

}
