using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level1Script : MonoBehaviour
{

    Simulation simulation;
    GameObject backButton;

    void Start()
    {
        simulation = new Simulation("Level1");
        backButton = GameObject.Find("Back");
        backButton.SetActive(false);
    }


    void Update()
    {
        if (!simulation.isSimOver())
        {
            simulation.simulate();
            gameObject.GetComponent<Text>().text = simulation.ToString();            
        }

        if(simulation.isSimOver())
        {
            //once the simulation is completed show back button to get back to menu
            backButton.SetActive(true);
        }

    }


}
