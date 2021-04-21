using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    private Rigidbody2D _rb;                //used to move the background
    private SpriteRenderer sprite;
    [SerializeField] private float speed;
    private static float initPosition1 = 2.06637f;
    private static float initPosition2 = 23.79644f;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        //2.06637
        //23.79644
    }
    private void Start()
    {
        if (gameObject.name.Contains("1"))
            initPosition1 = sprite.transform.position.x;
        else
        {
            gameObject.transform.position = new Vector3(sprite.bounds.size.x + initPosition1, gameObject.transform.position.y, gameObject.transform.position.z);
            initPosition2 = sprite.transform.position.x;
        }
    }
    void FixedUpdate()                      //FixedUpdate to avoid movement bug
    {
        if (gameObject.transform.position.x <= -19.66244)// - (sprite.bounds.size.x - initPosition1))//-19.66244
        {
            gameObject.transform.position = new Vector3(23.39644f, 0, 0.3f); //23.39644f
        }
        _rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
