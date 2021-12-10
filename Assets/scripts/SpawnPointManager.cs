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
            redSpawnPoints.Add(redSpawnPointsParent.GetChild(i));
        }

        for (int i = 0; i < blueSpawnPointsParent.childCount; i++)
        {
            blueSpawnPoints.Add(blueSpawnPointsParent.GetChild(i));
        }
    }

    public Vector3 getRandomSpawnPoint(bool redTeam)
    {

        if (redTeam) return redSpawnPoints[Random.Range(0, redSpawnPoints.Count)].position;
        else return blueSpawnPoints[Random.Range(0, blueSpawnPoints.Count)].position;
    }

    public Vector3 getClosestSpawnPoint(bool redTeam, Vector3 position)
    {

        List<Transform> spawnPoints;
        if (redTeam)
            spawnPoints = redSpawnPoints;
        else
            spawnPoints = blueSpawnPoints;

        int closestI = -1;
        float closestDist = 0.0f;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            float dist = Vector3.Distance(position, spawnPoints[i].position);
            if (closestI == -1 || (dist < closestDist))
            {
                closestI = i;
                closestDist = dist;
            }
        }

        return spawnPoints[closestI].position;
    }

}
