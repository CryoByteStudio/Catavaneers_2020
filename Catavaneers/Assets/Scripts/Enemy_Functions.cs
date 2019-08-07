using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Functions : MonoBehaviour
{

    public Transform Player;
    public Transform Caravan;
    int MoveSpeed = 4;
    //int MaxDist = 10;
    int MinDist = 5;




    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Caravan);

        if (Vector3.Distance(transform.position, Caravan.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }
    }
}
