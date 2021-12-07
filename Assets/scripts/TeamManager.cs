using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamManager : NetworkBehaviour
{

    List<GameObject> players;

    [SyncVar]
    int blueTeamPoints = 0;
    [SyncVar]
    int redTeamPoints = 0;

    List<Transform> redRespawnPoints;
    List<Transform> blueRespawnPoints;

    public GameObject redTeamFlag;
    public GameObject blueTeamFlag;
    public GameObject redTeamBase;
    public GameObject blueTeamBase;


    public bool friendlyFireEnabled = false;

    public void Start()
    {
        players = new List<GameObject>();
    }

    public void addPlayer(GameObject player)
    {
        if (players.Contains(player))
        {
            return;
        }

        players.Add(player);
        teleportPlayerToClosestSpawnPoint(player);
    }

    public void addRespawnPoint(Transform point, bool isRed)
    {
        if (redRespawnPoints == null)
        {
            redRespawnPoints = new List<Transform>();
        }

        if (blueRespawnPoints == null)
        {
            blueRespawnPoints = new List<Transform>();
        }


        if (isRed)
            redRespawnPoints.Add(point);
        else
            blueRespawnPoints.Add(point);
    }

    public List<GameObject> getPlayers()
    {
        return players;
    }

    public void addPointToRedTeam()
    {
        redTeamPoints++;
    }

    public void addPointToBlueTeam()
    {
        blueTeamPoints++;
    }

    public int getRedTeamPoints()
    {
        return redTeamPoints;
    }

    public int getBlueTeamPoints()
    {
        return blueTeamPoints;
    }

    public void setRedTeamPoints(int p)
    {
        redTeamPoints = p;
    }

    public void setBlueTeamPoints(int p)
    {
        blueTeamPoints = p;
    }

    public Vector3 getRandomRespawnPoint(bool redTeam)
    {
        if (redTeam)
        {
            int randIndex = Random.Range(0, redRespawnPoints.Count);
            return redRespawnPoints[randIndex].position;
        }
        else
        {
            int randIndex = Random.Range(0, blueRespawnPoints.Count);
            return blueRespawnPoints[randIndex].position;
        }
    }

    public GameObject getClosestRespawnPoint(Vector3 point, bool redTeam)
    {
        List<Transform> respawnPoints;
        if (redTeam)
            respawnPoints = redRespawnPoints;
        else
            respawnPoints = blueRespawnPoints;

        int closestIndex = -1;
        float closestDx = 0.0f;
        float closestDy = 0.0f;

        for (int i = 0; i < respawnPoints.Count; i++)
        {
            float dx = respawnPoints[i].position.x - point.x;
            float dy = respawnPoints[i].position.y - point.y;

            if (closestIndex == -1 || (closestDx * closestDx + closestDy * closestDy > dx * dx + dy * dy))
            {
                if (!respawnPoints[i].gameObject.GetComponent<SpawnPointBrain>().isOccupied())
                {
                    closestDx = dx;
                    closestDy = dy;
                    closestIndex = i;
                }
            }
        }

        if (closestIndex == -1)
        {
            if (redTeam)
                Debug.Log("There were no unoccupied red respawn points");
            else
                Debug.Log("There were no unoccupied blue respawn points");

            return null;
        }

        return respawnPoints[closestIndex].gameObject;
        
    }

    public int getRedPlayersCount()
    {
        int count = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerBrain>().onRedTeam)
            {
                count++;
            }
        }
        return count;
    }

    public int getBluePlayersCount()
    {
        int count = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].GetComponent<PlayerBrain>().onRedTeam)
            {
                count++;
            }
        }
        return count;
    }

    // called after one of the teams gets a point
    public void resetMatch()
    {

        redTeamBase.GetComponent<FlagBaseBrain>().returnFlagToBase();
        blueTeamBase.GetComponent<FlagBaseBrain>().returnFlagToBase();

        for (int i = 0; i < players.Count; i++)
        {
            teleportPlayerToClosestSpawnPoint(players[i]);
        }

        if (redTeamPoints >= 10 || blueTeamPoints >= 10)
        {
            gameOver();
            return;
        }
    }

    // called after one of the teams has ten points
    public void gameOver()
    {

    }


    public void teleportPlayerToClosestSpawnPoint(GameObject player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        GameObject spawnPoint = getClosestRespawnPoint(player.transform.position, player.GetComponent<PlayerBrain>().onRedTeam);

        spawnPoint.GetComponent<SpawnPointBrain>().setOccupiedForTime(5.0f);

        Vector3 newPos = spawnPoint.transform.position;
        newPos.y += 1.0f;
        player.transform.position = newPos;

        player.GetComponent<CharacterController>().enabled = true;
    }

}
