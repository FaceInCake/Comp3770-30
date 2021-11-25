using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeadCamera : MonoBehaviour
{


    public Transform target;
    public float camHeight = 10f;
    public float camAngle = 20f;
    public float camDistance = 45f;
     
    // Start is called before the first frame update
    void Start()
    {
        CamHandler();
    }

    // Update is called once per frame
    void Update()
    {
        CamHandler();
    }

    private void CamHandler()
    {
        if(!target)
        {
            return;
        }


        //Get the world position 
        Vector3 worldPosition = (Vector3.forward * -camDistance) + (Vector3.up * camHeight);
        //Debug.DrawLine(target.position, worldPosition, Color.red);

        //vector to rotate camera
        Vector3 rotatedVector = Quaternion.AngleAxis(camAngle, Vector3.up) * worldPosition;
        //Debug.DrawLine(target.position, rotatedVector, Color.green);

        //keep y-axis of the camera 'flat' 
        Vector3 flatTargetPosition = target.position;
        flatTargetPosition.y = 0f;

        //final position for the camera
        Vector3 finalPosition = flatTargetPosition + rotatedVector;
        //Debug.DrawLine(target.position, finalPosition, Color.blue);

        transform.position = finalPosition;
        transform.LookAt(flatTargetPosition);
    }
}
