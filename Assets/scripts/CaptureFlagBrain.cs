using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CaptureFlagBrain : NetworkBehaviour
{

    public uint heldByPlayerWithID = 9999;
    public bool isRed;

    public GameObject redBody;
    public GameObject blueBody;

    public TeamManager teamManager;

    public GameObject redBase;
    public GameObject blueBase;

    void Start()
    {
        if (isRed)
        {
            for (int i = 0; i < redBody.transform.childCount; i++)
                redBody.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;

            for (int i = 0; i < blueBody.transform.childCount; i++)
                blueBody.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
        }

        else
        {
            for (int i = 0; i < redBody.transform.childCount; i++)
                redBody.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;

            for (int i = 0; i < blueBody.transform.childCount; i++)
                blueBody.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    [Server]
    void OnTriggerEnter(Collider c)
    {
        // if the flag is already held by someone, ignore all collisions
        if (heldByPlayerWithID != 9999)
            return;

        if (c.gameObject.GetComponent<NetworkIdentity>() != null)
        {
            uint id = c.gameObject.GetComponent<NetworkIdentity>().netId;
            int index = getPlayerIndex(id);

            // if an opponent hits the flag, grab it
            if ((isRed && !teamManager.players[index].onRedTeam) || (!isRed && teamManager.players[index].onRedTeam))
            {
                heldByPlayerWithID = id;
                RpcSetFlagHeld(id, isRed);
            }

            // if teammate collides with the flag, send it back to base
            if ((isRed && teamManager.players[index].onRedTeam) || (!isRed && !teamManager.players[index].onRedTeam))
            {
                Vector3 pos;
                if (isRed)
                    pos = redBase.transform.position;
                else
                    pos = blueBase.transform.position;

                if (isRed)
                    GameObject.Find("RedTeamCaptureFlag").gameObject.transform.position = pos;
                else
                    GameObject.Find("BlueTeamCaptureFlag").gameObject.transform.position = pos;

                RpcSetFlagPos(isRed, pos);

            }

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
