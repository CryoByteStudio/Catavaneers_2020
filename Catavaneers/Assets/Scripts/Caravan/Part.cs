using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    // Property field
    /*
    Purpose:                Finds out if has Player as parent.
    Effects:                Return true if yes and false if no.
    Input/Output:           Input N/A. Output true/false.
    Global Variables Used:  No variable was altered.
    */
    public bool isPickedUp
    {
        get
        {
            if (transform.parent && transform.GetComponentInParent<CharacterControl>())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Property field
    /*
    Purpose:                Finds out if has Caravan as parent.
    Effects:                Return true if yes and false if no.
    Input/Output:           Input N/A. Output true/false.
    Global Variables Used:  No variable was altered.
    */
    public bool isOnCaravan
    {
        get
        {
            if (transform.parent && transform.GetComponentInParent<Caravan>())
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
    Purpose:                Attach self to provided Transform (in this case, part to caravan).
    Effects:                Part's parent is now caravan and position is the slot's position.
    Input/Output:           Input argument slot transform is determined depending on part before hand. Output N/A.
    Global Variables Used:  transform (Class Parts).
    */
    public void AttachTo(Transform slot_tf)
    {
        DisablePhysics();
        transform.parent = slot_tf;
        transform.position = slot_tf.position;
        transform.rotation = slot_tf.rotation;
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
        ShootOff();
    }

    /*
    Purpose:                Propel the object upward in random XZ direction.
    Effects:                Part is given rigidbody to use physics and shot upward when removed.
    Input/Output:           N/A.
    Global Variables Used:  None.
    */
    void ShootOff()
    {
        EnablePhysics();

        int speed = 10;

        float vox = Random.Range(-1.0f, 1.0f);
        float voy = Random.Range(0.1f, 1.0f);
        float voz = Random.Range(-1.0f, 1.0f);

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(vox * speed, voy * speed, voz * speed);
    }

    /*
    Purpose:                Disable kinematic to use gravity.
    Effects:                Kinematic is disabled.
    Input/Output:           N/A.
    Global Variables Used:  transform (Class Parts).
    */
    void EnablePhysics()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    /*
    Purpose:                Enable kinematic to stop gravity.
    Effects:                Kinematic is enabled..
    Input/Output:           N/A.
    Global Variables Used:  transform (Class Parts).
    */
    void DisablePhysics()
    {
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }
}
