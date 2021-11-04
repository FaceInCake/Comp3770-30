using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*

Lets say you wanted to access the counter for the damage
done to the boss for level 3, you'd type:

Scores sc = new Scores();
if (sc.loadScores())
    Debug.Log(sc.highScore.dmgToBoss3);
else
    Debug.Log("Failed to load high scores");

*/

[Serializable]
public class HighScore {
    public long [] dmgToBoss; // Damage done TO the boss for level at i
    public long [] dmgByBoss; // Damage done BY the boss for level at i
    public HighScore () {
        dmgToBoss = new long [3];
        dmgByBoss = new long [3];
    }
}

// This class is responsible for storing, saving, and loading the High Scores of the game
public class Scores {
    private static string path = Application.persistentDataPath+"/HighScores.dat";
    private FileStream file; // The high scores save file
    private BinaryFormatter bf;

    public HighScore highScore;

    public Scores () {
        bf = new BinaryFormatter();
        highScore = new HighScore();
    }

    // Returns true success
    public bool loadScores() {
        if (File.Exists(path))
            file = File.OpenRead(path);
        else
            return false;
        highScore = (HighScore) bf.Deserialize(file);
        file.Flush();
        file.Close();
        return true;
    }

    public void saveScores() {
        if (File.Exists(path))
            file = File.OpenWrite(path);
        else
            file = File.Create(path);
        bf.Serialize(file, highScore);
        file.Flush();
        file.Close();
    }

}
