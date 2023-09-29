using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed;
    public float sprintSpeed;
    public float crouchSpeed;

    public float movementSpeed;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    public bool readyToJump;
    public bool isCrouching;
    public bool isSprinting;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sprintKey = KeyCode.LeftShift;

    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public float sensX;

    private float yRotation;

    public float sensY;

    private float xRotation;

    public Camera playerCamera;

    public float jumpHelper;

    public Vector3 networkPosition;
    public Quaternion networkRotation;


    private void Awake()
    {
        if (!IsOwner) return;
    }
    private void Start()
    {
        if (!IsOwner) return;
        rb = GetComponent<Rigidbody>();

        playerCamera = GetComponentInChildren<Camera>();


        if (playerCamera != null && playerCamera.enabled)
        {
            Camera.SetupCurrent(playerCamera); //fp mode
        }

        //Cursor.lockState = CursorLockMode.Locked; //hiding cursor (3d fps game)
        //Cursor.visible = false;

        readyToJump = true;
    }

    public float GetDistanceToGround()
    {
        float raycastDistance = 20f;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            return hit.distance;
        }

        return Mathf.Infinity;
    }

    bool IsGrounded(float height)
    {
        float distanceToGround = GetDistanceToGround();
        if (distanceToGround <= height/2f + jumpHelper) 
        {
            return true;
        }

        return false;
    }


    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        
        Rotate();
        grounded = IsGrounded(playerHeight);

        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        CheckForSprintCrouch();
    }

    private void CheckForSprintCrouch()
    {
        if (Input.GetKey(sprintKey))
        {
            isSprinting = true;
            isCrouching = false;
        } else if (Input.GetKey(crouchKey))
        {
            isCrouching = true;
            isSprinting = false;
        } else
        {
            isCrouching = false;
            isSprinting = false;
        }

        movementSpeed = moveSpeed;
        if (isSprinting)
        {
            movementSpeed = sprintSpeed;
        }
        else if (isCrouching)
        {
            movementSpeed = crouchSpeed;
        }
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; 
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        

        // in air worster movement
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        } 
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limitation of velocity
        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}