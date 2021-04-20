using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour {

    public int speed = 500;
    public string[] ignoreTags;
    public GameObject explosion;

    private Rigidbody2D _rb;

    DropStuff Drop;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(explosion != null);
    }
	
	
	void FixedUpdate () //Using FixedUpdate to avoid wacky projectile movement bug
    {
        _rb.velocity = new Vector2(0, speed * Time.deltaTime);
        if (gameObject.name == "PlayerShot 1(Clone)")
            gameObject.transform.Rotate(0, 0, 1440 * Time.deltaTime);

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
                if (PowerUpsEffects.PUBossDamageEnabled)
                {
                    EnemyBoss.BossHealth--;
                    EnemyBoss.BossHealthBar.transform.localScale = new Vector3 (EnemyBoss.BossHealthBar.transform.localScale.x - EnemyBoss.BossHealthBarScale,1,1);
                }
                EnemyBoss.BossHealth--;
                EnemyBoss.BossHealthBar.transform.localScale = new Vector3(EnemyBoss.BossHealthBar.transform.localScale.x - EnemyBoss.BossHealthBarScale, 1,1);
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
        if (enemies.GetLength(0) <= 1 && EnemyBoss.BossHealth <= 0)
            GameManager.PlayerWon();

    }
}
