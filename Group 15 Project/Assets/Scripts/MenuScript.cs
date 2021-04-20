using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);   //Main Menu Scene
    }
    public void PlayGame() 
    {
        GameManager.gamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);  //Game Scene
    }

    public void ShowControls()
    {
        SceneManager.LoadScene(2);  //Controls Scene
    }
}
