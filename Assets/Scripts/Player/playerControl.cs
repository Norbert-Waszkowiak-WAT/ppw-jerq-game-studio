using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 0f;
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 3f;
    public float rotationSpeed = 2f;
    public float jumpForce = 2f;
    public float inAirMultiplicator = 0.7f;

    public bool isSprinting;
    public bool isCrouching;
    public bool playerIsGrounded;
    public bool playerIsNearGround;

    public Camera playerCamera;

    public float playerHeight = 2f;
    public float nearGroundDistance = 0.5f;

    private Rigidbody playerRigidBody;

    private float rotationY;
    private float rotationX;

    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode crouchKey = KeyCode.LeftControl;

    private Alteruna.Avatar avatar;

    private void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();

        /*if (avatar != null && !avatar.IsOwner)
            return;*/

        playerRigidBody = GetComponent<Rigidbody>();

        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).tag == "playerCamera")
            {
                playerCamera = transform.GetChild(i).GetComponent<Camera>();
                break;
            }
        }

        if (playerCamera != null && playerCamera.enabled)
        {
            Camera.SetupCurrent(playerCamera); //fp mode
        }

        //Cursor.lockState = CursorLockMode.Locked; //hiding cursor (3d fps game)
        //Cursor.visible = false;
    }

    void Update()
    {
        /*if (avatar != null && !avatar.IsOwner)
            return;*/

        speed = playerRigidBody.velocity.magnitude;
        playerIsGrounded = IsGrounded(playerHeight);
        playerIsNearGround = IsGrounded(playerHeight + nearGroundDistance);
        Vector3 movement = XZMovementCalculations();
        movement.y = playerRigidBody.velocity.y;

        float inAirSpeedMultiplicator = 1f;

        if (!playerIsNearGround)
        {
            inAirSpeedMultiplicator = inAirMultiplicator;
        }

        Vector3 newVelocity = Vector3.zero; // Initialize a new velocity vector

        if (Input.GetKey(sprintKey))
        {
            newVelocity = new Vector3(movement.x * sprintSpeed * inAirSpeedMultiplicator, movement.y, movement.z * sprintSpeed * inAirSpeedMultiplicator);
            isSprinting = true;
            isCrouching = false;
        }
        else if (Input.GetKey(crouchKey))
        {
            newVelocity = new Vector3(movement.x * sprintSpeed * inAirSpeedMultiplicator, movement.y, movement.z * crouchSpeed * inAirSpeedMultiplicator);
            isCrouching = true;
            isSprinting = false;
        }
        else
        {
            // Gradually interpolate the player's velocity towards zero when no keys are pressed
            newVelocity = Vector3.Lerp(playerRigidBody.velocity, Vector3.zero, 0.1f * Time.deltaTime);
            isSprinting = false;
            isCrouching = false;
        }

        // Apply the new velocity
        playerRigidBody.velocity = newVelocity;

        RotationCalculations();
        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);

        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }

        Jumping();
    }



    Vector3 XZMovementCalculations()
    {
        float moveX = Input.GetAxis("Horizontal"); // wasd keys
        float moveY = Input.GetAxis("Vertical");   

        Vector3 movement = new Vector3(moveX, 0f, moveY);

        Vector3 output = transform.TransformDirection(movement); //local -> gloabal rotation

        return output;
    }


    void Jumping()
    {
        if (Input.GetKeyDown(jumpKey) && playerIsGrounded)
        {
            Vector3 jumpDirection = playerRigidBody.velocity.normalized;

            jumpDirection += Vector3.up * 0.6f;
            jumpDirection.Normalize();

            Vector3 jumpVector = jumpDirection * jumpForce;

            playerRigidBody.AddForce(jumpVector, ForceMode.Impulse);
        }
    }


    void RotationCalculations()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed; //mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        rotationY += mouseX; 
        rotationX -= mouseY; //down mouse = down cam
    }

    bool IsGrounded(float height)
    {
        float raycastDistance = height; 

        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }
}
