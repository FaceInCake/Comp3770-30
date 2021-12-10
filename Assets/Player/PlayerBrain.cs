using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBrain : NetworkBehaviour
{

    GameObject redHat;
    GameObject blueHat;
    
    TeamManager teamManager;
    
    void Start()
    {
        redHat = gameObject.transform.Find("RedHat").gameObject;
        blueHat = gameObject.transform.Find("BlueHat").gameObject;

        teamManager = GameObject.Find("TeamManager").gameObject.GetComponent<TeamManager>();
    
        showHat();
    }
    
    void Update()
    {
        if (!isLocalPlayer)
            return;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < teamManager.players.Length; i++)
            {
                Debug.Log("Players[" + i + "].id = " + teamManager.players[i].id);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            int i = getPlayerIndex(netId);
            bool isOnRedTeam = teamManager.players[i].onRedTeam;
            CmdChangeTeams(netId, !isOnRedTeam);
        }
    }

    int getPlayerIndex(uint id)
    {
        for (int i = 0; i < teamManager.players.Length; i++)
        {
            if (teamManager.players[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }
    
    public void hideHat()
    {
        for (int i = 0; i < redHat.transform.childCount; i++)
            redHat.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
    
        for (int i = 0; i < blueHat.transform.childCount; i++)
            blueHat.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
    }
    
    public void showHat()
    {
        if (teamManager.players[getPlayerIndex(netId)].onRedTeam)
        {
            for (int i = 0; i < redHat.transform.childCount; i++)
                redHat.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
    
            for (int i = 0; i < blueHat.transform.childCount; i++)
                blueHat.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
        }
    
        else
        {
            for (int i = 0; i < redHat.transform.childCount; i++)
                redHat.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
    
            for (int i = 0; i < blueHat.transform.childCount; i++)
                blueHat.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    [Command]
    public void CmdChangeTeams(uint id, bool redTeam)
    {
        teamManager.players[getPlayerIndex(id)].onRedTeam = redTeam;
        teamManager.resyncPlayersList();
        RpcChangeTeams(redTeam);
    }

    [ClientRpc]
    public void RpcChangeTeams(bool redTeam)
    {
        showHat();
    }
    
}
