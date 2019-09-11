using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startHealth;

    [ReadOnly] public float health;

    float maxHealth;

    public bool IsDead { get { return health <= 0; } }

    public Health(float amount)
    {
        startHealth = amount;
    }

    private void Start()
    {
        if (startHealth == 0)
        {
            if (gameObject.tag == "Player") { startHealth = 5; }
            else if (gameObject.tag == "Enemy") { startHealth = 2; }
            else if (gameObject.tag == "Caravan") { maxHealth = Mathf.Infinity; return; }
        }
        
        health = startHealth;
        maxHealth = startHealth;
    }

    public void Reduce(float amount)
    {
        health = Mathf.Max(health - amount, 0);

        if (IsDead && gameObject.tag != "Caravan")
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        FindObjectOfType<EnemyManager>().DestroyEnemy(gameObject);
        Destroy(gameObject, 0.5f);
    }

    public void Add(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    public void SetHealth(float amount)
    {
        health = amount;
    }
}
