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

            player.GetComponent<CharacterController>().enabled = false;
            Vector3 newPos = teamManager.getClosestRespawnPoint(player.transform.position, onRedTeam);
            player.GetComponent<CharacterController>().enabled = true;

            newPos.y += 1.0f;
            player.transform.position = newPos;

            playerBrain.hideHat();

            player.GetComponent<MovePlayer>().hasDied();
            player.GetComponent<Alive>().heal(player.GetComponent<Alive>().getMaxHealth());
        }
    }

}
