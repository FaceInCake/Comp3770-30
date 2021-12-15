using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamManager : NetworkBehaviour
{

    public PlayerInfo[] players;
    public int redTeamPoints = 0;
    public int blueTeamPoints = 0;

    public SpawnPointManager spawnManager;

    void Awake()
    {
        players = new PlayerInfo[4];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new PlayerInfo();
            players[i].id = 9999;
            players[i].onRedTeam = true;
        }
    }

    [Server]
    void Update()
    {
        if (redTeamPoints >= 2 || blueTeamPoints >= 2)
        {
            // --- Choose a random level that is not the current one
            int nextLevel;
            while (true)
            {
                nextLevel = Random.Range(0, spawnManager.levels.Length);
                if (nextLevel != spawnManager.currentLevelIndex)
                {
                    break;
                }
            }
            
            // --- Switch all the spawn points and move flag bases to the new level
            spawnManager.setToLevel(nextLevel);

            
            // --- Respawn all the players
            StartCoroutine(waitToRespawn());
            respawnAllPlayers();

            // -- Set team points back to 0
            redTeamPoints = 0;
            blueTeamPoints = 0;
            RpcSetTeamPoints(0, 0);
        }
    }

    IEnumerator waitToRespawn()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        respawnAllPlayers();
    }

    [ClientRpc]
    public void RpcSetTeamPoints(int redPoints, int bluePoints)
    {
        redTeamPoints = redPoints;
        blueTeamPoints = bluePoints;
    }

    public delegate void PlayersShouldRespawn();
    public static event PlayersShouldRespawn RespawnAllPlayers;

    static void respawnAllPlayers()
    {
        if (RespawnAllPlayers != null)
        {
            RespawnAllPlayers();
        }
    }



    [Server]
    public void resyncPlayersList()
    {
        for (int i = 0; i < players.Length; i++)
        {
            RpcSyncPlayer(i, players[i].id, players[i].onRedTeam, players[i].position);
        }
    }

    [ClientRpc]
    void RpcSyncPlayer(int index, uint id, bool onRedTeam, Vector3 position)
    {
        players[index].id = id;
        players[index].onRedTeam = onRedTeam;
        players[index].position = position;
    }

}
