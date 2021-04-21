using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public static int score;
    public Text scoreTxt;
    public Text powerUpsActiveText; 
    private static float e = 2.71828f; //Math e, used for score graph

    private ShipControl shipControl;   //Used to fix a text bug

    void Awake()
    {
        Debug.Assert(scoreTxt != null);
    }

    void Start()
    {
        score = 0;                     //Initializing these 2 values to get 

        shipControl = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControl>();
    }

    public static void IncreaseScore()
    {
        score += (int)((e*100)/GameManager.GameTimer); //Score increases using a graph
                                                       //the faster you destroy enemies the more points you get
    }

    void LateUpdate()
    {
        scoreTxt.text = "Score: " + score;
    }

}
