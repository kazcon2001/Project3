using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public static int score;
    public Text scoreTxt;
    public Text powerUpsActiveText;

    void Awake()
    {
        Debug.Assert(scoreTxt != null);
    }

    void Start()
    {
        score = 0;
    }

    public static void IncreaseScore()
    {
        score++;
    }

    void LateUpdate()
    {
        scoreTxt.text = "Score: " + score;

        if (PowerUpsEffects.PUFireRateEnabled)
        {
            powerUpsActiveText.text += "2x Fire Rate";
        }
        if (PowerUpsEffects.PUInvincibilityEnabled && !powerUpsActiveText.text.Contains("Invincibility"))
        {
            powerUpsActiveText.text += "Invincibility";
        }
        if (PowerUpsEffects.PUCloneEnabled)
        {
            powerUpsActiveText.text += "Shadow Clone";
        }
    }

}
