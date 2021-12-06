using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CaptureFlagBrain : NetworkBehaviour
{
    public bool isRed;
    GameObject redChild;
    GameObject blueChild;

    GameObject flagBase;

    TeamManager teamManager;

    public bool isPickedUp = false;

    void Start()
    {
        redChild = gameObject.transform.Find("RedFlag").gameObject;
        blueChild = gameObject.transform.Find("BlueFlag").gameObject;

        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        if (isRed)
            flagBase = GameObject.Find("RedTeamFlagBase");
        else
            flagBase = GameObject.Find("BlueTeamFlagBase");


        if (isRed)
        {
            for (int i = 0; i < redChild.transform.childCount; i++)
                redChild.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;

            for (int i = 0; i < blueChild.transform.childCount; i++)
                blueChild.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
        } else {
            for (int i = 0; i < redChild.transform.childCount; i++)
                redChild.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;

            for (int i = 0; i < blueChild.transform.childCount; i++)
                blueChild.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        // ignore all incoming collisions if the flag is held by a player
        if (isPickedUp)
            return;


        GameObject possiblePlayer = c.gameObject;
        List<GameObject> players = teamManager.getPlayers();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == possiblePlayer)
            {
                handlePlayerCollision(possiblePlayer);
                return;
            }
        }

    }

    void handlePlayerCollision(GameObject player)
    {
        bool playerIsRed = player.GetComponent<PlayerBrain>().onRedTeam;

        if ((playerIsRed && isRed) || (!playerIsRed && !isRed))
        {
            // player and flag are both on the same team, so return the flag to its base
            gameObject.transform.position = flagBase.transform.position;
            isPickedUp = false;
            return;
        }

        if ((playerIsRed && !isRed) || (!playerIsRed && isRed))
        {
            // enemy has collided with this flag, stick onto them
            gameObject.transform.parent = player.transform;
            isPickedUp = true;
            return;
        }

    }

    public void dropFlag()
    {
        // the player carrying this flag has died, unstick from them
        gameObject.transform.parent = null;
        isPickedUp = false;

    }

}
