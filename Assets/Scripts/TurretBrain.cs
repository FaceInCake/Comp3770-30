using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBrain : MonoBehaviour
{

    public float range;
    public float bulletsPerSecond;
    public float damage;
    public float rotationSpeed;

    GameObject player;
    private Alive pAlive;

    GameObject turretBase;
    GameObject turretHead;
    GameObject muzzleFlash;
    AudioSource bang;

    void Start()
    {
        turretBase = transform.GetChild(0).gameObject;
        turretHead = transform.GetChild(1).gameObject;
        muzzleFlash = turretHead.transform.GetChild(0).gameObject;
        // Bang sound effect should be in the Head object
        bang = transform.GetChild(1).GetComponent<AudioSource>();

        player = GameObject.Find("Player").transform.GetChild(0).gameObject;
        pAlive = GameObject.Find("Player").GetComponent<Alive>();

        foreach (Transform t in muzzleFlash.transform)
        {
            t.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }



    float timeCounter = 0;
    int muzzleFlashIsShowingFrames = -1;
    void Update()
    {
        if (!player)
        {
            return;
        }

        timeCounter += Time.deltaTime;

        float dist = (player.transform.GetChild(0).position - transform.position).magnitude;
        if (dist < range)
        {

            //Vector3 toPlayer = new Vector3(player.transform.GetChild(0).position.x - transform.position.x, 0, player.transform.GetChild(0).position.z - transform.position.z).normalized;

            // turretHead.transform.forward = toPlayer;

            updateTurning();

            if (timeCounter > 1.0f / bulletsPerSecond)
            {
                timeCounter = 0;
                pAlive.dealDamage(damage);
                setMussleFlashVisible(true);
                bang.Play(0);
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
        float dx = player.transform.GetChild(0).position.x - getVirtualPoint(deltaAngle).x;
        float dy = player.transform.GetChild(0).position.z - getVirtualPoint(deltaAngle).z;
        return dx * dx + dy * dy;
    }

    void updateTurning()
    {
        float leftDist = getNewVirtualDistanceSquaredToPlayer(-rotationSpeed * Time.deltaTime);
        float rightDist = getNewVirtualDistanceSquaredToPlayer(rotationSpeed * Time.deltaTime);
        float noTurnDist = getNewVirtualDistanceSquaredToPlayer(0);

        if (leftDist < rightDist && leftDist < noTurnDist)
        {
            turretHead.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        }

        if (rightDist < leftDist && rightDist < noTurnDist)
        {
            turretHead.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }

    }

}