using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBrain : MonoBehaviour
{

    GameObject doorPX;
    GameObject doorNX;
    GameObject doorPZ;
    GameObject doorNZ;

    void Start()
    {
        init(); // need this function so it can be called before Start() in ProceduralLevelGenerator
    }

    public void init()
    {
        doorPX = gameObject.transform.Find("DoorPX").gameObject;
        doorNX = gameObject.transform.Find("DoorNX").gameObject;
        doorPZ = gameObject.transform.Find("DoorPZ").gameObject;
        doorNZ = gameObject.transform.Find("DoorNZ").gameObject;
    }

    public bool getDoorPXopened()
    {
        return doorPX.GetComponent<BoxCollider>().isTrigger;
    }

    public bool getDoorNXopened()
    {
        return doorNX.GetComponent<BoxCollider>().isTrigger;
    }

    public bool getDoorPZopened()
    {
        return doorPZ.GetComponent<BoxCollider>().isTrigger;
    }

    public bool getDoorNZopened()
    {
        return doorNZ.GetComponent<BoxCollider>().isTrigger;
    }


    public void openDoorPX()
    {
        doorPX.GetComponent<BoxCollider>().isTrigger = true;
        doorPX.GetComponent<MeshRenderer>().enabled = false;
    }

    public void openDoorNX()
    {
        doorNX.GetComponent<BoxCollider>().isTrigger = true;
        doorNX.GetComponent<MeshRenderer>().enabled = false;
    }

    public void openDoorPZ()
    {
        doorPZ.GetComponent<BoxCollider>().isTrigger = true;
        doorPZ.GetComponent<MeshRenderer>().enabled = false;
    }

    public void openDoorNZ()
    {
        doorNZ.GetComponent<BoxCollider>().isTrigger = true;
        doorNZ.GetComponent<MeshRenderer>().enabled = false;
    }


    public void closeDoorPX()
    {
        doorPX.GetComponent<BoxCollider>().isTrigger = false;
        doorPX.GetComponent<MeshRenderer>().enabled = true;
    }

    public void closeDoorNX()
    {
        doorNX.GetComponent<BoxCollider>().isTrigger = false;
        doorNX.GetComponent<MeshRenderer>().enabled = true;
    }

    public void closeDoorPZ()
    {
        doorPZ.GetComponent<BoxCollider>().isTrigger = false;
        doorPZ.GetComponent<MeshRenderer>().enabled = true;
    }

    public void closeDoorNZ()
    {
        doorNZ.GetComponent<BoxCollider>().isTrigger = false;
        doorNZ.GetComponent<MeshRenderer>().enabled = true;
    }


}
