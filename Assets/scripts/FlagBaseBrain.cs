using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FlagBaseBrain : NetworkBehaviour
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

        GameObject possiblePlayer = c.gameObject;
        List<GameObject> players = teamManager.getPlayers();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == possiblePlayer)
            {
                possiblePlayer.GetComponent<PlayerBrain>().handleCollisionWithBase(gameObject);
                return;
            }
        }


    }

}
