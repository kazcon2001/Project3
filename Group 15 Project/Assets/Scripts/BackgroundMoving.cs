using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate() //FixedUpdate to avoid movement bug
    {
        if (gameObject.transform.position.x <= -18.12)
        {
            gameObject.transform.position = new Vector3(18.5f, 0, 0.1f);
        }
        _rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
