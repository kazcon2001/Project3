using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    static private bool isPlayerDead;
    static private bool playerWon;
    static public bool gamePaused;
    public GameObject loseScreen;
    public GameObject wonScreen;
    public GameObject pauseScreen;

    public static float GameTimer;
    public Text GameTimerText;

    static private GameManager instance = null;
    //float timer=0;
    void Awake()
    {
        Debug.Assert(instance == null);
        instance = this;
    }

	// Use this for initialization
	void Start () {
        isPlayerDead = false;
        playerWon = false;

        Debug.Assert(loseScreen != null);
        loseScreen.SetActive(false);
        Debug.Assert(wonScreen != null);
        wonScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        GameTimer += Time.deltaTime;
        
        if (isPlayerDead)
            loseScreen.SetActive(true);
        if (playerWon)
            wonScreen.SetActive(true);
        if (gamePaused)
            pauseScreen.SetActive(true);
        else
            pauseScreen.SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused && Time.timeScale != 0)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused && Time.timeScale == 0)
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
        //SceneManager.LoadScene(0);
        Application.LoadLevel(0);
        GameTimer = 0;
    }

}
