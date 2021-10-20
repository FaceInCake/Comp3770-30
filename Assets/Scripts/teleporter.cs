using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour
{
    public Vector3 tpPosition;

    private GameObject player;
    private Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CapsuleCharacter").transform.GetChild(0).gameObject;
        spawn = GameObject.Find("SpawnLocale").transform;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c == player.GetComponent<Collider>())
        {
            player.transform.position = tpPosition;
            spawn.position = tpPosition;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
