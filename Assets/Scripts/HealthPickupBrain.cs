using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupBrain : MonoBehaviour
{
    public float respawnTime;

    GameObject pickupBase;
    GameObject pickupTop;
    float pickupTopY;

    void Start()
    {
        pickupBase = gameObject.transform.GetChild(0).gameObject;
        pickupTop = gameObject.transform.GetChild(1).gameObject;
        pickupTopY = pickupTop.transform.position.y;
    }

    void Update()
    {
        float dy = Mathf.Sin(Time.time * 2.0f) * 0.001f;
        Vector3 newPos = pickupTop.transform.position;
        newPos.y += dy;
        pickupTop.transform.position = newPos;

        updateRespawnTimer();

    }

    void updateRespawnTimer()
    {
        if (respawnTimer > -0.5)
        {
            respawnTimer += Time.deltaTime;
        }

        if (respawnTimer > respawnTime)
        {
            respawnTimer = -1.0f;
        }


        if (respawnTimer > -0.5f)
        {
            foreach (Transform t in pickupTop.transform)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            foreach (Transform t in pickupTop.transform)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }

    }

    private float respawnTimer = -1.0f;
    public void handleTriggerEnter(Collider c)
    {

        if (c.gameObject.GetComponent<Alive>() && respawnTimer < -0.5f)
        {
            c.gameObject.GetComponent<Alive>().heal(c.gameObject.GetComponent<Alive>().getMaxHealth());
            respawnTimer = 0.0f;

            GameEvents.current.HealthPickUp();
        }
    }

}
