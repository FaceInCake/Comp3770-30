using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// note: the player needs to have some friction because to stop jittering when the player stops, drag is disabled. So the player will never fully stop without friction.
public class MovePlayer : MonoBehaviour
{
    public float maxHorizontalSpeed = 6.0f;
    public float horizontalAcceleration = 10.0f;
    public float jumpVelocity = 8.0f;
    public float drag = 4.0f;

    GroundDetector groundDetector;
    Rigidbody rigidbody;

    int jumpCount = 0;

    void Start()
    {
        groundDetector = gameObject.transform.GetChild(0).GetComponent<GroundDetector>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }


    bool wasColliding = false;
    void Update()
    {

        // jump controls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundDetector.isColliding)
            {
                jumpCount = 0;
            }

            if (jumpCount <= 1) {
                jump();
                jumpCount++;
            }
        }

        if (wasColliding == false && groundDetector.isColliding == true)
        {
            jumpCount = 1;
        }
        wasColliding = groundDetector.isColliding;


        // WASD horizontal movement controls
        // The local negative X axis is forwards
        // The local negative Z axis is left
        Vector3 moveVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 forward = new Vector3(0.0f, 0.0f, 1.0f);
            moveVector += forward;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
            moveVector += left;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 backwards = new Vector3(0.0f, 0.0f, -1.0f);
            moveVector += backwards;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
            moveVector += right;
        }

        // applies control force to player
        rigidbody.AddForce(transform.TransformDirection(moveVector.normalized) * horizontalAcceleration, ForceMode.Force);

        // applies drag to the player on x-z plane
        Vector3 xzVel = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

        if (xzVel.magnitude > 0.1f)
            rigidbody.AddForce(xzVel.normalized * -drag);

        // controls the max speed of the player on the x-z plane
        xzVel = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        if (xzVel.magnitude > maxHorizontalSpeed)
        {
            rigidbody.AddForce(xzVel.normalized * (maxHorizontalSpeed - xzVel.magnitude), ForceMode.VelocityChange);
        }

    }


    // when the player jumps, their y-velocity is 'set' to the jumpVelocity variable
    // I tried just adding jumpVelocity to the current y-velocity but it isn't as satisfying as the current method
    void jump()
    {
        rigidbody.AddForce(Vector3.up * (jumpVelocity - rigidbody.velocity.y), ForceMode.VelocityChange);
    }
}
