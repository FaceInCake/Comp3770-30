using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform[] respawnPoints;

    public Vector3 getRandomRespawnPoint()
    {
        int randIndex = Random.Range(0, respawnPoints.Length);
        return respawnPoints[randIndex].position;
    }

    public Vector3 getClosestRespawnPoint(Vector3 point)
    {
        int closestIndex = 0;
        float closestDx = respawnPoints[closestIndex].position.x - point.x;
        float closestDy = respawnPoints[closestIndex].position.y - point.y;

        for (int i = 1; i < respawnPoints.Length; i++)
        {
            float dx = respawnPoints[i].position.x - point.x;
            float dy = respawnPoints[i].position.y - point.y;

            if (closestDx * closestDx + closestDy * closestDy > dx * dx + dy * dy)
            {
                closestDx = dx;
                closestDy = dy;
                closestIndex = i;
            }
        }

        return respawnPoints[closestIndex].position;
    }

}
