using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
  
    GameObject eventHandler;
    GameEventHandler eH;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        eventHandler = GameObject.Find("ScoreKeeper");
        if (eventHandler==null) {
            Destroy(gameObject);
            return;
        }
        eH = eventHandler.GetComponent<GameEventHandler>();

    }

 
    // Update is called once per frame
    void Update()
    {
        
        gameObject.GetComponent<Text>().text = "Med Kits Consumed : "+ eH.healthStat + "\n"
                                                + " Enemies Killed :" + eH.deadEnemies + "\n"
                                                + " Damage Received : " + eH.damage + "\n"
                                                +" Time taken to complete levels : " + ((int)eH.time/60).ToString() + ":" + (eH.time%60).ToString("f2");

        Destroy(eventHandler);
    }

}
