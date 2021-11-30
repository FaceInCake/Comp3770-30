using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerOnDeath : MonoBehaviour
{
    Alive life;
    public GameObject proceduralGenerator;
    

    void Start()
    {
        life = gameObject.GetComponent<Alive>();
        Alive.OnDeath += playerDeath;
    }

    private void OnDisable() {
        Alive.OnDeath -= playerDeath;
    }

    void playerDeath(GameObject entity)
    {
        if (gameObject == entity)
        {
            Debug.Log("Player has died");
            //Alive.OnDeath -= playerDeath;
            //life.enabled = false;

            SceneManager.LoadScene("GameFinished");
        }
    }

}
