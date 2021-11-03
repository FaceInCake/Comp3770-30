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

            gameObject.GetComponent<Text>().text = "";

            gameObject.GetComponent<Text>().text += "BossHP: " + simulation.bossHP + "\n";
            gameObject.GetComponent<Text>().text += "Boss damage recieved: " + simulation.totalDamageDeltToBoss + "\n";
            gameObject.GetComponent<Text>().text += "Boss damage delt: " + simulation.totalDamageDeltByBoss + "\n\n\n";


            gameObject.GetComponent<Text>().text += "TankHP: " + simulation.tankHP + "\n";
            gameObject.GetComponent<Text>().text += "Tank damage delt: " + simulation.totalDamageDeltByTank + "\n\n";

            gameObject.GetComponent<Text>().text += "RogueHP: " + simulation.rogueHP + "\n";
            gameObject.GetComponent<Text>().text += "Rogue damage delt: " + simulation.totalDamageDeltByRogue + "\n\n";

            gameObject.GetComponent<Text>().text += "MageHP: " + simulation.mageHP + "\n";
            gameObject.GetComponent<Text>().text += "Mage damage delt: " + simulation.totalDamageDeltByMage + "\n\n";

            gameObject.GetComponent<Text>().text += "DruidHP: " + simulation.druidHP + "\n";
            gameObject.GetComponent<Text>().text += "Druid damage delt: " + simulation.totalDamageDeltByDruid + "\n\n";



        }

        if (simulation.isSimOver())
        {
            //once the simulation is completed show back button to get back to menu
            backButton.SetActive(true);
        }

    }


}