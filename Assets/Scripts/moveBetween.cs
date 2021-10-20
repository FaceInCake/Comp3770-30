using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The initial and return point is the object position
public class moveBetween : MonoBehaviour
{
    private Vector3 pointA;
    public Vector3 pointB;

    public float speed = 1.0f;

    void Start()
    {
        pointA = gameObject.transform.position;
        pointB += pointA;
    }

    void Update()
    {
        //float dist = (pointA-pointB).magnitude;
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * speed, 1));
    }
}
