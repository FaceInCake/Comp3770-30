using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ProceduralLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.ExitPlaymode();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
