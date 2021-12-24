using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurretBrain : NetworkBehaviour
{

    public float range;
    public float bulletsPerSecond;
    public float damage;
    public float rotationSpeed;
    public float minimumFiringAngle;

    public bool firesAtEachTeam;
    public bool onRedTeam;

    TeamManager teamManager;

    uint targetPlayerID = 9999;
    Vector3 playerPosition;

    GameObject muzzleFlash;

    Transform turretBase;
    Transform turretHead;
    Transform redBaseParent;
    Transform redHeadParent;
    Transform blueBaseParent;
    Transform blueHeadParent;

    AudioSource bang;

    void Awake()
    {

        turretBase = gameObject.transform.Find("Base");
        turretHead = gameObject.transform.Find("Head");

        redBaseParent = gameObject.transform.Find("RedBase");
        redHeadParent = gameObject.transform.Find("RedHead");

        blueBaseParent = gameObject.transform.Find("BlueBase");
        blueHeadParent = gameObject.transform.Find("BlueHead");

        muzzleFlash = turretHead.Find("MuzzleFlash").gameObject;

        localSetToTeam(onRedTeam, firesAtEachTeam);

        // Bang sound effect should be in the Head object
        bang = transform.GetChild(1).GetComponent<AudioSource>();

        playerPosition = new Vector3(0, 0, 0);

        teamManager = GameObject.Find("TeamManager").gameObject.GetComponent<TeamManager>();

        foreach (Transform t in muzzleFlash.transform)
        {
            t.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }



    float timeCounter = 0;
    int muzzleFlashIsShowingFrames = -1;
    [ServerCallback]
    void Update()
    {
        if (targetPlayerID == 9999)
        {
            // -- Check if there are any enemies within range and target the closest one
            int closestIndex = -1;
            float closestDist = 0.0f;
            for (int i = 0; i < teamManager.players.Length; i++)
            {
                float dist = (teamManager.players[i].position - gameObject.transform.position).magnitude;
                if ((closestIndex == -1) || (dist < closestDist))
                {
                    if ((teamManager.players[i].onRedTeam != onRedTeam) || (firesAtEachTeam))
                    {
                        closestIndex = i;
                        closestDist = dist;
                    }
                }
            }

            if (closestIndex == -1)
            {
                targetPlayerID = 9999;
                return;
            }

            targetPlayerID = teamManager.players[closestIndex].id;
        }
        else
        {
            // -- If the player is not in range, stop targeting them
            if (Vector3.Distance(playerPosition, gameObject.transform.position) > range)
            {
                targetPlayerID = 9999;
            }
        }


        // --- Get the position of the targeted player
        playerPosition = teamManager.players[getPlayerIndex(targetPlayerID)].position;

        timeCounter += Time.deltaTime;

        float dist2 = (playerPosition - transform.position).magnitude;
        if (dist2 < range)
        {

            // -- these commented lines make the turret snap instantly to the player
            // Vector3 toPlayer = new Vector3(player.transform.GetChild(0).position.x - transform.position.x, 0, player.transform.GetChild(0).position.z - transform.position.z).normalized;
            // turretHead.transform.forward = toPlayer;

            updateTurning();

            if (timeCounter > 1.0f / bulletsPerSecond)
            {
                if (getAngleBetween(playerPosition - transform.position, getDirectionVector(0)) <= minimumFiringAngle)
                {
                    timeCounter = 0;
                    fireAtPlayer();
                }
            }

        }

        if (muzzleFlashIsShowingFrames != -1)
        {
            muzzleFlashIsShowingFrames++;
        }

        if (muzzleFlashIsShowingFrames > 20)
        {
            setMussleFlashVisible(false);
        }

    }

    void setMussleFlashVisible(bool isVisible)
    {
        if (isVisible == false)
        {
            muzzleFlashIsShowingFrames = -1;
        }
        else
        {
            muzzleFlashIsShowingFrames = 0;
        }

        foreach (Transform t in muzzleFlash.transform)
        {
            t.gameObject.GetComponent<Renderer>().enabled = isVisible;
        }
    }

    private Vector3 getDirectionVector(float deltaAngle)
    {
        float dx = Mathf.Cos(Mathf.Deg2Rad * (turretHead.transform.eulerAngles.y + deltaAngle));
        float dz = Mathf.Sin(Mathf.Deg2Rad * (turretHead.transform.eulerAngles.y + deltaAngle));

        return new Vector3(dz, 0, dx);
    }

    private Vector3 getVirtualPoint(float deltaAngle)
    {
        Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
        pos += getDirectionVector(deltaAngle) * 3;
        return pos;
    }

    private float getNewVirtualDistanceSquaredToPlayer(float deltaAngle)
    {
        float dx = playerPosition.x - getVirtualPoint(deltaAngle).x;
        float dy = playerPosition.z - getVirtualPoint(deltaAngle).z;
        return dx * dx + dy * dy;
    }

    void updateTurning()
    {
        float leftDist = getNewVirtualDistanceSquaredToPlayer(-rotationSpeed * Time.deltaTime);
        float rightDist = getNewVirtualDistanceSquaredToPlayer(rotationSpeed * Time.deltaTime);
        float noTurnDist = getNewVirtualDistanceSquaredToPlayer(0);

        if (leftDist < rightDist && leftDist < noTurnDist)
        {
            turretHead.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        }

        if (rightDist < leftDist && rightDist < noTurnDist)
        {
            turretHead.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }

        redHeadParent.transform.eulerAngles = new Vector3(
            0, turretHead.eulerAngles.y, 0
        );
        blueHeadParent.transform.eulerAngles = new Vector3(
            0, turretHead.eulerAngles.y, 0
        );

        syncRotation(turretHead.eulerAngles.y);

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


    [ClientRpc]
    void syncRotation(float rotation)
    {
        turretHead.transform.eulerAngles = new Vector3(
            0, rotation, 0
        );

        redHeadParent.transform.eulerAngles = new Vector3(
            0, rotation, 0
        );

        blueHeadParent.transform.eulerAngles = new Vector3(
            0, rotation, 0
        );
    }


    public delegate void TurretFired(uint targetId, float damage);
    public static event TurretFired OnTurretFired;

    static void turretFiredCallback(uint targetId, float damage)
    {
        if (OnTurretFired != null)
        {
            OnTurretFired(targetId, damage);
        }
    }


    void fireAtPlayer()
    {
        RpcDamagePlayer(targetPlayerID, damage);
        setMussleFlashVisible(true);
        bang.Play(0);
    }

    [ClientRpc]
    void RpcDamagePlayer(uint id, float damage)
    {
        turretFiredCallback(id, damage);
    }


    float getAngleBetween(Vector3 turretDir, Vector3 toPlayer)
    {
        return Mathf.Acos(Vector3.Dot(turretDir, toPlayer) / (Mathf.Abs(turretDir.magnitude * toPlayer.magnitude)));
    }

    [Server]
    public void setToTeam(bool onRedTeam, bool isNeutral)
    {
        localSetToTeam(onRedTeam, isNeutral);
        RpcSetToTeam(onRedTeam, isNeutral);
    }

    [ClientRpc]
    void RpcSetToTeam(bool onRedTeam, bool isNeutral)
    {
        localSetToTeam(onRedTeam, isNeutral);
    }

    void localSetToTeam(bool onRedTeam, bool isNeutral)
    {
        firesAtEachTeam = isNeutral;
        this.onRedTeam = onRedTeam;

        // --- show the correct parts for the team
        setChildrenVisible(turretBase, false);
        setChildrenVisible(turretHead, false);

        setChildrenVisible(redBaseParent, false);
        setChildrenVisible(redHeadParent, false);

        setChildrenVisible(blueBaseParent, false);
        setChildrenVisible(blueHeadParent, false);

        if (firesAtEachTeam)
        {
            setChildrenVisible(turretBase, true);
            setChildrenVisible(turretHead, true);
            return;
        }

        if (onRedTeam)
        {
            setChildrenVisible(redBaseParent, true);
            setChildrenVisible(redHeadParent, true);
        }
        else
        {
            setChildrenVisible(blueBaseParent, true);
            setChildrenVisible(blueHeadParent, true);
        }
    }

    void setChildrenVisible(Transform transform, bool isVisible)
    {

        foreach (Transform child in transform)
        {
            if (child.childCount > 0)
            {
                setChildrenVisible(child, isVisible);
            }
            else
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = isVisible;
            }
        }
    }


}