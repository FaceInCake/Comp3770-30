using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    GameObject player;
    public Vector3 spawnPoint;

    void Start()
    {
        player = GameObject.Find("CapsuleCharacter").transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c == player.GetComponent<Collider>())
        {
            player.transform.position = spawnPoint;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
