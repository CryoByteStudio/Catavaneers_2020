using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Functions : MonoBehaviour
{
    public GameObject[] Player;
    public Transform caravan_tf;
    int MoveSpeed = 4;
    //int MaxDist = 10;
    int MinDist = 3;

    public int spawn_id_int;

    NavMeshAgent self;

    void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        caravan_tf = GameObject.FindGameObjectWithTag("Caravan").GetComponent<Transform>();
        
        // Placeholder for better enemy movement
        self = gameObject.AddComponent<NavMeshAgent>();
        self.speed = MoveSpeed;
        self.stoppingDistance = 10f;
        self.destination = caravan_tf.position;
    }

    void Update()
    {
        //if (FindObjectOfType<Canvas>().GetComponent<Cycle_Manager>().is_day)
        //{
        //    FindObjectOfType<Spawner>().KillEnemy(spawn_id_int);
        //    Destroy(this.gameObject);
        //}

        //transform.LookAt(caravan_tf);

        //if (Vector3.Distance(transform.position, caravan_tf.position) >= MinDist)
        //{

        ////transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //}

        if (Vector3.Distance(self.transform.position, caravan_tf.position) <= 10f)
        {
            self.isStopped = true;
        }
        else
        {
            self.isStopped = false;
        }
    }


}
