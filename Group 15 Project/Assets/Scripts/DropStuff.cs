using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStuff : MonoBehaviour
{
    private AudioSource[] powerUpSound;         //Audio SFX
    public AudioClip[] powerUpClips;            //for power ups

    [Tooltip("All power ups added here'")] public GameObject[] powerUps;        
    [Tooltip("Chance of power ups spawning is 1 in 'Drop Chance'")] [SerializeField] private int dropChance;
    public Material ShadowCloneMaterial;  
    public Material InvincibleMaterial;             
    public static float Timer;                  //power up timer, 1 static timer for all power ups                 

    void Awake()
    {
        Timer = 0;

        powerUpSound = GetComponents<AudioSource>();
        
        Random.seed = (int)Time.realtimeSinceStartup;
    }

    void FixedUpdate()                          //FixedUpdate to fix wacky timer bug
    {
        Timer -= Time.deltaTime;

        //The following code if/which/where power up to spawn
        int drop = Random.Range(1, dropChance);
        if (drop <= dropChance/dropChance)                                                  //if
        {
            int powerUpType = Random.Range(0, powerUps.Length);                             //which
            Vector3 pos = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(5f, 9.5f));   //where

            Instantiate(powerUps[powerUpType], pos, Quaternion.Euler(180, 0, 0));           //180 to fix a bug where particle effects do not show
        }
    }

    public void PlaySound(int powerUpCode)
    {
        powerUpSound[powerUpCode].PlayOneShot(powerUpClips[powerUpCode]);
    }
}
