using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    public GameObject player;
    Vector3 direction;
    public float jumpForce = 5f;
    
    private void Start() {
        direction = transform.TransformDirection(Vector3.up*jumpForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        //make's sure the object is the player
        if (collision.gameObject.CompareTag("Player")) 
        {
            player = collision.gameObject;
            //allows player to jump
            player.GetComponentInChildren<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        }
    }
}