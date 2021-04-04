using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMotion : MonoBehaviour {

    [SerializeField] private int speed;
    [Tooltip ("Invisible barrier that stops enemies from going any further in the X-axis")] [SerializeField] private float xLimit = 4.5f;
    [Tooltip("offset at end of row transition")]
    [SerializeField] private Vector2 rowDrop = new Vector2(0.25f,0.5f);

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Assert(_rb);
    }

	// Use this for initialization
	void Start () 
    {
        

	}

    // Update is called once per frame
    void FixedUpdate ()
    {
        _rb.velocity = new Vector2(speed * Time.deltaTime, 0);

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

    }

}
