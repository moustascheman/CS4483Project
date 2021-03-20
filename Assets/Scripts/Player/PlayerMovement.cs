using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField]
    private BoxCollider2D groundBox;

    [SerializeField]
    private Rigidbody2D rb;
    
    //Editor Settings
    //Should be set and initialized in the editor
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float gravityValue;
    [SerializeField]
    private float initialJumpVelocity;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float terminalVelocity;
    [SerializeField]
    private SpriteRenderer playerSprite;
    [SerializeField]
    private float minDashTime;
    [SerializeField]
    private float maxDashTime;
    [SerializeField]
    private float dashXVelModifier;
    [SerializeField]
    private float dashJumpModifier;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float doubleJumpVel;


    public bool movementEnabled = true;
    public bool doubleJumpEnabled = true;
    private float jumpStatus;

    


    //True Private variables
    //Used to perform basic movement functions
    //Should not be modified in Editor
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float xInput;
    [SerializeField]
    private bool hasJumped = false;
    [SerializeField]
    private float yVelocity;
    [SerializeField]
    private bool facingRight = true;
    public bool IsDashing = false;
    private bool launchedJump = false;
    [SerializeField]
    private float DashTimer = 0;

    [SerializeField]
    private string currentState =  "idle";

    public bool updateAnimationAllowed = true;

    [SerializeField]
    private bool stunned = false;



    void FixedUpdate()
    {
        if (!stunned)
        {
            if (movementEnabled)
            {
                Move();
            }
            GroundCheck();
            ApplyGravity();
            updateDashTimer();
            UpdateAnimationState();
        }
    }

    public bool getGroundedState()
    {
        return isGrounded;
    }


    public void OnJump(InputValue input)
    {
        float status = input.Get<float>();
        
        jumpStatus = status;
        if (jumpStatus != 0)
        {
            if (!hasJumped && isGrounded)
            {
                yVelocity = IsDashing ? (initialJumpVelocity + dashJumpModifier) : initialJumpVelocity;
                isGrounded = false;
                hasJumped = true;
                launchedJump = true;
            }
        }
        else
        {
            if(yVelocity > 0)
            {
                yVelocity = 0;
            }
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 inVector = input.Get<Vector2>();
        xInput = inVector.x;
    }



    private void Move()
    {
        Vector2 yAmount = Vector2.zero;
        if (yVelocity > 0)
        {
            yAmount = Vector2.up * yVelocity * Time.deltaTime * jumpStatus;
            
        }
        else
        {
            yAmount = (Vector2.down * (-1 * yVelocity) * Time.deltaTime);
        }

        Vector2 moveAmount = Vector2.zero;
        if (IsDashing)
        {
            if(xInput == 0 && isGrounded)
            {
                Vector2 dir = facingRight ? Vector2.right : Vector2.left;
                moveAmount = dir * (moveSpeed + dashXVelModifier) * Time.deltaTime;
            }
            else if(xInput == 0 && !isGrounded)
            {
                moveAmount = Vector2.zero;
            }
            else if (xInput < 0)
            {
                moveAmount = Vector2.left * (moveSpeed + dashXVelModifier) * Time.deltaTime;
                if (facingRight)
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                    //playerSprite.flipX = true;
                    facingRight = false;
                }
            }
            else
            {
                moveAmount = Vector2.right * xInput * (moveSpeed +dashXVelModifier) * Time.deltaTime;
                if (!facingRight)
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                    //playerSprite.flipX = false;
                    facingRight = true;
                }
            }
        }
        else if (xInput != 0)
        {
            
            
            if (xInput < 0)
            {
                moveAmount = Vector2.left * moveSpeed * Time.deltaTime;
                if (facingRight)
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                    //playerSprite.flipX = true;
                    facingRight = false;
                }
            }
            else
            {
                moveAmount = Vector2.right * xInput * moveSpeed * Time.deltaTime;
                if (!facingRight)
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                    //playerSprite.flipX = false;
                    facingRight = true;
                }
            }

            
            
        }
        rb.MovePosition(rb.position + moveAmount + yAmount);

    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            float newVel = yVelocity - gravityValue;
            if(newVel < terminalVelocity)
            {
                newVel = terminalVelocity;
            }
            yVelocity = newVel;
        }
        
    }

    private void GroundCheck() {
        if (launchedJump)
        {
            launchedJump = false;
        }
        else
        {
            Collider2D col = Physics2D.OverlapBox(groundBox.bounds.center, groundBox.bounds.size, 0f, groundLayer);
            if (col)
            {
                //if you're touching the ground after a fall or jump, cancel the dash
                if (!isGrounded)
                {
                    yVelocity = 0;
                    IsDashing = false;
                   
                }
                isGrounded = true;
                hasJumped = false;
            }
            else
            {
                isGrounded = false;
            }
        }
    }


    public void OnDash(InputValue inp)
    {
        float inputValue = inp.Get<float>();
        if (!IsDashing)
        {
            if (isGrounded && inputValue > 0)
            {
                IsDashing = true;
                DashTimer = 0f;
            }
        }
        else
        {
            if(inputValue == 0)
            {
                if (isGrounded && DashTimer < minDashTime)
                {
                    float remTime = minDashTime - DashTimer;
                    DashTimer = maxDashTime - remTime;
                }
                else if (isGrounded)
                {
                    DashTimer = maxDashTime;
                }

            }
        }
    }

    public void endJumpEarly()
    {
        if (yVelocity > 0)
        {
            yVelocity = 0;
        }
    }

    private void updateDashTimer()
    {
        //In the air, ignore the dash timer (dash horizontal speed for full duration of jump/fall)
        if (isGrounded && IsDashing)
        {
            if(DashTimer >= maxDashTime)
            {
                IsDashing = false;
            }
            else
            {
                DashTimer++;
            }
        }
    }


    private void UpdateAnimationState()
    {
        if (updateAnimationAllowed)
        {
            if (isGrounded && xInput == 0 && !IsDashing)
            {
                changeAnimationState(PlayerAnimStates.IDLE_STATE);
            }
            else if (IsDashing && isGrounded)
            {
                changeAnimationState(PlayerAnimStates.DASH);
            }
            else if (isGrounded && !IsDashing && xInput != 0)
            {
                changeAnimationState(PlayerAnimStates.RUN_STATE);
            }
            else if (!isGrounded && yVelocity > 0)
            {
                changeAnimationState(PlayerAnimStates.JUMP_START);
            }
            else if (!isGrounded && yVelocity <= 0)
            {
                changeAnimationState(PlayerAnimStates.DESCEND_START);
            }
        }
    }

    public void changeAnimationState(string state)
    {
        if (state.Equals(currentState))
        {
            return;
        }
        currentState = state;
        anim.Play(state, -1, 0f);

    }

    public void resetAnimatonState()
    {
        currentState = PlayerAnimStates.WILDCARD;
    }

    public void stunPlayer()
    {
        //reset and stop all movement variables
        resetAnimatonState();
        endJumpEarly();
        IsDashing = false;
        stunned = true;
    }

    public void endStun()
    {
        stunned = false;
    }
}
