 using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipControl : MonoBehaviour 
{
    public GameObject shotPrefab;
    public float moveSpeed;
    [Tooltip("Invisible barrier that stops the player from going any further in the X-axis")] public float leftLimit, rightLimit;
    public int dashCount = 3;
    public static int dashes;
    public float dashDistance;
    public float FireCooldown;
    public static float fireCooldown;
    private float timer = 0;

    private AudioSource audioSource;

    private Rigidbody2D _rb;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
        Debug.Assert(shotPrefab.GetComponent<Laser>() != null);
    }

	// Use this for initialization
	void Start () 
    {
        dashes = dashCount;
        fireCooldown = FireCooldown;
	}

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0);

        if (transform.position.x <= leftLimit && _rb.velocity.x < 0)
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
        else if (transform.position.x >= rightLimit && _rb.velocity.x > 0)
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update () 
    {   
        GameManager.PlayerPaused();
        timer += Time.deltaTime;

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetButtonDown("Fire1") && GameManager.gamePaused == false && fireCooldown <= timer)
        {
            timer = 0;
            GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity) as GameObject;
            Debug.Assert(shot);
        }

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
