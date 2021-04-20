using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsEffects : MonoBehaviour
{
    [Tooltip ("How much time the power up lasts in seconds")] [SerializeField] private float powerUpTime;
    private GameObject player;
    private BoxCollider2D playerCollider;           //Used to make player Invincible
    private ShipControl shipControl;                //Used to reference Fire Cooldown
    private GameObject playerPivot;                 //Used to rotate fireOrb around player
    private GameObject fireOrb;                     

    private DropStuff dropStuff;                    //Used to trigger power up sound effects

    //Power Ups
    public static bool PUFireRateEnabled;
    public static bool PUInvincibilityEnabled;
    public static bool PUCloneEnabled;
    public static bool PUBossDamageEnabled;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        shipControl = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControl>();
        playerPivot = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Pivot").gameObject;
        fireOrb = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Pivot").transform.FindChild("Sphere").gameObject;
        dropStuff = GameObject.Find("PowerUps").GetComponent<DropStuff>();

    }

    void OnBecameInvisible()
    {
       if (name.Contains("(Clone)"))                //Bug where it destroys prefabs with particle 
                                                    //effects even though they are already off screen
            Destroy(gameObject);
    }

    //The following code gives the player boosts depending on the power ups consumed  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DropStuff.Timer = powerUpTime;          //Static timer takes value depending on power up consumed
            switch (gameObject.name)
            {
                case "PUFireRate(Clone)":           //Halves fire cooldown and adds a rotating fireball around player
                    ShipControl.fireCooldown = ShipControl.fireCooldown / 2;
                    PUFireRateEnabled = true; 
                    GameObject FireOrb = Instantiate(fireOrb, new Vector3(playerPivot.transform.position.x, playerPivot.transform.position.y + 0.7f, 0), playerPivot.transform.rotation);
                    FireOrb.transform.parent = player.transform.FindChild("Pivot");
                    FireOrb.SetActive(true);
                    dropStuff.PlaySound(0);
                    break;
                case "PUInvincibility(Clone)":      //Disables players collider and gives him a different colour
                    PUInvincibilityEnabled = true;
                    playerCollider.enabled = false;
                    player.GetComponent<Renderer>().material.SetColor("_Color",Color.cyan);
                    dropStuff.PlaySound(1);
                    break;
                case "PUPlusDash(Clone)":           //Adds 2 more dashes to the player
                    ShipControl.dashes += 2;
                    dropStuff.PlaySound(2);
                    break;
                case "PUClone(Clone)":              //Instantiates player with disabled colider to a certain distance the varies between which clone, NO MORE THAN 2 CLONES
                    PUCloneEnabled = true;
                    if (player.transform.childCount != 3)
                    {
                        if (((player.transform.childCount ) % 2) == 0)
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
                case "PUBossDamage(Clone)":         //Increases damage done to boss only by using a boolean value
                    PUBossDamageEnabled = true;
                    dropStuff.PlaySound(4);
                    break;

                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    void CheckPowerUps(float timerTemp)
    {
        if (timerTemp <= 0)
        {
            if (ShipControl.fireCooldown != shipControl.FireCooldown)
            {
                PUFireRateEnabled = false;
                ShipControl.fireCooldown = shipControl.FireCooldown;
                foreach (Transform child in player.transform.FindChild("Pivot"))
                {
                    if (child.gameObject.name.Contains("Clone"))
                        Destroy(child.gameObject);
                }
                Debug.Log("Disabled");
            }
            if (player != null)
            {
                if (playerCollider.enabled == false)
                {
                    PUInvincibilityEnabled = false;
                    playerCollider.enabled = true;
                    player.GetComponent<Renderer>().material.color = Color.white;
                    Debug.Log("Disabled");
                }
            }
            if (player.transform.childCount > 1)
            {
                PUCloneEnabled = false;
                foreach (Transform child in player.transform)
                {
                    if (child.gameObject.name != "Pivot")
                    Destroy(child.gameObject);
                }
                Debug.Log("Disabled");
            }
            if (PUBossDamageEnabled == true)
            {
                PUBossDamageEnabled = false;
                Debug.Log("Disabled");
            }
        }
    }

    void FixedUpdate()      //FixedUpdate to avoid Movement and Timer bugs
    {

        CheckPowerUps(DropStuff.Timer);
        if (playerPivot)
        playerPivot.transform.Rotate(0, 0, 30 * Time.deltaTime);

    }
}
