using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerOnDeath : NetworkBehaviour
{
    Alive life;
    TeamManager teamManager;
    GameObject player;
    PlayerBrain playerBrain;

    void Start()
    {
        life = gameObject.GetComponent<Alive>();
        Alive.OnDeath += playerDeath;

        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        player = gameObject;

        playerBrain = player.GetComponent<PlayerBrain>();


        if (teamManager.getRedPlayersCount() > teamManager.getBluePlayersCount())
        {
           playerBrain.onRedTeam = false;
        } else
        {
            playerBrain.onRedTeam = true;
        }
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
            Debug.Log("Player has died");

            bool onRedTeam = playerBrain.onRedTeam;

            GameObject possibleFlag = playerBrain.getHeldFlag();
            if (possibleFlag != null)
            {
                possibleFlag.GetComponent<CaptureFlagBrain>().dropFlag();
            }

            
            teamManager.teleportPlayerToClosestSpawnPoint(gameObject);

            playerBrain.hideHat();

            player.GetComponent<MovePlayer>().hasDied();
            player.GetComponent<Alive>().heal(player.GetComponent<Alive>().getMaxHealth());
        }
    }

}
