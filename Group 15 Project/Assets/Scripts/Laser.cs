using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour {

    public int speed = 500;
    public int damage = 1;
    public string[] ignoreTags;
    public GameObject explosion;

    private Rigidbody2D _rb;
    //private bool okToFire = false;

    DropStuff Drop;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(explosion != null);
    }

	// Use this for initialization
	void Start ()
    {
        
        //_rb.velocity = new Vector2(0, speed * Time.deltaTime);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        _rb.velocity = new Vector2(0, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool ignore = false;
        foreach (string tag in ignoreTags)
            if (other.tag == tag)
                ignore = true;

        if (!ignore)
        {
            bool explode = true;
            if (other.tag == "Enemy") { 
                Score.IncreaseScore();
            }
             else if (other.tag == "Player")
                GameManager.PlayerDied();
             else
            return;
            if (explode)
            {
                GameObject fire = (GameObject)Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
                Destroy(fire, 1.0f);
            }

            if (other.gameObject.name == "Boss")
            {
                EnemyBoss.BossHealth--;
            }
            else
            Destroy(other.gameObject);
            Destroy(gameObject);


            if (other.tag == "Enemy")
            {
                CheckWon();
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }

    private void CheckWon()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.GetLength(0) <= 1)
            GameManager.PlayerWon();
    }
}
