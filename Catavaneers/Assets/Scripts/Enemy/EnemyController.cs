using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject[] Player;
    public Transform caravan_tf;
    float MoveSpeed = 4f;

    static int newID = 0;
    public int id;

    NavMeshAgent self;

    public float distance;

    public Vector3 randomDestination;

    public bool reachedCaravan = false;

    [SerializeField] public float damage;

    void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        id = newID++;

        if (damage == 0f) damage = 1f;

        caravan_tf = GameObject.FindGameObjectWithTag("Caravan").GetComponent<Transform>();
        
        // Placeholder for better enemy movement
        self = gameObject.AddComponent<NavMeshAgent>();
        self.speed = MoveSpeed;
        self.stoppingDistance = 0.0f;

        randomDestination = GenerateRandomDestination();
    }

    void Update()
    {
        //if (FindObjectOfType<Canvas>().GetComponent<Cycle_Manager>().is_day)
        //{
        //    FindObjectOfType<Spawner>().KillEnemy(spawn_id_int);
        //    Destroy(this.gameObject);
        //}

        RandomMove();
    }

    private void RandomMove()
    {
        distance = self.remainingDistance;

        if (reachedCaravan)
        {
            self.destination = randomDestination;

            if (self.velocity.magnitude <= 0.2f)
            {
                Vector3 toward = randomDestination - transform.position;

                toward.x /= 1000;
                toward.y /= 1000;
                toward.z /= 1000;

                transform.LookAt(randomDestination);

                self.Move(toward);
            }

            if (distance <= 10f)
            {
                randomDestination = GenerateRandomDestination();
                self.destination = randomDestination;
            }
        }
        else
        {
            self.destination = caravan_tf.position;
        }
    }

    private Vector3 GenerateRandomDestination()
    {
        int layerMask = 1 << 8;

        float x, y, z;

        Vector3 randomDestination = Vector3.zero;
        bool hasHit = true;
        int count = 0;

        do
        {
            x = Random.Range(100f, 700f);
            y = Random.Range(20f, 400f);
            z = 0;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, z));
            RaycastHit hit;
            hasHit = Physics.Raycast(ray, out hit, 1000, layerMask);

            if (hasHit)
            {
                return randomDestination = hit.point;
            }

            if (count >= 20)
            {
                randomDestination = Vector3.zero;
                return randomDestination = Vector3.zero;
            }

            count++;

        } while (!hasHit);


        return randomDestination;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Caravan")
        {
            reachedCaravan = true;
            GetComponent<Assault>().DealDamage(c.transform, damage);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Caravan")
        {
            //self.isStopped = false;
        }
    }
}