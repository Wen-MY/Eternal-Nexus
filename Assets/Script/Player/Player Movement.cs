using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    public float walkSpeed = 50f;
    public float sprintSpeed = 75f;
    public float groundDrag = 5f;

    public float dashSpeed = 100f;

    [Header("Jump")]
    public float jumpMagnitude = 8f;//default 8 jump force
    public float jumpCooldown = 0.75f;
    public float gravityScale = 0.5f;
    bool onJump;

    [Header("Crouch")]
    public float crouchSpeed = 25f;
    public float crouchHeight = 0.5f;
    private float initialHeight;

    [Header("Ground Check")]
    public float playerHeight = 2f; //default 2
    public LayerMask groundLayer;
    bool onGround;

    [Header("Slope Check")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Animator animator;

    public enum MovementState
    { 
        crouching,
        walking,
        sprinting,
        dashing,
        inAir
    }

    public bool dashing;

    [Header("Others")]
    public Transform orientation;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState currentState;

    private float horizontalInput;
    private float verticalInput;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   //If freezeRotation is enabled, the rotation is not modified by the physics simulation. This is useful for creating first person shooters, because the player needs full control of the rotation using the mouse
        initialHeight = transform.localScale.y;
    }
    void FixedUpdate() //for physics calculations , use fixed update for smoother movement
    {
        playerMovement();
        applyGravity();
    }
    // Update is called once per frame
    void Update()
    {
        //player on ground check
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        if (onGround)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        getInput();
        speedLimiting();
        speedController();
        debug();

<<<<<<< HEAD:Assets/Player Movement.cs
        if (currentState == MovementState.walking || currentState == MovementState.sprinting || currentState == MovementState.crouching)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
=======

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        animator.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));
>>>>>>> 72a3b1df83f62ef7bba70459f005a3febefadd73:Assets/Script/Player/Player Movement.cs
    }

    private void getInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpInput();
        crouchInput();
          
    }

    private void playerMovement()
    {
        //calculation of player movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //seperate speed when on slope
        if(onSlope() && !exitingSlope)
        {
            rb.AddForce(getSlopeMoveDirection() * moveSpeed * 1.5f, ForceMode.Force);

            if(rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        rb.useGravity = !onSlope(); //turn off gravity when player climbing slope, prevent player slide down when climbing
        //seperate speed on ground and not on ground
        if(onGround)
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
    private void speedLimiting()
    {
        //limiting speed on slope
        if(onSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //limit velocity 
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        
    }
    private void jumpInput()
    {
        if (Input.GetButton("Jump") && !onJump && onGround)
        {
            onJump = true;
            exitingSlope = true;
            rb.velocity = new Vector3(rb.velocity.x * 0.5f, 0f, rb.velocity.z * 0.5f); //reset player jump height

            rb.AddForce(transform.up * jumpMagnitude, ForceMode.Impulse);
            Invoke(nameof(resetJump), jumpCooldown); //invoke the method reset jump after cooldown, allow jumping continuously
        }
    }
    private void applyGravity()
    {
        // Apply custom gravity to the Rigidbody
        Vector3 gravity = gravityScale * Physics.gravity * 2f;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
    private void resetJump()
    {
        onJump = false;
        exitingSlope = false;
    }
    private void crouchInput()
    {
        if (Input.GetButtonDown("Crouch")) //let the force only apply once
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if(Input.GetButtonUp("Crouch")) //when unpress , return to normal
        {
            transform.localScale = new Vector3(transform.localScale.x, initialHeight, transform.localScale.z);
        }
    }
       
    private void speedController()
    {
        if (onGround && Input.GetButton("Crouch"))
        {
            currentState = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        else if (onGround && Input.GetButton("Sprint") && Input.GetAxisRaw("Vertical")>0)
        {
            currentState = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if(onGround)
        {
            currentState = MovementState.walking;
            moveSpeed = walkSpeed;
   
        }
        else
        {
            currentState = MovementState.inAir;

            if (desiredMoveSpeed < sprintSpeed)
                desiredMoveSpeed = walkSpeed;
            else
                desiredMoveSpeed = sprintSpeed;
        }
        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;


        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = currentState;
    }
    
    private bool onSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out slopeHit,playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }
    private Vector3 getSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    private void dashInput()
    {
        if (dashing)
        {
            currentState = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
        }
    }
    private void debug()
    {
       
    }
}
