using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletesUponDeath : MonoBehaviour
{
    void Start()
    {
        Alive.OnDeath += objectDeath;
    }


    void Update()
    {

    }

    void objectDeath(GameObject entity)
    {
        if (gameObject == entity)
        {
            Alive.OnDeath -= objectDeath;
            Object.Destroy(gameObject);
        }
    }
}
