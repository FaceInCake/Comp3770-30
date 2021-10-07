using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    GameObject player;
    GameObject spawnLocale;

    void Start()
    {
        player = GameObject.Find("CapsuleCharacter").transform.GetChild(0).gameObject;
        spawnLocale = GameObject.Find("SpawnLocale");
    }

    void OnTriggerEnter(Collider c)
    {
        if (c == player.GetComponent<Collider>())
        {
            player.transform.position = spawnLocale.transform.position;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
