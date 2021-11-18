using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    public float acceleration;
    public float dragHorizontal;
    public float gravity;
    public float jumpVelocity;
    public float mouseSensitivityX;

    private float speedModifier = 1.0f; // multiplies acceleration
    
    private bool isGrounded = true;
    public Vector3 velocity = Vector3.zero;
    
    CharacterController controller;
    GameObject camera;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        camera = GameObject.Find("Main Camera");
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

    bool tryingToJump = false;
    public void OnJump()
    {
        tryingToJump = true;
    }

    private void jumpUpdate()
    {
        if (isGrounded && tryingToJump)
        {
            jump();
            tryingToJump = false;
        }
    }

    public void OnLook(InputValue input)
    {
        Vector2 i = input.Get<Vector2>();
        gameObject.transform.Rotate(0, i.x * mouseSensitivityX, 0, Space.World);

        camera.GetComponent<CharacterCamera>().onYaw(i.y);
    }

    public void OnToggleCamera()
    {
        camera.GetComponent<CharacterCamera>().isFirstPerson = !camera.GetComponent<CharacterCamera>().isFirstPerson;
    }

    Vector2 moveInput;
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    public void moveUpdate()
    {
        if (moveInput.y > 0.5f)
        {
            velocity += getForward() * (acceleration * speedModifier + dragHorizontal) * Time.deltaTime;
        }

        if (moveInput.x < -0.5)
        {
            velocity += getLeft() * (acceleration * speedModifier + dragHorizontal) * Time.deltaTime;
        }

        if (moveInput.y < -0.5f)
        {
            velocity += getBackward() * (acceleration * speedModifier + dragHorizontal) * Time.deltaTime;
        }

        if (moveInput.x > 0.5f)
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

        moveUpdate();
        jumpUpdate();

        updateSpeedModifier();

        controller.Move(velocity * Time.deltaTime);

    }

    void updateSpeedModifier()
    {
        if (currentSpeedModifierTime > -0.5f)
        {
            currentSpeedModifierTime += Time.deltaTime;
            if (currentSpeedModifierTime > currentSpeedModifierMaxTime)
            {
                currentSpeedModifierTime = -1.0f;
                speedModifier = 1.0f;
            }
        }
    }

    private float currentSpeedModifierTime = 0.0f;
    private float currentSpeedModifierMaxTime = 0.0f;
    public void applySpeedModifier(float modifier, float time)
    {
        speedModifier = modifier;
        currentSpeedModifierTime = 0.0f;
        currentSpeedModifierMaxTime = time;
    }

}
