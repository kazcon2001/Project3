using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public static int score;
    public Text scoreTxt;
    public Text powerUpsActiveText;
    private static float e = 2.71828f; //Math e, used for score graph

    private ShipControl shipControl;

    void Awake()
    {
        Debug.Assert(scoreTxt != null);
    }

    void Start()
    {
        score = 0;                    
        powerUpsActiveText.text = " ";  

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

        //The following code Adds/Removes text from "Active Power Ups:" depending on the Powers Ups status
        if (PowerUpsEffects.PUFireRateEnabled && !powerUpsActiveText.text.Contains("2x Fire Rate"))
        {
            powerUpsActiveText.text += "2x Fire Rate\n";
        }
        else if (ShipControl.fireCooldown == shipControl.FireCooldown) //Using this rather than checking PUFireCooldown 
                                                                       //makes it so we avoid a bug in "Active Power Ups:" text
                                                                       //where text is shown even after game is restarted
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
        else if (!PowerUpsEffects.PUCloneEnabled == false)
        {
            powerUpsActiveText.text = powerUpsActiveText.text.Replace("Shadow Clone", " ");
        }

        if (PowerUpsEffects.PUBossDamageEnabled && !powerUpsActiveText.text.Contains("Increased Damage"))
        {
            powerUpsActiveText.text += "Increased Damage\n";
        }
        else if (PowerUpsEffects.PUBossDamageEnabled == false)
        {
            powerUpsActiveText.text = powerUpsActiveText.text.Replace("Increased Damage", " ");
        }
    }

}
