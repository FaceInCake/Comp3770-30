using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SplashScreen : MonoBehaviour
{

    public Image splashImage;


    IEnumerator Start()
    {
        //set alpha to 0 so screen is blank
        splashImage.canvasRenderer.SetAlpha(0.0f);
        //fade image into the screen 
        FadeIn();
        //wait for 2.5 seconds before fading out
        yield return new WaitForSeconds(2.5f);
        //fade image out 
        FadeOut();
        //wait for 2.5 seconds before loading main menu 
        yield return new WaitForSeconds(2.5f);
        //load main menu
        SceneManager.LoadScene("MainMenu");
    }

    void FadeIn()
    {
        //go from 0 alpha value to 100 aplha value over 1.5 seconds
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        //go from 1 apha value back to 0 alpha value over 2.5 seconds
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
