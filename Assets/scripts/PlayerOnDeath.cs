using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerOnDeath : NetworkBehaviour
{
    Alive life;
    RespawnManager respawnManager;
    GameObject player;

    void Start()
    {
        life = gameObject.GetComponent<Alive>();
        Alive.OnDeath += playerDeath;

        respawnManager = GameObject.Find("RespawnManager").GetComponent<RespawnManager>();

        player = gameObject;
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

            player.GetComponent<CharacterController>().enabled = false;
            //player.transform.position = respawnManager.getRandomRespawnPoint();
            player.transform.position = respawnManager.getClosestRespawnPoint(player.transform.position);
            player.GetComponent<CharacterController>().enabled = true;

            player.GetComponent<MovePlayer>().hasDied();
            player.GetComponent<Alive>().heal(player.GetComponent<Alive>().getMaxHealth());
        }
    }

}
