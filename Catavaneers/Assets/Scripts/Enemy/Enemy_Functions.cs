using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Functions : MonoBehaviour
{

    public Transform Player;
    public Transform Caravan;
    float MoveSpeed = 4f;
    //int MaxDist = 10;
    float MinDist = 5f;



    void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        // Placeholder for better enemy movement
        NavMeshAgent self = gameObject.AddComponent<NavMeshAgent>();
        self.speed = MoveSpeed;
        self.stoppingDistance = MinDist;
        self.destination = Caravan.position;
    }

    void Update()
    {
        //transform.LookAt(Caravan);

        //if (Vector3.Distance(transform.position, Caravan.position) >= MinDist)
        //{

        //transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //}
    }
}
