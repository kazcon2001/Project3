using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {

    public GameObject shotPrefab;
    [Tooltip("Time between shots in (min, max) (X = min, Y = max)")]public Vector2 timeBetweenShots = new Vector2(0.5f,1.0f);
    public float speed = 1;

    private float nextShot = -1;
    private bool okToFire = false;

    GameManager gameManager;

    void Awake()
    {
        Debug.Assert(shotPrefab.GetComponent<Laser>() != null);
        Random.seed = (int)Time.realtimeSinceStartup;
    }

	// Use this for initialization
	void Start () 
    {
        nextShot = Time.time + Random.Range(timeBetweenShots.x, timeBetweenShots.y);	    
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    if(okToFire && nextShot < Time.time)
        {
            nextShot = Time.time + Random.Range(timeBetweenShots.x, timeBetweenShots.y);
            Instantiate(shotPrefab, transform.position, Quaternion.identity);
        }
	}

    void OnBecameVisible()
    {
        okToFire = true;
    }

    void OnBecameInvisible()
    {
        okToFire = false;
    }

}
