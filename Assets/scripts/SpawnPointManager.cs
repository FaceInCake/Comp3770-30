using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public List<Transform> redSpawnPoints;
    public List<Transform> blueSpawnPoints;

    public Transform redSpawnPointsParent;
    public Transform blueSpawnPointsParent;

    void Awake()
    {
        for (int i = 0; i < redSpawnPointsParent.childCount; i++)
        {
            redSpawnPoints.Add(redSpawnPointsParent.GetChild(0));
        }

        for (int i = 0; i < blueSpawnPointsParent.childCount; i++)
        {
            blueSpawnPoints.Add(blueSpawnPointsParent.GetChild(0));
        }
    }

    public Vector3 getRandomSpawnPoint(bool redTeam)
    {
        if (redTeam) return redSpawnPoints[0].position;
        else return blueSpawnPoints[0].position;
    }

    public Vector3 getClosestSpawnPoint(bool redTeam, Vector3 position)
    {
        if (redTeam) return redSpawnPoints[0].position;
        else return blueSpawnPoints[0].position;
    }

}
