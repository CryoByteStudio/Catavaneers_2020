using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan : MonoBehaviour
{
    [SerializeField] public List<Transform> parts_tf;
    
    public bool IsFullPart
    {
        get { return parts_tf.Count == 12; }
    }

    void Start()
    {
        parts_tf = new List<Transform>();
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
            int part_num = Int32.Parse(words[2]);
            return transform.GetChild(part_num - 1);
        }
        else
        {
            Debug.Log("Can't find part slot...");
            return null;
        }
    }

    /*
    Purpose:                //Get a random transform of the object in the parts_tf list of caravan.
                            Get first element in parts_tf.
    Effects:                Return transform of the last part in the list of parts that are attached to the caravan.
    Input/Output:           Input N/A. Output part_tf last value or null.
    Global Variables Used:  part_tf (Class InteractWithCaracan).
    */
    public Transform FindPartSlot()
    {
        //return (parts_tf.Count > 0) ? parts_tf[UnityEngine.Random.Range(0, parts_tf.Count)] : null;
        return (parts_tf.Count > 0) ? parts_tf[0] : null;
    }
}
