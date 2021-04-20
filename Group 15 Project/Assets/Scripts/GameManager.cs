using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    static private bool isPlayerDead;
    static private bool playerWon;
    static public bool gamePaused;

    public GameObject loseScreen;
    public GameObject wonScreen;
    public GameObject pauseScreen;

    public static float GameTimer; //Timer shown on screen
    public Text GameTimerText;

    static private GameManager instance = null;

    void Awake()
    {

        Debug.Assert(instance == null);
        instance = this;
    }

	
	void Start () {
        
        isPlayerDead = false;
        playerWon = false;

        Debug.Assert(loseScreen != null);
        loseScreen.SetActive(false);
        Debug.Assert(wonScreen != null);
        wonScreen.SetActive(false);
	}

    void Update () {

        //the following code Sets certain screens depending on player status
        if (isPlayerDead)
            loseScreen.SetActive(true);
        if (playerWon)
            wonScreen.SetActive(true);
        if (gamePaused)
            pauseScreen.SetActive(true);
        else
            pauseScreen.SetActive(false);


        GameTimer += Time.deltaTime;
        GameTimer = Mathf.Round(GameTimer * 1000f) / 1000f; //Used to round Timer up to 3 decimal places
        GameTimerText.text = "Timer: " + GameTimer;
    }

    public static void PlayerDied()
    {
        isPlayerDead = true;
        Time.timeScale = 0;
    }

    public static void PlayerWon()
    {
        instance.StartCoroutine("PlayerWonHandler");
    }

    public static void PlayerPaused()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }

    private IEnumerator PlayerWonHandler()
    { 
        yield return new WaitForSeconds(1.5f);
        playerWon = true;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Application.LoadLevel(1);
        GameTimer = 0;
    }

}
