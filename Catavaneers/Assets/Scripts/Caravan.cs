using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravan : MonoBehaviour
{
    #region CORE VARIABLES
    [SerializeField]
    public LinkedList<Transform> parts_tf;
    [HideInInspector]
    public GameObject[] slot_go;

    private int part_health_value_int;
    // Public for debug, change to private when sure.
    public int _health_int;
    [SerializeField]
    public int health_int
    {
        get { return _health_int; }
    }
    #endregion

    #region INITIALIZATION
    void Start()
    {
        part_health_value_int = 2;
        parts_tf = new LinkedList<Transform>();
        slot_go = GameObject.FindGameObjectsWithTag("Slot");
    }
    #endregion

    #region DEBUG
    public int partCount;
    void Update()
    {
        partCount = parts_tf.Count;
    }
    #endregion

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
            int part_no_int = Int32.Parse(words[2]) - 1;
            return slot_go[part_no_int].GetComponent<Slot>().part_slot_tf;
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

    /*
    Purpose:                Update health depending on part counts.
    Effects:                Health is updated based on part counts.
    Input/Output:           Input N/A. Output N/A
    Global Variables Used:  _health_int, part_health_value_int (Class Caravan).
    */
    public void UpdateHealth()
    {
        _health_int = parts_tf.Count * part_health_value_int;
    }

    /*
    Purpose:                Reduce health based on input damage and DetachPart().
    Effects:                Health is reduced and part(s) is removed based on input damage.
    Input/Output:           Input int damage. Output N/A.
    Global Variables Used:  _health_int (Class Caravan), variables from DetachPart() (Class Caravan).
    */
    public void TakeDamage(int damage)
    {
        if (_health_int >= damage)
        {
            _health_int -= damage;
            DetachPart();
        }
        else
        {
            _health_int = 0;
            DetachPart();
        }
    }

    /*
    Purpose:                Detach part randomly based on health of caravan.
    Effects:                Part(s) is detached randomly depending on caravan health.
    Input/Output:           Input N/A. Output N/A.
    Global Variables Used:  _health_int (Class Caravan), parts_tf (Class Caravan).
    */
    public void DetachPart()
    {
        while (parts_tf.Count > 0)
        {
            if ((_health_int + 1) / 2 == parts_tf.Count)
            {
                break;
            }
            else
            {
                int j = Mathf.FloorToInt(UnityEngine.Random.Range(0.0f, 1.0f));
                if (j == 0)
                {
                    parts_tf.Last.Value.GetComponent<Part>().Drop();
                    parts_tf.RemoveLast();
                }
                else
                {
                    parts_tf.First.Value.GetComponent<Part>().Drop();
                    parts_tf.RemoveFirst();
                }
            }
        }
    }
}