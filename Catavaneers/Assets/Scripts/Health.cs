using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;

    public Health(int health)
    {
        this.health = health;
    }

    private void Start()
    {
        if (health == 0)
        {
            if (gameObject.tag == "Player") health = 5;
            else if (gameObject.tag == "Enemy") health = 2;
        }
    }

    public void Reduce(int amount)
    {
        health -= amount;
    }

    public void Add(int amount)
    {
        health += amount;
    }

    public void SetHealth(int amount)
    {
        health = amount;
    }
}
