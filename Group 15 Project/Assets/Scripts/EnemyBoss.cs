using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private int health;
    public static int BossHealth;

    private void Awake()
    {
        BossHealth = health;
    }

    void FixedUpdate()
    {
        Debug.Log(BossHealth);
        if (BossHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
