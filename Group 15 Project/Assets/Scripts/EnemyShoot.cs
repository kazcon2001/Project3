using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {

    public GameObject shotPrefab;
    [Tooltip("Time between shots in (min, max) (X = min, Y = max)")] public Vector2 timeBetweenShots = new Vector2(0.5f,1.0f);
    public float speed = 1;                                     //speed multiplier for projectile

    private float nextShot = -1;                                //nextShot decides time to shoot  
    private bool okToFire = false;                              //decides enemies ability to shoot depending on their visibility
                                                                //e.g if enemy is behind the 2D background or dead

    void Awake()
    {
        Debug.Assert(shotPrefab.GetComponent<Laser>() != null);
        Random.seed = (int)Time.realtimeSinceStartup;
    }

	void Start () 
    {
        nextShot = Time.time + Random.Range(timeBetweenShots.x, timeBetweenShots.y);	    
	}
	
	void FixedUpdate ()                                         //Fixed Update to avoid wacky movement bug
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
