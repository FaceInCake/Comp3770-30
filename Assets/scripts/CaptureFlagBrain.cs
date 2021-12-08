using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CaptureFlagBrain : NetworkBehaviour
{
    public bool isRed;
    GameObject redChild;
    GameObject blueChild;

    public GameObject flagBase;

    public TeamManager teamManager;

    void Start()
    {
        redChild = gameObject.transform.Find("RedFlag").gameObject;
        blueChild = gameObject.transform.Find("BlueFlag").gameObject;


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

        GameObject possiblePlayer = c.gameObject;
        List<GameObject> players = teamManager.getPlayers();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == possiblePlayer)
            {
                possiblePlayer.GetComponent<PlayerBrain>().handleCollisionWithFlag(gameObject);
                return;
            }
        }

    }

}
