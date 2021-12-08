using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovePlayer : NetworkBehaviour
{
    public float acceleration;
    public float dragHorizontal;
    public float gravity;
    public float jumpVelocity;
    public float mouseSensitivityX;

    public float respawnTime = 2.0f;
    float currentRespawnTimer = -1.0f;

    private float speedModifier = 1.0f; // multiplies acceleration
    
    private bool isGrounded = true;
    public Vector3 velocity = Vector3.zero;

    TeamManager teamManager;
    
    CharacterController controller;
    GameObject camera;
    Alive health;
    GameObject body;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        camera = gameObject.transform.GetChild(1).gameObject;
        health = gameObject.GetComponent<Alive>();
        health.setMaxHealth(400);
        body = gameObject.transform.GetChild(0).gameObject;

        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        if (!isLocalPlayer)
        {
            camera.SetActive(false);
        }
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

    public void OnLook(Vector2 i)
    {
        gameObject.transform.Rotate(0, i.x * mouseSensitivityX, 0, Space.World);

        camera.GetComponent<CharacterCamera>().onYaw(i.y);
    }

    public void OnToggleCamera()
    {
        camera.GetComponent<CharacterCamera>().isFirstPerson = !camera.GetComponent<CharacterCamera>().isFirstPerson;
    }

    Vector2 moveInput;
    public void OnMove(Vector2 input)
    {
        moveInput = input;
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

        if (!isLocalPlayer)
        {
            return;
        }

        if (currentRespawnTimer > respawnTime)
        {
            resetRespawnTimer();
        }

        Vector2 mouseMovement = new Vector2();
        mouseMovement.x = Input.GetAxis("Mouse X");
        mouseMovement.y = Input.GetAxis("Mouse Y");
        OnLook(mouseMovement);

        if (currentRespawnTimer >= 0.0f)
        {
            currentRespawnTimer += Time.deltaTime;
            return;
        }

        isGrounded = controller.isGrounded;

        applyGravity();
        applyHorizontalDrag();

        Vector2 dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) dir.y = dir.y + 1.0f;
        if (Input.GetKey(KeyCode.A)) dir.x = dir.x - 1.0f;
        if (Input.GetKey(KeyCode.S)) dir.y = dir.y -1.0f;
        if (Input.GetKey(KeyCode.D)) dir.x = dir.x + 1.0f;
        OnMove(dir.normalized);

        if (Input.GetKeyDown(KeyCode.Space)) OnJump();


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
        if (!isLocalPlayer)
            return;

        speedModifier = modifier;
        currentSpeedModifierTime = 0.0f;
        currentSpeedModifierMaxTime = time;
    }


    // called by PlayerOnDeath
    public void hasDied()
    {
        if (!isLocalPlayer)
            return;

        currentRespawnTimer = 0.0f;
        body.GetComponent<MeshRenderer>().enabled = false;
    }

    // called automatically
    public void resetRespawnTimer()
    {
        if (!isLocalPlayer)
            return;

        currentRespawnTimer = -1.0f;
        body.GetComponent<MeshRenderer>().enabled = true;

        gameObject.GetComponent<PlayerBrain>().showHat();
    }


    public void teleportToClosestSpawnPoint()
    {
        if (!isLocalPlayer)
            return;

        gameObject.GetComponent<CharacterController>().enabled = false;

        GameObject spawnPoint = teamManager.getClosestRespawnPoint(gameObject.transform.position, gameObject.GetComponent<PlayerBrain>().isOnRedTeam());
        Vector3 newPos = spawnPoint.transform.position;
        newPos.y += 1.0f;
        gameObject.transform.position = newPos;

        gameObject.GetComponent<CharacterController>().enabled = true;
    }

}
