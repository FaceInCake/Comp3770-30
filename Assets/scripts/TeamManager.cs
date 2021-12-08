using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamManager : NetworkBehaviour
{

    //public SyncList<GameObject> players = new SyncList<GameObject>();

    [SyncVar]
    public GameObject player1;

    [SyncVar]
    public GameObject player2;

    [SyncVar]
    int blueTeamPoints = 0;
    [SyncVar]
    int redTeamPoints = 0;

    List<Transform> redRespawnPoints;
    List<Transform> blueRespawnPoints;

    public Transform redSpawners;
    public Transform blueSpawners;

    public GameObject redTeamFlag;
    public GameObject blueTeamFlag;
    public GameObject redTeamBase;
    public GameObject blueTeamBase;

    [SyncVar]
    bool friendlyFireEnabled = false;


    public void Awake()
    {
        redRespawnPoints = new List<Transform>();
        blueRespawnPoints = new List<Transform>();

        for (int i = 0; i < redSpawners.childCount; i++)
        { 
            redRespawnPoints.Add(redSpawners.GetChild(i));
        }

        for (int i = 0; i < blueSpawners.childCount; i++)
        {
            blueRespawnPoints.Add(blueSpawners.GetChild(i));
        }
    }


    [Server]
    public void setFriendlyFireEnabled(bool enabled)
    {
        friendlyFireEnabled = enabled;
    }

    public bool getFriendlyFireEnabled()
    {
        return friendlyFireEnabled;
    }


    public List<GameObject> getPlayers()
    {
        List<GameObject> p = new List<GameObject>();
        if (player1 != null) p.Add(player1);
        if (player2 != null) p.Add(player2);

        Debug.Log("Players count: "+ p.Count);

        return p;
    }

    [Server]
    public void addPointToRedTeam()
    {
        redTeamPoints++;
    }

    [Server]
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

    [Server]
    public void setRedTeamPoints(int p)
    {
        redTeamPoints = p;
    }

    [Server]
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
                closestDx = dx;
                closestDy = dy;
                closestIndex = i;
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

        if (player1 != null)
        {
            if (player1.GetComponent<PlayerBrain>().isOnRedTeam()) count++;
        }

        if (player2 != null)
        {
            if (player2.GetComponent<PlayerBrain>().isOnRedTeam()) count++;
        }

        return count;
    }

    public int getBluePlayersCount()
    {
        int count = 0;

        if (player1 != null)
        {
            if (!player1.GetComponent<PlayerBrain>().isOnRedTeam()) count++;
        }

        if (player2 != null)
        {
            if (!player2.GetComponent<PlayerBrain>().isOnRedTeam()) count++;
        }

        return count;
    }

}
