using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBaseBrain : MonoBehaviour
{

    public bool isRed;
    GameObject redBase;
    GameObject blueBase;

    public GameObject flag;

    TeamManager teamManager;

    void Start()
    { 

        redBase = gameObject.transform.Find("RedBase").gameObject;
        blueBase = gameObject.transform.Find("BlueBase").gameObject;

        teamManager = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        if (isRed)
        {
            for (int i = 0; i < redBase.transform.childCount; i++)
                redBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;

            for (int i = 0; i < blueBase.transform.childCount; i++)
                blueBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            for (int i = 0; i < redBase.transform.childCount; i++)
                redBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;

            for (int i = 0; i < blueBase.transform.childCount; i++)
                blueBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
        }
    }


    void OnTriggerEnter(Collider c)
    {

        // ignore all incoming collisions if the flag is held by a player
        if (flag.GetComponent<CaptureFlagBrain>().isPickedUp)
        {
            return;
        }


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
            // ally returned to base, if they have the opponent's flag then they get a point and the game resets

            if (player.GetComponent<PlayerBrain>().getHeldFlag() != null)
            {
                if (isRed)
                {
                    teamManager.addPointToRedTeam();
                    player.GetComponent<PlayerBrain>().getHeldFlag().GetComponent<CaptureFlagBrain>().dropFlag(player);
                    teamManager.GetComponent<TeamManager>().resetMatch();
                }
                else
                {
                    teamManager.addPointToBlueTeam();
                    player.GetComponent<PlayerBrain>().getHeldFlag().GetComponent<CaptureFlagBrain>().dropFlag(player);
                    teamManager.GetComponent<TeamManager>().resetMatch();
                }
            }
        }
    }

    public void returnFlagToBase()
    {
        flag.transform.position = gameObject.transform.position;
    }

}
