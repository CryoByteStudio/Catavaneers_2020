using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_weapon_damage : MonoBehaviour
{
    [SerializeField] public float damage_fl; //damage player deals

    public bool IsPvp { get { return FindObjectOfType<Cycle_Manager>().pvp_bool; } }

    /*
    Purpose:                - Check for collision with c.gameObject 
                            - If collided with object with tag "Enemy" then deal damage to enemy.
    Effects:                - Deal damage to enemy.
    Input/Output:           Input Collider c. ; damage_fl ; interact_botton_str ; Output N/A.
    Global Variables Used:  damage_fl.
    */
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
                Debug.Log("attacking" + c.gameObject.name);
                c.gameObject.GetComponent<Health>().Reduce(damage_fl);
        }

        if (c.gameObject.tag == "Player" && IsPvp)
        {
            Debug.Log(gameObject.name + " collided with " + c.gameObject.name);
            Debug.Log("can attack player? " + IsPvp);
            Debug.Log("attacking" + c.gameObject.name);
            c.gameObject.GetComponent<Health>().Reduce(damage_fl);
        }
    }
}
