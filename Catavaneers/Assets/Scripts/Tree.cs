using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int Tree_HP;

    Player_Inventory p_inv;

    // Start is called before the first frame update
    void Start()
    {
        p_inv = GetComponent<Player_Inventory>();

        /*
        Purpose:                It's a backup in case we forget to set the tree's health.
        Effects:                Set's a tree's health to 3 in case we forget.
        Input/Output:           N/A.
        Global Variables Used:  Tree_HP.
        */

        if (Tree_HP == 0)
        {
            Tree_HP = 5;
            Debug.Log("You silly goose you, you forgot to set the Tree HP");
        }
    }

    // Update is called every frame
    void Update()
    {
        if (Tree_HP == 0)
            Destroy(gameObject);
    }

    /*
    Purpose:                To collect wood.
    Effects:                Collects a piece of wood and decreases a tree's hp.
    Input/Output:           Fire 1.
    Global Variables Used:  Player_Inventory.wood, Tree_HP.
    */

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player" && Input.GetButtonDown(c.gameObject.GetComponent<Character_interaction>().interact_button_str))
        {
            Tree_HP -= 1;
        }
    }
}
