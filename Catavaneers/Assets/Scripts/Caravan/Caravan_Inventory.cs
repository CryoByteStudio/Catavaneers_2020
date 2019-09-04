using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan_Inventory : MonoBehaviour
{
    public int wood;
    public int bandages;
    public int traps;
    public int[] inventory = new int[3];

    void Start()
    {
        
    }
    
    void Update()
    {
        /*
        Purpose:                Makes sure the array is tied to those variables
        Effects:                Allows us to modify these variables instead of the array directly
        Input/Output:           N/A
        Global Variables Used:  N/A
        */

        inventory[0] = wood;
        inventory[1] = bandages;
        inventory[2] = traps;
    }
}
