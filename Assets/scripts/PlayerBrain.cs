using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBrain : NetworkBehaviour
{

    bool onRedTeam;

    GameObject redHat;
    GameObject blueHat;


    GameObject heldFlag = null;

    GameObject redFlag;
    GameObject blueFlag;
    GameObject redFlagBase;
    GameObject blueFlagBase;

    GameObject teamManager;

    void Start()
    { 
        teamManager = GameObject.Find("TeamManager");

        redFlag = GameObject.Find("RedTeamCaptureFlag");
        blueFlag = GameObject.Find("BlueTeamCaptureFlag");
        redFlagBase = GameObject.Find("RedTeamFlagBase");
        blueFlagBase = GameObject.Find("BlueTeamFlagBase");


        Debug.Log("Red players count: " + teamManager.GetComponent<TeamManager>().getRedPlayersCount());
        Debug.Log("Blue players count: " + teamManager.GetComponent<TeamManager>().getBluePlayersCount());

        if (teamManager.GetComponent<TeamManager>().getRedPlayersCount() < teamManager.GetComponent<TeamManager>().getBluePlayersCount())
        {
            onRedTeam = false;
            setToTeam(false);
        }
        else
        {
            onRedTeam = true;
            setToTeam(true);
        }


        redHat = gameObject.transform.Find("RedHat").gameObject;
        blueHat = gameObject.transform.Find("BlueHat").gameObject;

        showHat();

        addPlayer(gameObject);

        Debug.Log("Red players count: " + teamManager.GetComponent<TeamManager>().getRedPlayersCount()); 
        Debug.Log("Blue players count: " + teamManager.GetComponent<TeamManager>().getBluePlayersCount());

    }

    void Update()
    {
        if (!isLocalPlayer)
            return;


        if (heldFlag != null)
        {
            setFlagPos(heldFlag, gameObject.transform.position);
        }

    }

    void setToTeam(bool redTeam)
    {
        onRedTeam = redTeam;
    }

    public bool isOnRedTeam()
    {
        return onRedTeam;
    }

    [Command]
    public void setFlagPos(GameObject flag, Vector3 pos)
    {
        flag.transform.position = pos;
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

    [Command]
    void setHeldFlag(GameObject flag)
    {
        heldFlag = flag;
    }

    public GameObject getHeldFlag()
    {
        return heldFlag;
    }


    [Command]
    public void dropFlag()
    {
        if (heldFlag == null)
            return;

        heldFlag = null;
    }


    public void handleCollisionWithFlag(GameObject flag)
    {
        // if the flag is held, then ignore all collisions
        List<GameObject> players = teamManager.GetComponent<TeamManager>().getPlayers();
        foreach (GameObject p in players)
        {
            if (p.GetComponent<PlayerBrain>().getHeldFlag() == flag)
            {
                return;
            }
        }


        bool isRed = flag.GetComponent<CaptureFlagBrain>().isRed;

        if ((onRedTeam && isRed) || (!onRedTeam && !isRed))
        {
            // player and flag are both on the same team, so return the flag to its base
            returnFlagToBase(heldFlag);
            return;
        }

        if ((onRedTeam && !isRed) || (!onRedTeam && isRed))
        {
            // enemy has collided with this flag, stick onto them
            setHeldFlag(flag);
            return;
        }
    }

    [Command]
    void returnFlagToBase(GameObject flag)
    {

        Vector3 pos;

        if (flag.GetComponent<CaptureFlagBrain>().isRed)
        {
            pos = redFlagBase.transform.position;
        }
        else
        {
            pos = blueFlagBase.transform.position;
        }

        setFlagPos(flag, pos);

        // if any players are holding the flag then they need to drop it
        List<GameObject> players = teamManager.GetComponent<TeamManager>().getPlayers();
        foreach (GameObject p in players)
        {
            if (p.GetComponent<PlayerBrain>().getHeldFlag() == flag)
            {
                p.GetComponent<PlayerBrain>().dropFlag();
            }
        }

        //flag.transform.position = flag.GetComponent<CaptureFlagBrain>().flagBase.transform.position;

    }


    public void handleCollisionWithBase(GameObject baseObject)
    {
        bool isRed = baseObject.GetComponent<FlagBaseBrain>().isRed;

        if ((onRedTeam && isRed) || (!onRedTeam && !isRed))
        {
            // ally returned to base, if they have the opponent's flag then they get a point and the game resets
            // player can't hold their own flag, so if they have a flag then they get a capture point
            if (getHeldFlag() != null)
            {
                if (isRed)
                {
                    teamManager.GetComponent<TeamManager>().addPointToRedTeam();
                    dropFlag();
                    resetMatch();
                }
                else
                {
                    teamManager.GetComponent<TeamManager>().addPointToBlueTeam();
                    dropFlag();
                    resetMatch();
                }
            }
        }
    }


    // called after one of the teams gets a point
    [Command]
    public void resetMatch()
    {

        returnFlagToBase(redFlag);
        returnFlagToBase(blueFlag);

        List<GameObject> players = teamManager.GetComponent<TeamManager>().getPlayers();

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<MovePlayer>().teleportToClosestSpawnPoint();
        }

        //if (redTeamPoints >= 10 || blueTeamPoints >= 10)
        //{
            //gameOver();
            //return;
        //}
    }


    [Command]
    public void addPlayer(GameObject player)
    {
        if (teamManager.GetComponent<TeamManager>().player1 == null)
        {
            teamManager.GetComponent<TeamManager>().player1 = player;
            return;
        }

        if (teamManager.GetComponent<TeamManager>().player2 == null)
        {
            teamManager.GetComponent<TeamManager>().player2 = player;
            return;
        }

        player.GetComponent<MovePlayer>().teleportToClosestSpawnPoint();
    }

}
