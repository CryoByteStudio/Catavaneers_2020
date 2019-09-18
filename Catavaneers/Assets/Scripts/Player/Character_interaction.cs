using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_interaction : MonoBehaviour
{
    #region variables
    [SerializeField] Transform attach_tf; //attach point prfab in the player
    [SerializeField] public float damage_fl; //damage player deals
    [SerializeField] GameObject Weapon; //weapon refab attached to player
    [SerializeField] float attack_speed_fl; //attack speed of player

    public string interact_botton_str = "Primary_interact_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly
    private float last_attack; //time since last attack
    Player_Inventory p_inv;
    public string collider_part;
    #endregion


    void Start()
    {
        p_inv = GetComponent<Player_Inventory>();
    }

    private void Update()
    {
        last_attack += Time.deltaTime;

        if (Input.GetButtonDown(interact_botton_str) && !has_part)
        {
            Attack_Behaviour();
        }
    }

    private void Attack_Behaviour()
    {
        if (last_attack > attack_speed_fl)
        {
            Weapon.SetActive(true);
            last_attack = 0;
            StartCoroutine(weapon_put_away());
        }

    }

    IEnumerator weapon_put_away()
    {
        yield return new WaitForSeconds(0.5f);
        Weapon.SetActive(false);
    }



    // Property field
    /*
    Purpose:                Finds out if attach point has "Part" object as child.
    Effects:                Return true if yes and false if no.
    Input/Output:           Input N/A. Output true/false.
    Global Variables Used:  No variable was altered.
    */
    public bool has_part
    {
        get
        {
            if (attach_tf.childCount != 0 && attach_tf.GetComponentInChildren<Part>())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /*
    Purpose:                Attach object to player's child transform attach_point as child and reposition to attach_point's position.
    Effects:                Part that was picked up is now child of player's child transform attach_point (Player is a grandparent!).
                            Will not be transfered to other player if collided while being held or attached to caravan
    Input/Output:           Input Collider c. Output N/A.
    Global Variables Used:  transform of Collider c's gameObject, Player_Inventory.CaravanPart,
    */
    void PickUpPart(Collider c)
    {
        Part part = c.GetComponent<Part>();

        if (!part.isOnCaravan && !part.isPickedUp)
        {
            c.GetComponent<Part>().AttachTo(transform.GetChild(0));
            p_inv.CaravanPart += 1; //I added this for the sake of inventory ~Silas
        }
    }

    /*
    Purpose:                Check for trigger if collided with object with tag "Part" and has_object is false to pick that object up.
    Effects:                PickUpPart().
    Input/Output:           Input Collider c. Output N/A.
    Global Variables Used:  See PickUpPart().
    */
    void OnTriggerEnter(Collider c)
    {
        if (!has_part && c.gameObject.tag == "Part")
        {
            PickUpPart(c);
        }
        else if (has_part)
        {
            Debug.Log(transform.name + " is holding a part");
        }
    }


    /*
    Purpose:                To collect wood.
    Effects:                Collects a piece of wood and decreases a tree's hp.
    Input/Output:           Fire 1.
    Global Variables Used:  Player_Inventory.wood, Tree.Tree_HP.
    */

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Tree" && Input.GetButtonDown(interact_botton_str)) //Look man, ive tried "Fire 1" but i couldnt seem to get it to work so this'll be a temperary thing until it works ~Silas
        {
            p_inv.wood += 5;
        }
    }

}
