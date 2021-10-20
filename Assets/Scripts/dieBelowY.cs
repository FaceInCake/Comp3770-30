using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieBelowY : MonoBehaviour
{
    GameObject player;
    GameObject spawnLocale;

    void Start()
    {
        player = GameObject.Find("CapsuleCharacter").transform.GetChild(0).gameObject;
        spawnLocale = GameObject.Find("SpawnLocale");
    }

    void Update()
    {
        if (player.transform.position.y < -5.0f)
        {
            player.transform.position = spawnLocale.transform.position;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
