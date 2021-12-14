using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FlagBaseBrain : NetworkBehaviour
{

    public bool isRed;
    GameObject redBase;
    GameObject blueBase;
    public GameObject redFlag;
    public GameObject blueFlag;

    public GameObject redFlagBase;
    public GameObject blueFlagBase;

    public TeamManager teamManager;

    void Start()
    { 
    
        redBase = gameObject.transform.Find("RedBase").gameObject;
        blueBase = gameObject.transform.Find("BlueBase").gameObject;
    
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

    [Server]
    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.GetComponent<NetworkIdentity>() != null)
        {
            uint id = c.gameObject.GetComponent<NetworkIdentity>().netId;
            int index = getPlayerIndex(id);

            bool playerIsRed = teamManager.players[index].onRedTeam;

            // if player is holding red flag
            if (redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID == id)
            {
                if (isRed)
                {
                    // red flag was returned to the red base
                    return;
                }
                else
                {
                    // red flag brought to the blue base

                    // -- add a point to the blue team
                    addPointToTeam(false);

                    // -- drop both flags
                    redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = 9999;
                    RpcSetFlagHeld(9999, true);
                    blueFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = 9999;
                    RpcSetFlagHeld(9999, false);

                    // -- move flags onto their bases
                    redFlag.transform.position = redFlagBase.transform.position;
                    RpcSetFlagPos(true, redFlagBase.transform.position);
                    blueFlag.transform.position = blueFlagBase.transform.position;
                    RpcSetFlagPos(false, blueFlagBase.transform.position);

                    // -- move players onto spawn points
                    flagReturnedCallback(true); // player is subscribed to this and will respawn itself

                }
            }

            // if player is holding blue flag
            if (blueFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID == id)
            {
                if (isRed)
                {
                    // blue flag brought to the red base

                    // -- add point to the red team
                    addPointToTeam(true);

                    // -- drop both flags
                    redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = 9999;
                    RpcSetFlagHeld(9999, true);
                    blueFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = 9999;
                    RpcSetFlagHeld(9999, false);

                    // -- move flags onto their bases
                    redFlag.transform.position = redFlagBase.transform.position;
                    RpcSetFlagPos(true, redFlagBase.transform.position);
                    blueFlag.transform.position = blueFlagBase.transform.position;
                    RpcSetFlagPos(false, blueFlagBase.transform.position);

                    // -- move players onto spawn points
                    flagReturnedCallback(false); // player is subscribed to this and will respawn itself

                }
                else
                {
                    // blue flag was brought to the blue base
                    return;
                }
            }

        }
    }

    [Server]
    void addPointToTeam(bool redTeam)
    {
        if (redTeam)
            teamManager.redTeamPoints++;
        else
            teamManager.blueTeamPoints++;

        RpcSetTeamPoints(teamManager.redTeamPoints, teamManager.blueTeamPoints);

    }


    [ClientRpc]
    void RpcSetTeamPoints(int redPoints, int bluePoints)
    {
        teamManager.redTeamPoints = redPoints;
        teamManager.blueTeamPoints = bluePoints;

        Debug.Log("Red team points: " + teamManager.redTeamPoints + "   Blue team points: " + teamManager.blueTeamPoints);
    }


    public delegate void FlagReturned(bool flagIsRed);
    public static event FlagReturned OnFlagReturn;

    static void flagReturnedCallback(bool flagIsRed)
    {
        if (OnFlagReturn != null)
        {
            OnFlagReturn(flagIsRed);
        }
    }



    [ClientRpc]
    public void RpcSetFlagHeld(uint id, bool red)
    {
        if (red)
            GameObject.Find("RedTeamCaptureFlag").gameObject.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = id;
        else
            GameObject.Find("BlueTeamCaptureFlag").gameObject.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = id;
    }


    [ClientRpc]
    public void RpcSetFlagPos(bool red, Vector3 pos)
    {
        if (red)
            GameObject.Find("RedTeamCaptureFlag").gameObject.transform.position = pos;
        else
            GameObject.Find("BlueTeamCaptureFlag").gameObject.transform.position = pos;
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
