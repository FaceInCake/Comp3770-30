using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnPointManager : NetworkBehaviour
{
    public int currentLevelIndex;
    public GameObject[] levels;

    public GameObject redFlagBase;
    public GameObject blueFlagBase;

    public GameObject redFlag;
    public GameObject blueFlag;

    List<Transform> redSpawnPoints;
    List<Transform> blueSpawnPoints;

    void Awake()
    {
        currentLevelIndex = Random.Range(0, levels.Length);

        redSpawnPoints = new List<Transform>();
        blueSpawnPoints = new List<Transform>();
        setToLevel(currentLevelIndex);
    }

    // is called by an rpc and server function
    public void useLevelAssets(int index)
    {

        // --- Set the spawn point lists to use the spawn points in the selected arena
        redSpawnPoints.Clear();
        blueSpawnPoints.Clear();

        Transform redSpawnPointsParent = levels[index].transform.Find("RedSpawnPoints");
        Transform blueSpawnPointsParent = levels[index].transform.Find("BlueSpawnPoints");

        for (int i = 0; i < redSpawnPointsParent.childCount; i++)
        {
            redSpawnPoints.Add(redSpawnPointsParent.GetChild(i));
        }

        for (int i = 0; i < blueSpawnPointsParent.childCount; i++)
        {
            blueSpawnPoints.Add(blueSpawnPointsParent.GetChild(i));
        }

        // Set the flags and bases to the correct location in the selected arena
        redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = 9999;
        blueFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = 9999;

        redFlag.transform.position = levels[index].transform.Find("RedBaseLocation").position;
        blueFlag.transform.position = levels[index].transform.Find("BlueBaseLocation").position;

        redFlagBase.transform.position = levels[index].transform.Find("RedBaseLocation").position;
        blueFlagBase.transform.position = levels[index].transform.Find("BlueBaseLocation").position;

    }

    [Server]
    public void setToLevel(int index)
    {
        currentLevelIndex = index;
        useLevelAssets(index);
        RpcSetToLevel(index);
    }

    [ClientRpc]
    public void RpcSetToLevel(int index)
    {
        currentLevelIndex = index;
        useLevelAssets(index);
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
