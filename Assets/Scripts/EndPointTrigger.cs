using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointTrigger : MonoBehaviour
{
    [SerializeField]
    private string toLoad;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && toLoad!=null)
        {
            SceneManager.LoadScene(sceneName:toLoad);
        }
    }

}
