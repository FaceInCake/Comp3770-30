using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation {
    //<-- Store all necessary data

    Simulation (string name) {
        //<-- Set starting values
        //<-- open csv file
    }

    ~Simulation () {
        //<-- close the csv file
    }

    // Simulate a time step
    bool simulate () {
        //<-- Do the damage, heal, and dmg count
        writeTimeStep();
        if (isSimOver()) {
            //<-- Close the csv file
            compareHighScores();
            return true;
        }
        return false;
    }

    bool isSimOver() {
        //<-- Is a player dead or boss is dead?
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
