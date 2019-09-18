using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startHealth;
    [SerializeField] float respawn_time_fl = 10.0f;
    [SerializeField] float respawn_health_ratio_fl = 1.0f;

    [SerializeField] public float health;

    public float maxHealth;

    public bool IsDead { get { return health <= 0; } }
    
    public bool IsIronCat { get { return FindObjectOfType<Cycle_Manager>().iron_cat_bool; } }

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

    private void Update()
    {
        if (IsDead && gameObject.tag != "Caravan")
        {
            Debug.Log(gameObject.name + " is dead");
            Die();
        }
    }

    public void Reduce(float amount)
    {
        health = Mathf.Max(health - amount, 0);
        Debug.Log(gameObject.name + " health = " + health);

        if (IsDead && gameObject.tag != "Caravan")
        {
            Debug.Log(gameObject.name + " is dead");
            Die();
        }
    }

    private void Die()
    {
        if(gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            FindObjectOfType<EnemyManager>().DestroyEnemy(gameObject);
            Destroy(gameObject, 0.5f);
        }

        if(gameObject.tag == "Player")
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Transform respanw_position = GetComponent<PlayerAI>().caravan_attach_point;
            transform.position = respanw_position.position;
            Debug.Log(gameObject.name + " position is " + transform.position);            
            if (!IsIronCat) { StartCoroutine(SetActive()); };
        }
    }

    IEnumerator SetActive()
    {
        SetHealth(startHealth * respawn_health_ratio_fl);
        yield return new WaitForSeconds(respawn_time_fl);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
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
