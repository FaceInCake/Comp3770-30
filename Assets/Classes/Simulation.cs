using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class Simulation {
    private string name;

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


    private StreamWriter fileWriter;


    public int randRange(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    public Simulation(string name) {
        this.name = name;

        bossHP = 5000;
        tankHP = 3000;
        rogueHP = 1500;
        mageHP = 1000;
        druidHP = 1250;
        priestHP = 900;
        priestMP = 1000;

        fileWriter = new StreamWriter(Application.dataPath + "/SaveData/" + name + ".csv");
    }

    ~Simulation () {
        fileWriter.Flush();
        fileWriter.Close();
    }

    // Simulate a time step
    public bool simulate () {

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
        totalDamageDeltToBoss += dmg;

        //Deal 15 - 25 dmg to the boss(dealt by rogue)
        dmg = randRange(15, 25);
        bossHP -= dmg;
        totalDamageDeltByRogue += dmg;
        totalDamageDeltToBoss += dmg;

        //Deal 5 - 30 dmg to the boss(dealt by mage)
        dmg = randRange(5, 30);
        bossHP -= dmg;
        totalDamageDeltByMage += dmg;
        totalDamageDeltToBoss += dmg;

        //Deal 5 - 15 dmg to the boss(dealt by druid)
        dmg = randRange(5, 15);
        bossHP -= dmg;
        totalDamageDeltByDruid += dmg;
        totalDamageDeltToBoss += dmg;


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

    public bool isSimOver() {

        if (bossHP < 0) return true;
        if (tankHP <= 0) return true;
        if (rogueHP <= 0) return true;
        if (mageHP <= 0) return true;
        if (druidHP <= 0) return true;

        return false;
    }

    public void compareHighScores () {
        Scores sc = new Scores();
        sc.loadScores();
        int lvl = 0;
        if (this.name.Equals("Level2")) lvl=1;
        else if (this.name.Equals("Level3")) lvl=2;
        if (this.totalDamageDeltByBoss > sc.highScore.dmgByBoss[lvl])
            sc.highScore.dmgByBoss[lvl] = this.totalDamageDeltByBoss;
        if (this.totalDamageDeltToBoss > sc.highScore.dmgToBoss[lvl])
            sc.highScore.dmgToBoss[lvl] = this.totalDamageDeltToBoss;
        sc.saveScores();        
    }

    public void writeTimeStep () {
        object [] ar = {
            timeStep,
            bossHP,
            tankHP,
            rogueHP,
            mageHP,
            druidHP,
            priestHP
        };
        fileWriter.Write(string.Join(",", ar)+"\n");
    }

}
