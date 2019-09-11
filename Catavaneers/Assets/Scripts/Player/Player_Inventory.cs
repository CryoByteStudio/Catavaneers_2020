using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public int GP;
    public int wood;
    public int CaravanPart;
    public int Trap1;
    public int Trap2;
    public int Bandage;
    int[] inventory = new int[6];
    bool has_trap;
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Purpose:                Makes sure you don't cheat and add too many traps/caravan parts to your inventory
        Effects:                Caps the amount to 1
        Input/Output:           N/A
        Global Variables Used:  N/A
        */
        if (Trap1 > 1)
        {
            Trap1 = 1;
            Debug.Log("Hey, it looks like you tried to carry too many traps. Don't worry I'll carry some for you friend, don't want you to trip and fall now do we?");
        }
        if (Trap2 > 1)
        {
            Trap2 = 1;
            Debug.Log("Hey, it looks like you tried to carry too many traps. Don't worry I'll carry some for you friend, don't want you to trip and fall now do we?");
        }
        if (CaravanPart > 1)
        {
            CaravanPart = 1;
            Debug.Log("Don't worry so much my friend, take your time, those Caravan Parts aren't going anywhere");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Purpose:                Makes sure the array is tied to those variables
        Effects:                Allows us to modify these variables instead of the array directly
        Input/Output:           N/A
        Global Variables Used:  N/A
        */
        inventory[0] = GP;
        inventory[1] = wood;
        inventory[2] = CaravanPart;
        inventory[3] = Trap1;
        inventory[4] = Trap2;
        inventory[5] = Bandage;

        //Not sure if we'll ever use this but i wanted to add it just in case
        if (inventory[3] == 1 || inventory[4] == 1)
            has_trap = true;
        else
            has_trap = false;
    }
}
