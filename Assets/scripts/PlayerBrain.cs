using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBrain : NetworkBehaviour
{

    public bool onRedTeam;

    GameObject redHat;
    GameObject blueHat;


    public GameObject heldFlag = null;

    void Start()
    { 
        GameObject teams = GameObject.Find("TeamManager");
        teams.GetComponent<TeamManager>().addPlayer(gameObject);

        redHat = gameObject.transform.Find("RedHat").gameObject;
        blueHat = gameObject.transform.Find("BlueHat").gameObject;

        showHat();

    }

    void Update()
    {
        if (!isLocalPlayer)
            return;


        if (heldFlag != null)
        {
            heldFlag.transform.position = gameObject.transform.position;
        }

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
        if (onRedTeam)
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

    public GameObject getHeldFlag()
    {
        return heldFlag;
    }



}
