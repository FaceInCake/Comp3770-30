using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerOnDeath : MonoBehaviour
{
    Alive life;
    public GameObject proceduralGenerator;
    

    void Start()
    {
        life = gameObject.GetComponent<Alive>();
        Alive.OnDeath += playerDeath;
    }

    private void OnDisable() {
        Alive.OnDeath -= playerDeath;
    }

    void playerDeath(GameObject entity)
    {
        if (gameObject == entity)
        {
            int roomsToGenerate = proceduralGenerator.GetComponent<ProceduralLevelGenerator>().numberOfRooms;
            proceduralGenerator.GetComponent<ProceduralLevelGenerator>().generateNewLevel(roomsToGenerate);
            gameObject.GetComponent<NavMeshAgent>().Warp(Vector3.zero);
            life.setHealth(life.getMaxHealth());
        }
    }

}
