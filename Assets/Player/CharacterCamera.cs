using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{

    public GameObject characterBody;
    public bool isFirstPerson;

    public float fp_mouseSensitivity;
    public float fp_maxCameraAngleY;
    public float fp_minCameraAngleY;
    public float fp_camHeight;
    float fp_currentAngleY;

    public float tp_mouseSensitivity;
    public float tp_minCameraAngleY;
    public float tp_maxCameraAngleY;
    public float tp_camDistance;
    float tp_currentAngleY;

    void Start()
    {
        isFirstPerson = true;
    }

    float mouseYaw = 0.0f;
    public void onYaw(float value)
    {
        mouseYaw = value;
    }

    void updateThirdPerson()
    { 

        float mouseDY = mouseYaw * -tp_mouseSensitivity;
        tp_currentAngleY += mouseDY;

        if (tp_currentAngleY > tp_maxCameraAngleY)
        {
            tp_currentAngleY = tp_maxCameraAngleY;
        }

        if (tp_currentAngleY < tp_minCameraAngleY)
        {
            tp_currentAngleY = tp_minCameraAngleY;
        }


        float camHeight = Mathf.Sin(tp_currentAngleY) * tp_camDistance;
        float camDistHorozontal = Mathf.Cos(tp_currentAngleY) * tp_camDistance;
        Vector3 offset = new Vector3(-tp_camDistance, -camHeight, 0);

        float desiredAngle = characterBody.transform.eulerAngles.y;
        float angle = desiredAngle;

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = characterBody.transform.position - (rotation * offset);

        transform.LookAt(characterBody.transform);
    }

    void updateFirstPerson()
    {
        //float camDY = Input.GetAxis("Mouse Y") * -fp_mouseSensitivity;
        float camDY = mouseYaw * -fp_mouseSensitivity;
        fp_currentAngleY += camDY;
        if (fp_currentAngleY > fp_maxCameraAngleY)
        {
            fp_currentAngleY = fp_maxCameraAngleY;
        }

        if (fp_currentAngleY < fp_minCameraAngleY)
        {
            fp_currentAngleY = fp_minCameraAngleY;
        }

        Quaternion rotation = Quaternion.Euler(fp_currentAngleY, 0, 0);

        transform.position = characterBody.transform.position + Vector3.up * fp_camHeight;
        transform.rotation = rotation;
        transform.Rotate(0, characterBody.transform.eulerAngles.y - 90, 0, Space.World);
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            isFirstPerson = !isFirstPerson;
        }
        */

        if (isFirstPerson)
        {
            updateFirstPerson();
        }

        else
        {
            updateThirdPerson();
        }
    }
}

