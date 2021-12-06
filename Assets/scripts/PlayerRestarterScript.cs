using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestarterScript : MonoBehaviour
{
    private GameObject player;
    private GameObject body;
    private Alive php;
    void Start()
    {
        player = GameObject.Find("Player");
        body = player.transform.GetChild(0).gameObject;
        php = player.GetComponent<Alive>();
        Alive.OnDeath += someThingDied;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.y < -1)
            restartPlayer();        
    }

    private void OnDisable() {
        Alive.OnDeath -= someThingDied;
    }    

    void someThingDied (GameObject obj) {
        if (obj.tag == "Player") {
            restartPlayer();
        }
    }

    void restartPlayer () {
        body.transform.SetPositionAndRotation(
            this.transform.position, Quaternion.identity);
        php.heal(999999);
        gun g = player.transform.GetComponentInChildren<gun>();
        if (g) g.currentAmmo = g.maxAmmo;
    }
}
