using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsEffects : MonoBehaviour
{
    [Tooltip ("How much time the power up lasts in seconds")] [SerializeField] private float powerUpTime;
    private GameObject player;
    private BoxCollider2D playerTemp;
    private ShipControl shipControl;

    private DropStuff dropStuff;

    //private static float timer; //DROPSTUFF STATIC

    public static bool PUFireRateEnabled;
    public static bool PUInvincibilityEnabled;
    public static bool PUCloneEnabled;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTemp = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        shipControl = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControl>();
        dropStuff = GameObject.Find("PowerUps").GetComponent<DropStuff>();

    }
    void Start()
    {
        //timer = 0;
        PUFireRateEnabled = false;
        PUInvincibilityEnabled = false;
        PUCloneEnabled = false;
    }

    void OnBecameInvisible()
    {
       if (name.Contains("(Clone)"))
           Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DropStuff.Timer = powerUpTime;
            switch (gameObject.name)
            {
                case "PUFireRate(Clone)":
                    ShipControl.fireCooldown = ShipControl.fireCooldown / 2;
                    PUFireRateEnabled = true;
                    dropStuff.PlaySound(0);
                    break;
                case "PUInvincibility(Clone)":  
                    PUInvincibilityEnabled = true;
                    playerTemp.enabled = false;
                    player.GetComponent<Renderer>().material.SetColor("_Color",Color.cyan);
                    dropStuff.PlaySound(1);
                    break;
                case "PUPlusDash(Clone)":
                    ShipControl.dashes += 2;
                    dropStuff.PlaySound(2);
                    break;
                case "PUClone(Clone)": //PROBLEM PROBLEM PROBLEM
                    PUCloneEnabled = true;
                    if (player.transform.childCount != 2)
                    {
                        if (((player.transform.childCount + 1) % 2) == 0)
                        {
                            GameObject playerClone = Instantiate(player, new Vector3(player.transform.position.x - 1.7f, player.transform.position.y, 0), Quaternion.identity);
                            playerClone.transform.parent = player.transform;
                            playerClone.transform.GetComponent<BoxCollider2D>().enabled = false;
                            playerClone.GetComponent<Renderer>().material = dropStuff.ShadowCloneMaterial;
                        }
                        else
                        {
                            GameObject playerClone = Instantiate(player, new Vector3(player.transform.position.x + 1.7f, player.transform.position.y, 0), Quaternion.identity);
                            playerClone.transform.parent = player.transform;
                            playerClone.transform.GetComponent<BoxCollider2D>().enabled = false;
                            playerClone.GetComponent<Renderer>().material = dropStuff.ShadowCloneMaterial;
                        }
                    }
                    dropStuff.PlaySound(3);
                    break;

                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    //private void CheckPowerUps()
    //{
    //    if(PUFireRateEnabled == true)
    //    {
    //        PUFireRateEnabled = false;
    //        ShipControl.fireCooldown = shipControl.FireCooldown;
    //        Debug.Log("Disabled");
    //    }
    //    if (PUInvincibilityEnabled == true)
    //    {
    //        PUInvincibilityEnabled = false;
    //        playerTemp.enabled = true;
    //        Debug.Log("Disabled");
    //    }
    //    if (PUCloneEnabled == true)
    //    {
    //        PUCloneEnabled = false;
    //        foreach (Transform child in player.transform)
    //        {
    //            Destroy(child.gameObject);
    //        }
    //        Debug.Log("Disabled");
    //    }
    //}

    void CheckPowerUps2(float timerTemp)
    {
        if (timerTemp <= 0)
        {
            if (ShipControl.fireCooldown != shipControl.FireCooldown)
            {
                PUFireRateEnabled = false;
                ShipControl.fireCooldown = shipControl.FireCooldown;
                Debug.Log("Disabled");
            }
            if (player != null)
            {
                if (playerTemp.enabled == false)
                {
                    PUInvincibilityEnabled = false;
                    playerTemp.enabled = true;
                    player.GetComponent<Renderer>().material.color = Color.white;
                    Debug.Log("Disabled");
                }
            }
            if (player.transform.childCount > 0)//PUCloneEnabled == true) //has children? destroy them
            {
                PUCloneEnabled = false;
                foreach (Transform child in player.transform)
                {
                    Destroy(child.gameObject);
                }
                Debug.Log("Disabled");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //DropStuff.Timer -= Time.deltaTime ;
       // if (timer >= 0)
       // {
            
       //     timer -= Time.deltaTime;
       // }
       //else
       //{
       //     //CheckPowerUps2(timer);
       //     timer = 0.0f;
       //     //CheckPowerUps(); //if you have multiple power ups this function called many times// e.g firecooldown/4
       //}
        CheckPowerUps2(DropStuff.Timer);

        
    }
}
