using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    List<GameObject> players;
    int blueTeamPoints = 0;
    int redTeamPoints = 0;

    List<Transform> redRespawnPoints;
    List<Transform> blueRespawnPoints;

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

    public Vector3 getClosestRespawnPoint(Vector3 point, bool redTeam)
    {
        if (redTeam)
        {
            int closestIndex = 0;
            float closestDx = redRespawnPoints[closestIndex].position.x - point.x;
            float closestDy = redRespawnPoints[closestIndex].position.y - point.y;

            for (int i = 1; i < redRespawnPoints.Count; i++)
            {
                float dx = redRespawnPoints[i].position.x - point.x;
                float dy = redRespawnPoints[i].position.y - point.y;

                if (closestDx * closestDx + closestDy * closestDy > dx * dx + dy * dy)
                {
                    closestDx = dx;
                    closestDy = dy;
                    closestIndex = i;
                }
            }

            return redRespawnPoints[closestIndex].position;
        } else
        {
            int closestIndex = 0;
            float closestDx = blueRespawnPoints[closestIndex].position.x - point.x;
            float closestDy = blueRespawnPoints[closestIndex].position.y - point.y;

            for (int i = 1; i < blueRespawnPoints.Count; i++)
            {
                float dx = blueRespawnPoints[i].position.x - point.x;
                float dy = blueRespawnPoints[i].position.y - point.y;

                if (closestDx * closestDx + closestDy * closestDy > dx * dx + dy * dy)
                {
                    closestDx = dx;
                    closestDy = dy;
                    closestIndex = i;
                }
            }

            return blueRespawnPoints[closestIndex].position;
        }
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

}
