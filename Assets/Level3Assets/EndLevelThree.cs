using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelThree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            SceneManager.LoadScene(sceneName: "GameFinished");
        }
    }
}
