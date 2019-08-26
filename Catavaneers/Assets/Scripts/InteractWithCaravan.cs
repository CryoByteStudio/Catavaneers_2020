using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCaravan : MonoBehaviour
{
    Transform caravan_tf;
    Transform part_slot_tf;

    [SerializeField] string Place_Part_str = "Place_Part_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly

    Caravan caravan;

    Player_Inventory p_inv;

    Caravan_Inventory c_inv;

    CharacterControl char_control;

    void Start()
    {
        char_control = GetComponent<CharacterControl>();
        p_inv = GetComponent<Player_Inventory>();
    }

    /*
    Purpose:                Interact with caravan (Add and Remove Parts).
    Effects:                Effects from AddToCaravan() and RemoveFromCaravan().
    Input/Output:           Keyboard input "E" to add & "R" to remove. Output N/A.
    Global Variables Used:  caravan_tf, part_slot_tf, caravan, char_control (Class InteractWithCaravan), 
                            transform, parts_tf (Class Caravan), transform (Class Part), has_object (Class CharacterControl).
    */
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Caravan")
        {
            caravan_tf = c.gameObject.transform;

            caravan = caravan_tf.GetComponentInParent<Caravan>();

            if (Input.GetButtonDown(Place_Part_str))
            {
                if (char_control.has_part)
                {
                    AddToCaravan();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                //if (!char_control.has_part)
                {
                    RemoveFromCaravan();
                }
            }
            /*
            Purpose:                Temporary shop code
            Effects:                Allows Player to buy equipment and bandages from the shop
            Input/Output:           Keyboard input "T" to deposit wood, "Y" to buy a trap, "U" to buy bandages
            Global Variables Used:  Player_Inventory.wood, Caravan_Inventory.wood, Player_Inventory.GP, Player_Inventory.Trap1 & Trap2,
                                    Player_Inventory.Bandage, Caravan_Inventory.bandages
            */
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (p_inv.wood > 0)
                {
                    c_inv.wood += p_inv.wood;
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

    /*
    Purpose:                Remove object from caravan.
    Effects:                Part's parent is now attach_point of the capsule (capsule is now grandparent!)
    Input/Output:           N/A.
    Global Variables Used:  caravan_tf, parts_tf, caravan, char_control (Class Caravan), transform (Class Part), 
                            has_object (Class CharacterControl)
    */
    public void RemoveFromCaravan()
    {
        Transform part_tf = caravan.FindPartSlot();

        if (part_tf != null)
        {
            part_tf.GetComponent<Part>().Drop();
            caravan.parts_tf.RemoveLast();
        }
        else Debug.Log("No part attached to caravan");
    }

    /*
    Purpose:                Add object to caravan.
    Effects:                Part's parent is now 1 of 12 transforms children of the caravan (caravan is now grandparent!)
    Input/Output:           N/A.
    Global Variables Used:  caravan_tf, part_slot_tf, caravan, char_control (Class InteractWithCaravan), transform, parts_tf (Class Caravan),
                            transform (Class Part), has_object (Class CharacterControl), Player_Inventory.CaravanPart,
    */
    public void AddToCaravan()
    {
        Transform part_tf = transform.GetChild(0).GetChild(0);

        part_slot_tf = caravan.FindPartSlot(part_tf);
        part_tf.GetComponent<Part>().AttachTo(part_slot_tf);

        caravan.parts_tf.AddFirst(part_tf);
        p_inv.CaravanPart -= 1;
    }
}
