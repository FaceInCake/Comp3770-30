using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointTrigger : MonoBehaviour
{
    [SerializeField]
    private string toLoad;


    private float gameTime;

    private void Start()
    {
        gameTime = Time.time;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && toLoad!=null)
        {
            float t = Time.time - gameTime;
            GameEvents.current.saveTime(t);
            SceneManager.LoadScene(sceneName:toLoad);

        }
    }

}
