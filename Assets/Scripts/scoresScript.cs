using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scoresScript : MonoBehaviour
{
    // Start is called before the first frame update
    //Instance of Scores class to load saved scores
    Scores highScores;


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

        //layout for updating each text object in the scores scene
        lvl1BTP.GetComponent<Text>().text = "-";
        lvl2BTP.GetComponent<Text>().text = "-";
        lvl3BTP.GetComponent<Text>().text = "-";
        lvl1PTB.GetComponent<Text>().text = "-";
        lvl2PTB.GetComponent<Text>().text = "-";
        lvl3PTB.GetComponent<Text>().text = "-";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
