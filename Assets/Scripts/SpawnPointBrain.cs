using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBrain : MonoBehaviour
{
    public bool isRed;

    void Start()
    {
        GameObject tManager = GameObject.Find("TeamManager");
        tManager.GetComponent<TeamManager>().addRespawnPoint(gameObject.transform, isRed);
    }

}
