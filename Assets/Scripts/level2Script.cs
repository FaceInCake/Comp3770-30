using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level2Script : MonoBehaviour
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
           //simulation details for level 2

        }

        if (simulation.isSimOver())
        {
            //once the simulation is completed show back button to get back to menu
            backButton.SetActive(true);
        }

    }


}
