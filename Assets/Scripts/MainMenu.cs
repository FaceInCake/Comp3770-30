using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void loadScoreScene()
    {
        SceneManager.LoadScene("Scores");
    }

    public void loadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void loadLevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void loadLevelThree()
    {
        SceneManager.LoadScene("Level3");
    }


}
