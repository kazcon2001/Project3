using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMotion : MonoBehaviour {

    [SerializeField] private int speed;
    [Tooltip ("Stops enemies from going any further in the X-axis")] [SerializeField] private float xLimit = 4.5f;
    [Tooltip("Offset at end of row transition")] [SerializeField] private Vector2 rowDrop = new Vector2(0.25f,0.5f);

    private Rigidbody2D _rb;        //used to move the enemy
    private GameObject player;      //used to kill player

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(_rb);
    }

    void FixedUpdate ()             //Using FixedUpdate to avoid wacky movement bug
    {
        _rb.velocity = new Vector2(speed * Time.deltaTime, 0); 

        //The following code decides rowDrop x and y and inverts speed based on the direction of the enemy
        if (transform.position.x >= xLimit)                      
        {
            transform.position = new Vector2(transform.position.x - rowDrop.x, transform.position.y - rowDrop.y);
            speed = -speed;
            _rb.velocity = new Vector2(speed * Time.deltaTime, 0);
        }
        else if (transform.position.x <= -xLimit)
        {
            transform.position = new Vector2(transform.position.x + rowDrop.x, transform.position.y - rowDrop.y);
            speed = -speed;
            _rb.velocity = new Vector2(speed * Time.deltaTime, 0);
        }

        //If the enemies go below player height, then the player loses
        if (transform.position.y <= player.transform.position.y)
            GameManager.PlayerDied();
    }

}
