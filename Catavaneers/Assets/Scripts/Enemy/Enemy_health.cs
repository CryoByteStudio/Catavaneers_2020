using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_health : MonoBehaviour
{
    [SerializeField] float Health_fl;

    private void Update()
    {
        if(Health_fl<=0)
        {
            Debug.Log("enemy is dead");
            Destroy(this.gameObject);
        }
    }

    /*
    Purpose:                - Deal damage to enemy 
    Effects:                - Deal damage to enemy.
    Input/Output:           Input float damage ; Health_fl ; Output N/A.
    Global Variables Used:  Health_fl.
    */
    public void Take_damage(float damage)
    {
        Health_fl -= damage;
        Debug.Log("enemy takes " + damage + " damage");
        Debug.Log("enemy health: " + Health_fl);
    }
}
