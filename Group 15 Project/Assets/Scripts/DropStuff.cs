using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStuff : MonoBehaviour
{
    private AudioSource[] powerUpSound;
    public AudioClip[] powerUpClips;

    public GameObject[] powerUps;
    //[SerializeField] private int dropPercentage;
    [Tooltip("Chance of power ups spawning is 1 in 'Drop Chance'")] [SerializeField] private int dropChance;
    public Material ShadowCloneMaterial;
    public Material InvincibleMaterial;
    public static float Timer;


    // Start is called before the first frame update
    void Awake()
    {
        //powerUps[0] = Resources.Load<GameObject>("Prefabs/PUFireRate");
        //powerUps[1] = Resources.Load<GameObject>("Prefabs/PUInvincibility");
        //powerUps[2] = Resources.Load<GameObject>("Prefabs/PUPlusDash");
        //powerUps[3] = Resources.Load<GameObject>("Prefabs/PUClone");
        Timer = 0;

        powerUpSound = GetComponents<AudioSource>();
        
        Random.seed = (int)Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Timer -= Time.deltaTime;
        //Debug.Log(Timer);

        int drop = Random.Range(1, dropChance);
        if (drop <= dropChance/dropChance)
        {
            int powerUpType = Random.Range(0, powerUps.Length);
            Vector3 pos = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(5f, 9.5f));

            Instantiate(powerUps[powerUpType], pos, Quaternion.Euler(180, 0, 0));//Quaternion.identity);
        }
    }

    public void PlaySound(int powerUpCode)
    {
        powerUpSound[powerUpCode].PlayOneShot(powerUpClips[powerUpCode]);
    }
}
