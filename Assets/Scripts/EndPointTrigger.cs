using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointTrigger : MonoBehaviour
{
    void Start()
    {
        
    }


    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            SceneManager.LoadScene(sceneName:"Level2");
        }
    }

}
