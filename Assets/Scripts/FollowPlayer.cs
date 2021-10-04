using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0, 10, -10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update the position of the camera based on the position of the "target" (the player) + the offset
        transform.position = target.position + offset;
    }
}
