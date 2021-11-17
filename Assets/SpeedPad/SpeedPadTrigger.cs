using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPadTrigger : MonoBehaviour
{
    public float speedBoost;
    public float boostTime;

    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            player.GetComponent<MovePlayer>().applySpeedModifier(speedBoost, boostTime);
        }
    }

}
