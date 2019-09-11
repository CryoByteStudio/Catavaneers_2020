using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCaravan : MonoBehaviour
{
    private bool _can_interact;
    public bool can_interact { get { return _can_interact; } }
    Transform caravan_tf;
    Transform part_slot_tf;

    private string interact_button_str;
    Caravan caravan;

    Player_Inventory p_inv;

    Caravan_Inventory c_inv;

    Character_interaction char_interact;

    Health health;

    void Start()
    {
        char_interact = GetComponent<Character_interaction>();
        caravan = GameObject.FindGameObjectWithTag("Caravan").GetComponent<Caravan>();
        p_inv = GetComponent<Player_Inventory>();
        interact_button_str = char_interact.interact_button_str;
    }

    /*
    Purpose:                Interact with caravan (Add and Remove Parts).
    Effects:                Effects from AddToCaravan() and RemoveFromCaravan().
    Input/Output:           Keyboard input "E" to add & "R" to remove. Output N/A.
    Global Variables Used:  caravan_tf, part_slot_tf, caravan, char_interact (Class InteractWithCaravan), 
                            transform, parts_tf (Class Caravan), transform (Class Part), has_object (Class CharacterControl).
    */
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Caravan")
        {
            _can_interact = true;

            caravan_tf = c.gameObject.transform;

            caravan = caravan_tf.GetComponentInParent<Caravan>();

            health = caravan.GetComponent<Health>();

            if (Input.GetButtonDown(interact_button_str) && gameObject.tag == "Player" && p_inv.CaravanPart!=0)
            {
                if (char_interact.has_part)
                {
                    AddToCaravan();
                }
            }
            /*
            Purpose:                Temporary shop code
            Effects:                Allows Player to buy equipment and bandages from the shop
            Input/Output:           Keyboard input "T" to deposit wood, "Y" to buy a trap, "U" to buy bandages
            Global Variables Used:  Player_Inventory.wood, Caravan_Inventory.wood, Player_Inventory.GP, Player_Inventory.Trap1 & Trap2,
                                    Player_Inventory.Bandage, Caravan_Inventory.bandages
            */
            else if (Input.GetButtonDown(interact_button_str) && p_inv.wood>0)
            {
                if (p_inv.wood > 0)
                {
                    c_inv.wood += p_inv.wood;
                    p_inv.Score += 3 * p_inv.wood;
                    p_inv.wood = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (p_inv.GP > 4 && c_inv.traps > 0)
                {
                    if (p_inv.Trap1 < 1)
                    {
                        p_inv.GP = p_inv.GP - 5;
                        p_inv.Trap1 = 1;
                    }
                    else if (p_inv.Trap1 > 0 && p_inv.Trap2 < 1)
                    {
                        p_inv.GP = p_inv.GP - 5;
                        p_inv.Trap2 = 1;
                    }
                    else if (p_inv.Trap1 > 0 && p_inv.Trap2 > 0)
                        Debug.Log("I'm sorry friend, but you're carrying too much right now");
                }
                else if (p_inv.GP < 5)
                    Debug.Log("I'm sorry friend, but it looks like you're a little light on coin");
                else if (c_inv.traps < 1)
                    Debug.Log("I'm sorry friend, but it looks like I'm fresh out of stock");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (p_inv.GP > 0 && c_inv.bandages > 0)
                {
                    p_inv.GP -= 1;
                    p_inv.Bandage += 1;
                    c_inv.bandages -= 1;
                }
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Caravan")
        {
            _can_interact = false;
        }
    }

    /*
    Purpose:                Remove object from caravan.
    Effects:                Part's parent is now attach_point of the capsule (capsule is now grandparent!)
    Input/Output:           N/A.
    Global Variables Used:  caravan_tf, parts_tf, caravan, char_interact (Class Caravan), transform (Class Part), 
                            has_object (Class CharacterControl)
    */
    public void RemoveFromCaravan()
    {
        if (caravan.FindPartSlot())
        {
            Part part = PartToBeRemoved();
            part.Drop();
            caravan.parts_tf.Remove(part.transform);

            if (!health)
                health = caravan.GetComponent<Health>();

            health.Reduce(part.healthValue);
        }
        else Debug.Log("No part attached to caravan");
    }

    public Part PartToBeRemoved()
    {
        if (caravan && caravan.FindPartSlot())
            return caravan.FindPartSlot().GetComponent<Part>();
        else return null;
    }

    /*
    Purpose:                Add object to caravan.
    Effects:                Part's parent is now 1 of 12 transforms children of the caravan (caravan is now grandparent!)
    Input/Output:           N/A.
    Global Variables Used:  caravan_tf, part_slot_tf, caravan, char_interact (Class InteractWithCaravan), transform, parts_tf (Class Caravan),
                            transform (Class Part), has_object (Class CharacterControl), Player_Inventory.CaravanPart,
    */
    public void AddToCaravan()
    {
        Part part = transform.GetChild(0).GetChild(0).GetComponent<Part>();

        part_slot_tf = caravan.FindPartSlot(part.transform);
        part.AttachTo(part_slot_tf);

        caravan.parts_tf.Add(part.transform);
        p_inv.CaravanPart -= 1;

        health.Add(part.healthValue);
    }
}
