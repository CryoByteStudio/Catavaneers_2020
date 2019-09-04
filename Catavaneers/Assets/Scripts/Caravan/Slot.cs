using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    #region CORE VARIABLES
    public Transform part_slot_tf;
    public Transform part_tf;

    private int _part_number_int;
    public int part_number_int
    {
        get { return _part_number_int; }
    }

    private bool _is_Empty;
    public bool is_Empty
    {
        get { return (!part_tf) ? _is_Empty = true : _is_Empty = false; }
    }
    #endregion

    #region INITIALIZATION
    void Start()
    {
        FindSlotNumber();
        part_slot_tf = transform;
    }
    #endregion

    void Update()
    {
        part_tf = transform.Find("Caravan_part_" + _part_number_int);
    }


    void FindSlotNumber()
    {
        string[] words = gameObject.name.Split('_');

        foreach (var value in words)
        {
            if (int.TryParse(value, out _part_number_int))
            {
                break;
            }
        }
    }
}
