using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can attach to anything to test Scores
// Press L to load the scores
// Press S to save the scores
// Press P to print the current stored scores
// Press 1 to increment the counter for dmg done TO boss for level 1
// Press 2 to increment the counter for dmg done BY boss for level 1
// Press 3 to increment the counter for dmg done TO boss for level 2
// Press 4 to increment the counter for dmg done BY boss for level 2
// Press 5 to increment the counter for dmg done TO boss for level 3
// Press 6 to increment the counter for dmg done BY boss for level 3
public class ScoreTester : MonoBehaviour
{
    Scores sc;

    // Start is called before the first frame update
    void Start()
    {
        sc = new Scores();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            sc.loadScores();
        else
        if (Input.GetKeyDown(KeyCode.S))
            sc.saveScores();
        else
        if (Input.GetKeyDown(KeyCode.Alpha1))
            sc.highScore.dmgToBoss[0] += 1;
        else
        if (Input.GetKeyDown(KeyCode.Alpha2))
            sc.highScore.dmgByBoss[0] += 1;
        else
        if (Input.GetKeyDown(KeyCode.Alpha3))
            sc.highScore.dmgToBoss[1] += 1;
        else
        if (Input.GetKeyDown(KeyCode.Alpha4))
            sc.highScore.dmgByBoss[1] += 1;
        else
        if (Input.GetKeyDown(KeyCode.Alpha5))
            sc.highScore.dmgToBoss[2] += 1;
        else
        if (Input.GetKeyDown(KeyCode.Alpha6))
            sc.highScore.dmgByBoss[2] += 1;
        else
        if (Input.GetKeyDown(KeyCode.P))
            Debug.LogFormat(
                "{0} : {1} | {2} : {3} | {4} : {5}",
                sc.highScore.dmgToBoss[0],
                sc.highScore.dmgByBoss[0],
                sc.highScore.dmgToBoss[1],
                sc.highScore.dmgByBoss[1],
                sc.highScore.dmgToBoss[2],
                sc.highScore.dmgByBoss[2]
            );
    }
}
