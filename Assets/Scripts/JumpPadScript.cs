using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    Vector3 direction;
    public float jumpForce;
    
    private void Start() {
        direction = transform.TransformDirection(Vector3.up*jumpForce);
    }

    void OnTriggerEnter(Collider col)
    {
        //make's sure the object is the player
        if (col.CompareTag("Player")) 
        {
            //allows player to jump
            //col.GetComponentInChildren<MovePlayer>().velocity.y += jumpForce;
        }
    }
}