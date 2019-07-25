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
    Purpose:                Add rigidbody to object to use Unity physics engine.
    Effects:                Part now has a rigidbody.
    Input/Output:           N/A.
    Global Variables Used:  transform (Class Parts).
    */
    void EnablePhysics()
    {
        transform.gameObject.AddComponent<Rigidbody>();
    }

    /*
    Purpose:                Remove rigidbody of object to disable physics.
    Effects:                Rigidbody component is removed from part.
    Input/Output:           N/A.
    Global Variables Used:  transform (Class Parts).
    */
    void DisablePhysics()
    {
        Destroy(transform.gameObject.GetComponent<Rigidbody>());
    }
}
