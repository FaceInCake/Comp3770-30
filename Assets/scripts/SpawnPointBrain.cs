using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnPointBrain : NetworkBehaviour
{
    [SyncVar]
    public bool isRed;
    [SyncVar]
    int entitiesInTriggerArea = 0;
    [SyncVar]
    public int allowedEntitiesInTriggerArea = 1;

    void Start()
    {
        GameObject tManager = GameObject.Find("TeamManager");
        tManager.GetComponent<TeamManager>().addRespawnPoint(gameObject.transform, isRed);

    }
  

    public bool isOccupied()
    {
        return entitiesInTriggerArea == allowedEntitiesInTriggerArea;
    }

    float occupationTimer = -1.0f;
    float maxOccupationTime;
    public void setOccupiedForTime(float seconds)
    {
        occupationTimer = 0.0f;
        maxOccupationTime = seconds;
        entitiesInTriggerArea++;
    }

    void Update()
    {
        if (occupationTimer > -0.5f)
        {
            occupationTimer += Time.deltaTime;
            if (occupationTimer > maxOccupationTime)
            {
                occupationTimer = -1.0f;
                entitiesInTriggerArea--;
            }
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        entitiesInTriggerArea++;
    }

    public void OnTriggerExit(Collider c)
    {
        entitiesInTriggerArea--;
    }

}
