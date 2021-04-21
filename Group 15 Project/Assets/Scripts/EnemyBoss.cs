using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private int health;        
    public static int BossHealth;
    public static GameObject BossHealthBar;     //Boss Health Bar
    public static float BossHealthBarScale;     //how much the reduction per hit

    void Awake()
    {
        BossHealth = health;
        BossHealthBarScale = 1f / health;
    }

    void Start()
    {
        BossHealthBar = GameObject.Find("BossHealth").transform.FindChild("Bar").transform.FindChild("Health").gameObject;
    }

    void Update()
    {
        if (BossHealth <= 0)
            Destroy(gameObject);
    }
}
