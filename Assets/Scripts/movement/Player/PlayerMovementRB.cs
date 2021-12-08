using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRB : MonoBehaviour
{
    public float speed = 50f;
    Rigidbody rb;
    Vector3 moveDirection;
    float horizontalMovement;
    float verticalMovement;
    bool isGrounded;
    public float jumpForce = 15f;
    public float groundDrag = 6f;
    public float airDrag = 2f;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.4f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.8f + 0.1f);
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        GrabInput();
        // Drag is causing floatiness
        ControlDrag();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void GrabInput()    
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * speed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }
}
