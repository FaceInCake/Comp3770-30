using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FlagBaseBrain : NetworkBehaviour
{

    public bool isRed;
    GameObject redBase;
    GameObject blueBase;
    
    void Start()
    { 
    
        redBase = gameObject.transform.Find("RedBase").gameObject;
        blueBase = gameObject.transform.Find("BlueBase").gameObject;
    
        if (isRed)
        {
            for (int i = 0; i < redBase.transform.childCount; i++)
                redBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
    
            for (int i = 0; i < blueBase.transform.childCount; i++)
                blueBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            for (int i = 0; i < redBase.transform.childCount; i++)
                redBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false;
    
            for (int i = 0; i < blueBase.transform.childCount; i++)
                blueBase.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

}
