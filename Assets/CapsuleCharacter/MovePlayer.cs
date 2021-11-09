using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float acceleration;
    public float dragHorizontal;
    public float gravity;
    public float jumpVelocity;
    public float mouseSensitivityX;

    float speedModifier = 1.0f; // multiplies acceleration
    
    private bool isGrounded = true;
    private Vector3 velocity = Vector3.zero;
    
    CharacterController controller;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }


    public Vector3 getForward()
    {
        return -gameObject.transform.right;
    }

    public Vector3 getBackward()
    {
        return gameObject.transform.right;
    }

    public Vector3 getLeft()
    {
        return -gameObject.transform.forward;
    }

    public Vector3 getRight()
    {
        return gameObject.transform.forward;
    }


    private void applyGravity()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
    }

    private void jump()
    {
        velocity.y += jumpVelocity;
    }

    private void handleJumpInput()
    {
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            jump();
        }
    }

    private void handleCameraRotationInput()
    {
        float dx = Input.GetAxis("Mouse X");

        gameObject.transform.Rotate(0, dx * mouseSensitivityX, 0, Space.World);

    }

    private void handleDirectionalInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            velocity += getForward() * (acceleration * speedModifier + dragHorizontal) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            velocity += getLeft() * (acceleration * speedModifier + dragHorizontal) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            velocity += getBackward() * (acceleration * speedModifier + dragHorizontal) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            velocity += getRight() * (acceleration * speedModifier + dragHorizontal) * Time.deltaTime;
        }
    }

    private void applyHorizontalDrag()
    {
        velocity -= new Vector3(velocity.x, 0, velocity.z) * dragHorizontal * Time.deltaTime;
    }


    // in the Unity docs the controller is manipulated in the Update function, not FixedUpdate
    void Update()
    {

        isGrounded = controller.isGrounded;

        applyGravity();
        applyHorizontalDrag();

        handleJumpInput();
        handleDirectionalInput();
        handleCameraRotationInput();

        controller.Move(velocity * Time.deltaTime);

    }

}
