using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTuretTest : MonoBehaviour
{

    GameObject turret;

    void Start()
    {
        turret = GameObject.Find("Turret");   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            turret.GetComponent<Alive>().dealDamage(30);
        }
    }
}
