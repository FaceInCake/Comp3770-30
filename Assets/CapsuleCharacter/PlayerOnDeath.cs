using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnDeath : MonoBehaviour
{
    Alive life;

    void Start()
    {
        life = gameObject.GetComponent<Alive>();
        Alive.OnDeath += playerDeath;
    }


    void Update()
    {
    }

    void playerDeath(GameObject entity)
    {
        if (gameObject == entity)
        {
            Debug.Log("Player has died");
            Alive.OnDeath -= playerDeath;
            life.enabled = false;
        }
    }

}
