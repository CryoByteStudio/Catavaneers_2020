using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    
    /*
    Purpose:                Attach self to provided Transform (in this case, part to caravan).
    Effects:                Part's parent is now caravan and position is the slot's position.
    Input/Output:           Input argument slot transform is determined depending on part before hand. Output N/A.
    Global Variables Used:  transform (Class Parts).
    */
    public void AttachTo(Transform slot_tf)
    {
        transform.parent = slot_tf;
        transform.position = slot_tf.position;
    }

    
    /*
    Purpose:                Remove object from parent.
    Effects:                Part's parent is now null.
    Input/Output:           N/A.
    Global Variables Used:  transform (Class Parts).
    */
    public void Drop()
    {
        transform.parent = null;
    }
}
