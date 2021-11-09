using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// note: the player needs to have some friction because to stop jittering when the player stops, drag is disabled. So the player will never fully stop without friction.
public class MovePlayer : MonoBehaviour
{
    public float horizontalAcceleration;
    public float jumpVelocity;

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
        if (Input.GetKey(KeyCode.W)) moveVector += Vector3.forward;
        if (Input.GetKey(KeyCode.A)) moveVector += Vector3.left;
        if (Input.GetKey(KeyCode.S)) moveVector += Vector3.back;
        if (Input.GetKey(KeyCode.D)) moveVector += Vector3.right;
        Vector3.Normalize(moveVector);
        
        // applies drag to the player on x-z plane
        Vector3 xzVel = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        if (xzVel.magnitude>=1) moveVector /= xzVel.magnitude;
        
        // Move
        rigidbody.AddForce(moveVector*horizontalAcceleration*Time.deltaTime);
    }


    // when the player jumps, their y-velocity is 'set' to the jumpVelocity variable
    // I tried just adding jumpVelocity to the current y-velocity but it isn't as satisfying as the current method
    void jump()
    {
        rigidbody.AddForce(Vector3.up * (jumpVelocity - rigidbody.velocity.y), ForceMode.VelocityChange);
    }
}
