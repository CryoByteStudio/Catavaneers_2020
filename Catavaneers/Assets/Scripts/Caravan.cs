using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan : MonoBehaviour
{
    [SerializeField] public LinkedList<Transform> parts_tf;
    [SerializeField] public int health;

    void Start()
    {
        parts_tf = new LinkedList<Transform>();
    }

    /*
    Purpose:                Get unique transform in the 12 transform children of the caravan using *** NAME *** of object in the argument.
    Effects:                Return transform child[0] for part_1, transform child[1] for part_2 and so on.
    Input/Output:           Input transform part_tf as argument. Output: 1 of 12 child transform of caravan.
    Global Variables Used:  part_tf (Class InteractWithCaracan), transform (Class Caravan).
    */
    public Transform FindPartSlot(Transform part_tf)
    {
        string[] words = part_tf.name.Split('_');

        if (words[1] == "part")
        {
            switch (Int32.Parse(words[2]))
            {
                case 1:
                    return transform.GetChild(0);
                case 2:
                    return transform.GetChild(1);
                case 3:
                    return transform.GetChild(2);
                case 4:
                    return transform.GetChild(3);
                case 5:
                    return transform.GetChild(4);
                case 6:
                    return transform.GetChild(5);
                case 7:
                    return transform.GetChild(6);
                case 8:
                    return transform.GetChild(7);
                case 9:
                    return transform.GetChild(8);
                case 10:
                    return transform.GetChild(9);
                case 11:
                    return transform.GetChild(10);
                case 12:
                    return transform.GetChild(11);
                default:
                    Debug.Log("Can't find part slot...");
                    return null;
            }
        }
        else
        {
            Debug.Log("Can't find part slot...");
            return null;
        }
    }

    /*
    Purpose:                Get transform of the last object in the parts_tf linked list of caravan.
    Effects:                Return transform of the last part in the linked list of parts that are attached to the caravan.
    Input/Output:           Input N/A. Output part_tf last value or null.
    Global Variables Used:  part_tf (Class InteractWithCaracan).
    */
    public Transform FindPartSlot()
    {
        return (parts_tf.Count > 0) ? parts_tf.Last.Value : null;
    }
}
