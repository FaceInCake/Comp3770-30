using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level3Script : MonoBehaviour
{

    Simulation simulation;
    GameObject backButton;

    void Start()
    {

        simulation = new Simulation("Level3");
        backButton = GameObject.Find("Back");
        backButton.SetActive(false);

    }


    void Update()
    {
        if (!simulation.isSimOver())
        {
            simulation.simulate();
            int additionalDamage = simulation.totalDamageDeltByBoss / 100;
            simulation.tankHP -= additionalDamage;
            simulation.totalDamageDeltByBoss += additionalDamage;

            gameObject.GetComponent<Text>().text = simulation.ToString();
        }

        if (simulation.isSimOver())
        {
            //once the simulation is completed show back button to get back to menu
            backButton.SetActive(true);
        }

    }


}