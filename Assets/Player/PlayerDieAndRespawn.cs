﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerDieAndRespawn : NetworkBehaviour
{

    Alive life;
    TeamManager teamManager;
    PlayerGrabFlagBrain grabFlagBrain;
    GameObject redFlag;
    GameObject blueFlag;

    SpawnPointManager spawnPointManager;

    void Start()
    {
        life = gameObject.GetComponent<Alive>();
        grabFlagBrain = gameObject.GetComponent<PlayerGrabFlagBrain>();
        Alive.OnDeath += playerDeath;

        redFlag = GameObject.Find("RedTeamCaptureFlag");
        blueFlag = GameObject.Find("BlueTeamCaptureFlag");

        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        spawnPointManager = GameObject.Find("SpawnPointManager").GetComponent<SpawnPointManager>();
    }

    private void OnDisable() {
        Alive.OnDeath -= playerDeath;
    }

    void playerDeath(GameObject entity)
    {
        if (!isLocalPlayer)
        {
            return;
        }
    
        if (gameObject == entity)
        {
            bool onRedTeam = teamManager.players[getPlayerIndex(netId)].onRedTeam;
            Debug.Log("Player has died: onRedTeam = " + onRedTeam);
    
            // if the player is holding a flag
            if (redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID == netId)
            {
                // they will set the flag with the color of their team on the ground
                grabFlagBrain.CmdSetFlagHeld(9999, onRedTeam);
            }


            teleportToClosestSpawnPoint(); // has client authority because this is the player (don't need a command)

            // set paralized time
    
            gameObject.GetComponent<Alive>().heal(gameObject.GetComponent<Alive>().getMaxHealth());
        }
    }

    void teleportToClosestSpawnPoint()
    {
        bool onRedTeam = teamManager.players[getPlayerIndex(netId)].onRedTeam;

        gameObject.GetComponent<CharacterController>().enabled = false;
        Vector3 pos = spawnPointManager.getClosestSpawnPoint(onRedTeam, gameObject.transform.position);
        pos.y += 1.0f;
        gameObject.transform.position = pos;
        gameObject.GetComponent<CharacterController>().enabled = true;
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

}
