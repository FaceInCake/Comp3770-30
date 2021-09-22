using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIt : MonoBehaviour
{
    private Rigidbody rb; // kinematic rigidbodies are unaffected by physics so setting velocity doesn't work
    private bool movingLeft = false;
    private float vel = 1.0f / 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate() // called 50 times per second
    {
        if (movingLeft)
        {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x - vel,
                gameObject.transform.position.y,
                gameObject.transform.position.z
            );
        }
        else
        {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x + vel,
                gameObject.transform.position.y,
                gameObject.transform.position.z
            );
        }

        if (movingLeft && gameObject.transform.position.x <= -3.0f)
        {
            movingLeft = false;
        }
        if (!movingLeft && gameObject.transform.position.x >= 3.0f)
        {
            movingLeft = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }


}
