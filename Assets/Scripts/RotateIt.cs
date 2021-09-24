using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIt : MonoBehaviour
{

    void Start()
    {
        
    }

    // called every 1/50th of a second
    void FixedUpdate()
    {
        gameObject.transform.Rotate(30.0f / 50.0f, 60.0f / 50.0f, 90.0f / 50.0f, Space.World);
    }

    void Update()
    {
        
    }
}
