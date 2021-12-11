using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerGrabFlagBrain : NetworkBehaviour
{

    GameObject redFlag;
    GameObject blueFlag;
    GameObject redFlagBase;
    GameObject blueFlagBase;

    void Start()
    {
        redFlag = GameObject.Find("RedTeamCaptureFlag");
        blueFlag = GameObject.Find("BlueTeamCaptureFlag");
        redFlagBase = GameObject.Find("RedTeamFlagBase");
        blueFlagBase = GameObject.Find("BlueTeamFlagBase");
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdSetFlagHeld(netId, true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            CmdSetFlagHeld(netId, false);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            CmdSetFlagHeld(9999, false);
            CmdSetFlagHeld(9999, true);
        }


        if (redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID == netId)
        {
            CmdSetFlagPos(true, gameObject.transform.position);
        }

        if (blueFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID == netId)
        {
            CmdSetFlagPos(false, gameObject.transform.position);
        }

    }

    [Command]
    public void CmdSetFlagHeld(uint id, bool red)
    {
        if (red)
            redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = id;
        else
            blueFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = id;

        RpcSetFlagHeld(id, red);
    }

    [ClientRpc]
    public void RpcSetFlagHeld(uint id, bool red)
    {
        if (red)
            redFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = id;
        else
            blueFlag.GetComponent<CaptureFlagBrain>().heldByPlayerWithID = id;
    }

    [Command]
    public void CmdSetFlagPos(bool red, Vector3 pos)
    {
        if (red)
            redFlag.transform.position = pos;
        else
            blueFlag.transform.position = pos;
        
        RpcSetFlagPos(red, pos);
    }

    [ClientRpc]
    public void RpcSetFlagPos(bool red, Vector3 pos)
    {
        if (red)
            redFlag.transform.position = pos;
        else
            blueFlag.transform.position = pos;
    }


}
