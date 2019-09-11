using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_interaction : MonoBehaviour
{
    [SerializeField] Transform attach_tf;
    public string interact_button_str = "Primary_interact_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly
    [SerializeField] public float damage_fl; //damage player deals

    Player_Inventory p_inv;
    public string collider_part;


    void Start()
    {
        p_inv = GetComponent<Player_Inventory>();
    }

    void Update()
    {
        
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
    Purpose:                - Check for collision with c.gameObject 
                            - If collided with object with tag "Enemy" then deal damage to enemy.
    Effects:                - Deal damage to enemy.
    Input/Output:           Input Collider c. ; damage_fl ; interact_button_str ; Output N/A.
    Global Variables Used:  damage_fl.
    */
    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            if (Input.GetButtonDown(interact_button_str))
            {
                Debug.Log("attack");
                c.gameObject.GetComponent<Enemy_health>().Take_damage(damage_fl);
            }
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
        if (c.gameObject.tag == "Tree" && Input.GetButtonDown(interact_button_str)) //Look man, ive tried "Fire 1" but i couldnt seem to get it to work so this'll be a temperary thing until it works ~Silas
        {
            p_inv.wood += 5;
        }
    }

}
