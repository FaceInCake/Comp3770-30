using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndPointBrain : MonoBehaviour
{

    public GameObject player;
    public GameObject proceduralGenerator;
    Vector3 initialPlayerPosition;

    int roomsToGenerate = 0;

    void Start()
    {
        initialPlayerPosition = player.transform.position;
        roomsToGenerate = proceduralGenerator.GetComponent<ProceduralLevelGenerator>().numberOfRooms;
    }


    int framesUntilGenerateNavMesh = -1;
    void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            proceduralGenerator.GetComponent<ProceduralLevelGenerator>().generateNewLevel(roomsToGenerate);
            player.GetComponent<NavMeshAgent>().Warp(initialPlayerPosition);
            framesUntilGenerateNavMesh = 0;
        }
    }

    void Update()
    {
        if (framesUntilGenerateNavMesh >= 0)
        {
            if (framesUntilGenerateNavMesh == 3)
            {
                proceduralGenerator.GetComponent<ProceduralLevelGenerator>().generateNavMesh();
                framesUntilGenerateNavMesh = -1;
            }

            framesUntilGenerateNavMesh++;
        }
    }

}
