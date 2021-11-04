using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scoresScript : MonoBehaviour
{
    //Text objects in the scene
    //Where BTP stands for Boss to Party and PTB stands for Party to Boss 
    GameObject lvl1BTP;
    GameObject lvl2BTP;
    GameObject lvl3BTP;
    GameObject lvl1PTB;
    GameObject lvl2PTB;
    GameObject lvl3PTB;
   

    void Start()
    {
        lvl1BTP = GameObject.Find("lvl1BTP");
        lvl2BTP = GameObject.Find("lvl2BTP");
        lvl3BTP = GameObject.Find("lvl3BTP");
        lvl1PTB = GameObject.Find("lvl1PTB");
        lvl2PTB = GameObject.Find("lvl2PTB");
        lvl3PTB = GameObject.Find("lvl3PTB");

        updateScores();
    }

    public void updateScores()
    {
        Scores sc = new Scores();
        if (sc.loadScores()) {
            //layout for updating each text object in the scores scene
            lvl1BTP.GetComponent<Text>().text = sc.highScore.dmgByBoss[0].ToString();
            lvl2BTP.GetComponent<Text>().text = sc.highScore.dmgByBoss[1].ToString();
            lvl3BTP.GetComponent<Text>().text = sc.highScore.dmgByBoss[2].ToString();
            lvl1PTB.GetComponent<Text>().text = sc.highScore.dmgToBoss[0].ToString();
            lvl2PTB.GetComponent<Text>().text = sc.highScore.dmgToBoss[1].ToString();
            lvl3PTB.GetComponent<Text>().text = sc.highScore.dmgToBoss[2].ToString();
        } else Debug.Log("Unable to load scores");
    }

}
