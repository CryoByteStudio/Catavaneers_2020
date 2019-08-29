using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    #region VARIABLES
    public enum States
    {
        FixingCaravan, Idle, Attack
    }

    [SerializeField] Transform caravan_attach_point;

    [SerializeField] EnemyManager enemyManager;

    [SerializeField] int enemyThreshold;

    NavMeshAgent self;

    List<GameObject> parts_list;

    List<GameObject> targets_list;

    public States states = new States();

    public bool has_part;

    public bool isAttached;

    public bool IsAttached
    {
        get { return isAttached; }
        set { isAttached = value; }
    }

    [SerializeField] float attackInterval;

    float timeSinceLastAttack = Mathf.Infinity;

    #endregion

    #region INITIATE
    private void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        if (!caravan_attach_point)
            Debug.LogError("No reference to caravan attach point");

        if (!enemyManager)
            enemyManager = FindObjectOfType<EnemyManager>();

        if (enemyThreshold == 0) enemyThreshold = 20;

        if (attackInterval == 0f) attackInterval = 1f;

        GameObject[] parts = GameObject.FindGameObjectsWithTag("Part");
        parts_list = new List<GameObject>();
        targets_list = new List<GameObject>();

        self = GetComponent<NavMeshAgent>();

        foreach (GameObject part in parts)
        {
            part.transform.parent = null;
            parts_list.Add(part);
        }
    }
    #endregion

    private void Update()
    {
        has_part = GetComponent<CharacterControl>().has_part;

        if (IsAttached) { AttachSelf(caravan_attach_point); return; }
        else DetachSelf();

        if (GetComponent<CharacterControl>().player_active_bl)
        {
            self.isStopped = true;
            return;
        }
        else
        {
            self.isStopped = false;
        }

        Automate();
    }

    #region DRIVER
    private void Automate()
    {
        Caravan caravan = GameObject.FindGameObjectWithTag("Caravan").GetComponent<Caravan>();

        if ((!caravan.IsFullPart && (CheckPartsAvailability() || has_part) && enemyManager.GetEnemyCount() < enemyThreshold) || has_part)
        {
            states = States.FixingCaravan;
        }
        else if (enemyManager.GetEnemyCount() == 0)
        {
            states = States.Idle;
        }
        else
        {
            states = States.Attack;
        }

        switch (states)
        {
            case States.FixingCaravan:
                FixCaravan();
                break;
            case States.Idle:
                ReturnToCaravan();
                break;
            case States.Attack:
                Attack();
                break;
        }
    }
    #endregion

    #region IDLE
    private void ReturnToCaravan()
    {
        self.stoppingDistance = 0.0f;
        self.destination = caravan_attach_point.position;
    }
    #endregion

    #region FIXING CARAVAN
    private void FixCaravan()
    {
        PickUpPart();
        AttachPartToCaravan();
    }

    private Vector3 FindClosestPart()
    {
        Vector3 closest_part_position = transform.position;
        float smallestDistance;
        float distance;

        if (parts_list.Count > 0)
        {
            for (int i = 0; i < parts_list.Count; i++)
            {
                if (parts_list[i].transform.parent == null)
                {
                    closest_part_position = parts_list[i].transform.position;
                    break;
                }
            }

            for (int i = 0; i < parts_list.Count; i++)
            {
                smallestDistance = Vector3.Distance(transform.position, closest_part_position);
                distance = Vector3.Distance(transform.position, parts_list[i].transform.position);

                if (distance < smallestDistance && parts_list[i].transform.parent == null)
                    closest_part_position = parts_list[i].transform.position;
                
            }
        }
        
        return closest_part_position;
    }

    private bool CheckPartsAvailability()
    {
        for (int i = 0; i < parts_list.Count; i++)
        {
            if (parts_list[i].transform.parent == null)
                return true;
        }

        return false;
    }

    private void PickUpPart()
    {
        if (!GetComponent<CharacterControl>().has_part)
        {
            self.stoppingDistance = 0.0f;
            self.destination = FindClosestPart();
        }
    }

    private void AttachPartToCaravan()
    {
        if (GetComponent<CharacterControl>().has_part)
        {
            Caravan caravan = GameObject.FindGameObjectWithTag("Caravan").GetComponent<Caravan>();

            self.stoppingDistance = 0.0f;
            self.destination = caravan.transform.position;

            if (GetComponent<InteractWithCaravan>().CanInteract)
                GetComponent<InteractWithCaravan>().AddToCaravan();
        }
    }
    #endregion

    #region MOVE WITH CARAVAN
    private void AttachSelf(Transform destination)
    {
        GetComponent<CharacterControl>().enabled = false;
        transform.position = destination.position;
    }

    private void DetachSelf()
    {
        GetComponent<CharacterControl>().enabled = true;
    }
    #endregion

    #region ATTACK
    private Transform FindClosestTarget()
    {
        // Find target based on game state (******** NOT IMPLEMENTED ********)

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        targets_list.Clear();
        for (int i = 0; i < enemies.Length; i++)
        {
            targets_list.Add(enemies[i]);
        }

        Transform closest_target = null;
        Vector3 closest_target_position = transform.position;
        float smallest_distance;
        float current_distance;

        if (targets_list.Count > 0)
        {
            closest_target_position = targets_list[0].transform.position;
            closest_target = targets_list[0].transform;

            for (int i = 0; i < targets_list.Count; i++)
            {
                smallest_distance = Vector3.Distance(transform.position, closest_target_position);
                current_distance = Vector3.Distance(transform.position, targets_list[i].transform.position);

                if (current_distance < smallest_distance)
                {
                    closest_target_position = targets_list[i].transform.position;
                    closest_target = targets_list[i].transform;
                }
            }
        }

        return closest_target;
    }

    private void Attack()
    {
        Assault assault = GetComponent<Assault>();

        Transform target = FindClosestTarget();

        self.destination = target.position;

        self.stoppingDistance = 1f;

        if (Vector3.Distance(transform.position, target.position) <= 2.5f && CanAttack())
        {
            timeSinceLastAttack = 0f;
            assault.DealDamage(target, GetComponent<CharacterControl>().damage);
        }
    }

    private bool CanAttack()
    {
        timeSinceLastAttack += Time.deltaTime;

        return timeSinceLastAttack >= attackInterval;
    }

    #endregion
}