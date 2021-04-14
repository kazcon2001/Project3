using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public static int score;
    public Text scoreTxt;
    public Text powerUpsActiveText;
    private static float e = 2.71828f;

    void Awake()
    {
        Debug.Assert(scoreTxt != null);
    }

    void Start()
    {
        score = 0;
        powerUpsActiveText.text = " ";
    }

    public static void IncreaseScore()
    {
        score += (int)((e*100)/GameManager.GameTimer);
    }

    void LateUpdate()
    {
        scoreTxt.text = "Score: " + score;

        if (PowerUpsEffects.PUFireRateEnabled && !powerUpsActiveText.text.Contains("2x Fire Rate"))
        {
            powerUpsActiveText.text += "2x Fire Rate\n";
        }
        else if (PowerUpsEffects.PUFireRateEnabled == false)
        {
            powerUpsActiveText.text = powerUpsActiveText.text.Replace("2x Fire Rate", " ");
        }

        if (PowerUpsEffects.PUInvincibilityEnabled && !powerUpsActiveText.text.Contains("Invincibility"))
        {
            powerUpsActiveText.text += "Invincibility\n";
        }
        else if(PowerUpsEffects.PUInvincibilityEnabled == false)
        {
            powerUpsActiveText.text = powerUpsActiveText.text.Replace("Invincibility", " ");
        }

        if (PowerUpsEffects.PUCloneEnabled && !powerUpsActiveText.text.Contains("Shadow Clone"))
        {
            powerUpsActiveText.text += "Shadow Clone\n";
        }
        else if (!PowerUpsEffects.PUCloneEnabled)
        {
            powerUpsActiveText.text = powerUpsActiveText.text.Replace("Shadow Clone", " ");
        }
        //else if ()
        //{
        //    powerUpsActiveText.
        //}
    }

}
