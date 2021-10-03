using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isColliding = false;

    void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other != gameObject.transform.parent.GetComponent<Collider>())
        {
            isColliding = true;
        }
    }
}
