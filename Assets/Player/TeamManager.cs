using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TeamManager : NetworkBehaviour
{

    public PlayerInfo[] players;

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
    public void resyncPlayersList()
    {
        for (int i = 0; i < players.Length; i++)
        {
            RpcSyncPlayer(i, players[i].id, players[i].onRedTeam);
        }
    }

    [ClientRpc]
    void RpcSyncPlayer(int index, uint id, bool onRedTeam)
    {
        players[index].id = id;
        players[index].onRedTeam = onRedTeam;
    }

}
