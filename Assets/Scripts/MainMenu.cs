using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //bool gameover = false;
    //GameObject startButton;

    //private void Start()
    //{
    //    startButton = GameObject.Find("Start");
    //}

    //private void Update()
    //{
    //    //if(gameover == true)
    //    //{
    //    //    startButton.SetActive(false);
    //    //}
    //}

    public void StartGame()
    {
        //if(gameover == false)
        //{
        //    SceneManager.LoadScene("Level1");
        //    Debug.Log("Level One Loaded!!!");
        //}
        SceneManager.LoadScene("TestLevel");
        Debug.Log("TestLevel Loaded!!!");

    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.ExitPlaymode();
        Debug.Log("Game quit!!!");
        //gameover = true;
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
