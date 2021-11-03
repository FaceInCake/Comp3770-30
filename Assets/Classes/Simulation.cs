using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation {

    int timeStep = 0;

    public int bossHP;
    public int totalDamageDeltToBoss = 0;
    public int totalDamageDeltByBoss = 0;

    public int tankHP;
    public int totalDamageDeltByTank = 0;

    public int rogueHP;
    public int totalDamageDeltByRogue = 0;

    public int mageHP;
    public int totalDamageDeltByMage = 0;

    public int druidHP;
    public int totalDamageDeltByDruid = 0;

    public int priestHP;
    public int priestMP;
    public int totalDamageDeltByPriest = 0;

    private int randRange(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    Simulation(string name) {

        bossHP = 5000;
        tankHP = 3000;
        rogueHP = 1500;
        mageHP = 1000;
        druidHP = 1250;
        priestHP = 900;
        priestMP = 1000;

        //<-- open csv file
    }

    ~Simulation () {
        //<-- close the csv file
    }

    // Simulate a time step
    bool simulate () {

        //Deal 5 - 20 dmg to damage-dealers and healer(by boss)
        int dmg = randRange(5, 20);

        tankHP -= dmg;
        rogueHP -= dmg;
        mageHP -= dmg;
        druidHP -= dmg;

        totalDamageDeltByBoss += dmg;


        //Deal 40 - 50 dmg to the tank(dealt by boss)
        dmg = randRange(40, 50);
        tankHP -= dmg;
        totalDamageDeltByBoss += dmg;

        //Deal 5 - 10 dmg to the boss(dealt by tank)
        dmg = randRange(5, 10);
        bossHP -= dmg;
        totalDamageDeltByTank += dmg;


        //Deal 15 - 25 dmg to the boss(dealt by rogue)
        dmg = randRange(15, 25);
        bossHP -= dmg;
        totalDamageDeltByRogue += dmg;

        //Deal 5 - 30 dmg to the boss(dealt by mage)
        dmg = randRange(5, 30);
        bossHP -= dmg;
        totalDamageDeltByMage += dmg;

        //Deal 5 - 15 dmg to the boss(dealt by druid)
        dmg = randRange(5, 15);
        bossHP -= dmg;
        totalDamageDeltByDruid += dmg;


        //Priest randomly selects them self or a damage dealer. (Twice as likely to target them self).Heals that target for 15 HP and drains 5 MP.
        bool selectedSelf = randRange(1, 3) != 1;

        int healAmount = 0;
        if (priestMP >= 5) healAmount = 15;

        if (selectedSelf)
        {
            druidHP += healAmount;
        }
        else
        {
            int chosenTarget = randRange(1, 4);

            switch (chosenTarget)
            {
                case 1:
                    tankHP += healAmount;
                    break;
                case 2:
                    rogueHP += healAmount;
                    break;
                case 3:
                    mageHP += healAmount;
                    break;
                case 4:
                    druidHP += healAmount;
                    break;
            }
        }


        //Priest drains 10 MP and heals the tank for 25 HP
        if (priestMP > 10)
        {
            priestMP -= 10;
            tankHP += 25;
        }

        //Priest regains 3 mana
        priestMP += 3;

        timeStep++;


        writeTimeStep();
        if (isSimOver()) {
            //<-- Close the csv file
            compareHighScores();
            return true;
        }
        return false;
    }

    bool isSimOver() {

        if (bossHP < 0) return true;
        if (tankHP <= 0) return true;
        if (rogueHP <= 0) return true;
        if (mageHP <= 0) return true;
        if (druidHP <= 0) return true;

        return false;
    }

    void compareHighScores () {
        //<-- Create Scores instance
        //<-- load in the current high scores
        //<-- Compare these scores to the high scores
        //<-- Update the high scores where necessary (dmg dealt is higher)
    }

    void writeTimeStep () {
        
    }

}