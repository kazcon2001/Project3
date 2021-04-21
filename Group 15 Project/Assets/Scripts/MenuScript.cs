using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);      //Main Menu Scene
    }
    public void PlayGame() 
    {
        GameManager.gamePaused = false; //fixes bug where if game was paused and player decided to
        Time.timeScale = 1;             //go to menu, the game would be paused if he played again

        SceneManager.LoadScene(1);      //Game Scene
    }

    public void ShowControls()
    {
        SceneManager.LoadScene(2);      //Controls Scene
    }
}
