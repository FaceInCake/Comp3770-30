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

        simulation = new Simulation("Level2");
        backButton = GameObject.Find("Back");
        backButton.SetActive(false);

    }


    void Update()
    {
        if (!simulation.isSimOver())
        {
            simulation.simulate();
            if (simulation.tankHP < 1500)
            {
                if (simulation.randRange(1, 2) == 1)
                {
                    // cast big heal
                    simulation.tankHP += 25;
                }
                else
                {
                    // cast small heal
                    simulation.tankHP += 15;
                }
            }
            gameObject.GetComponent<Text>().text = simulation.ToString();
        }

        if (simulation.isSimOver())
        {
            //once the simulation is completed show back button to get back to menu
            backButton.SetActive(true);
        }

    }


}
