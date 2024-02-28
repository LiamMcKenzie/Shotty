using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//https://www.youtube.com/watch?v=KbtcEVCM7bw

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration;
    public float decceleration;
    public float velPower;
    public float frictionAmount = 0.2f;

    public float jumpForce;

    private bool isGrounded;

    public Vector2 moveInput;
    private Rigidbody2D rb;
    public float gravityStrength = 9.8f; 
    
    public float movement;

    public LayerMask groundLayer;

    public Transform groundCheckPoint;
    public Vector2 groundCheckSize;

    [SerializeField]
    private InputActionReference movementInput, jump;

    public float jumpInput;
    public bool canJump = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        //Vector2 movement = new Vector2(horizontalInput, verticalInput);

        moveInput = movementInput.action.ReadValue<Vector2>();

        jumpInput = jump.action.ReadValue<float>();

        //rb.velocity = moveInput * speed;
        if(Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer)){
            isGrounded = true;
        }else
        {
            isGrounded = false;
        }

        if(jumpInput == 1 && isGrounded && canJump)
        {
            Jump();
            jumpInput = 0;
        }

        if(jumpInput == 0 && isGrounded && canJump == false )
        {
            canJump = true;
        }


        
    }

    void FixedUpdate()
    {
        ApplyGravity();

        float targetSpeed = moveInput.x * moveSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right);

        Friction();



    }

    void ApplyGravity()
    {
        Vector2 gravityForce = Vector2.down * gravityStrength;

        rb.velocity += gravityForce * Time.fixedDeltaTime;

    
    }

    void Friction()
    {
        if(isGrounded && Mathf.Abs(moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        canJump = false;
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("jump");
    }
    /*
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }*/
}
