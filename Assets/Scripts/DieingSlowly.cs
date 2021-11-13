using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieingSlowly : MonoBehaviour
{
    Alive life;

    void Start()
    {
        life = gameObject.GetComponent<Alive>();
    }

    void Update()
    {
        life.dealDamage(0.1f); // this is a mood..
    }
}
