using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Functions : MonoBehaviour
{

    public Transform Player;
    public Transform caravan_tf;
    int MoveSpeed = 4;
    //int MaxDist = 10;
    int MinDist = 3;

    public int spawn_id_int;



    void Start()
    {
        spawn_id_int = FindObjectOfType<Caravan>().GetComponentInChildren<Spawner>().SpawnID;
        caravan_tf = FindObjectOfType<Caravan>().GetComponent<Transform>();
    }

    void Update()
    {
        if (FindObjectOfType<Canvas>().GetComponent<Cycle_Manager>().is_day)
        {
            FindObjectOfType<Spawner>().KillEnemy(spawn_id_int);
            Destroy(this.gameObject);
        }
        transform.LookAt(caravan_tf);

        if (Vector3.Distance(transform.position, caravan_tf.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }
        
    }


}
