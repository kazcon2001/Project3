 using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControl : MonoBehaviour 
{
    public GameObject shotPrefab;       //Player Projectile
    public GameObject swordPrefab;      //Player Projectile on PUBossDamage power up;

    public float moveSpeed;
    [Tooltip("Stops the player from going any further in the X-axis")] public float leftLimit, rightLimit;

    public int dashCount = 3;
    public static int dashes;
    public float dashDistance;

    public float FireCooldown;
    public static float fireCooldown;
    private float timer = 0;            //Fire Cooldown timer not to be mistaken with Game Timer

    private AudioSource audioSource;    //Used for Dash SFX

    private Rigidbody2D _rb;            //Used to move the player

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
        Debug.Assert(shotPrefab.GetComponent<Laser>() != null);
    }

	void Start () 
    {
        dashes = dashCount;
        fireCooldown = FireCooldown;
	}

    void FixedUpdate()   //Using FixedUpdate to avoid wacky movement bug
    {
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0);

        if (transform.position.x <= leftLimit && _rb.velocity.x < 0)                                    //If player tries to go off bounds
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);    //then this code places the player
        else if (transform.position.x >= rightLimit && _rb.velocity.x > 0)                              //back inside bounds. Also works
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);   //with player dashing out

    }

    void Update () 
    {
        GameManager.PlayerPaused();

        timer += Time.deltaTime;

        //Player Shoot Code, won't work if cooldown timer is not ready
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetButtonDown("Fire1") && GameManager.gamePaused == false && fireCooldown <= timer)
        {
            timer = 0;

            //shot changes to sword slash if the player has activated PUBossDamage (rotated so it can have a proper angle)
            if (PowerUpsEffects.PUBossDamageEnabled)
            {
                GameObject shot = Instantiate(swordPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 72))) as GameObject;
                Debug.Assert(shot);
            }
            else
            {
                GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity) as GameObject;
                Debug.Assert(shot);
            }
        }

        //Player Dash code, only works if player is moving
        //Teleports the player away from his previous spot depending on direction and reduces the amount of dashes
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetButtonDown("Dash") && dashes > 0 )
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                audioSource.PlayOneShot(audioSource.clip);
                transform.position = new Vector3(transform.position.x + dashDistance, transform.position.y, transform.position.z);
                dashes--;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                audioSource.PlayOneShot(audioSource.clip);
                transform.position = new Vector3(transform.position.x - dashDistance, transform.position.y, transform.position.z);
                dashes--;
            }
        }
	}
}
