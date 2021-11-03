using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void LoadScoreScene()
    {
        SceneManager.LoadScene("Scores");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene("Level3");
    }


}
