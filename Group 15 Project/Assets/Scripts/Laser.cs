using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour {

    public int speed = 500;
    public string[] ignoreTags;                                         //shots will ignore these tags
    public GameObject explosion;                                        //Effect on enemy or player death

    private Rigidbody2D _rb;                                            //used to move the projectile

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(explosion != null);
    }
	
	
	void FixedUpdate () //Using FixedUpdate to avoid wacky projectile movement bug
    {
        _rb.velocity = new Vector2(0, speed * Time.deltaTime);
        if (gameObject.name == "PlayerShot 1(Clone)")                   //rotates player Shuriken
            gameObject.transform.Rotate(0, 0, 1440 * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool ignore = false;                                            //ignores the following code of the
        foreach (string tag in ignoreTags)                              //colllision made if it was made 
            if (other.tag == tag)                                       //with the object that produced it.
                ignore = true;                                          //So enemies or player can't get killed by their own shots

        if (!ignore)
        {
            bool explode = true;                                        //activates death effect

            if (other.tag == "Enemy") { 
                Score.IncreaseScore();
            }
             else if (other.tag == "Player")
                GameManager.PlayerDied();
             else
            return;

            if (explode)                                                //creates death effect for 1 second
            {
                GameObject fire = (GameObject)Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
                Destroy(fire, 1.0f);
            }

            if (other.gameObject.name == "Boss")                        //if the boss is hit then reduces health and healthbar 
            {
                if (PowerUpsEffects.PUBossDamageEnabled)                //if the "Increased Damage" power up is active then boss takes more damage
                {
                    EnemyBoss.BossHealth--;
                    EnemyBoss.BossHealthBar.transform.localScale = new Vector3 (EnemyBoss.BossHealthBar.transform.localScale.x - EnemyBoss.BossHealthBarScale,1,1);
                }
                EnemyBoss.BossHealth--;
                EnemyBoss.BossHealthBar.transform.localScale = new Vector3(EnemyBoss.BossHealthBar.transform.localScale.x - EnemyBoss.BossHealthBarScale, 1,1);
            }
            else
            Destroy(other.gameObject);                                  //Destroy enemy or player
            Destroy(gameObject);                                        //Destroy projectile


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

    private void CheckWon()                                             //if all enemies are dead including
    {                                                                   //the boss then player has won
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.GetLength(0) <= 1 && EnemyBoss.BossHealth <= 0)
            GameManager.PlayerWon();

    }
}
