using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    public enum States
    {
        FixingCaravan, Idle
    }

    [SerializeField] Transform caravan_attach_point;

    NavMeshAgent agent;

    List<GameObject> parts_list;

    public States states = new States();

    public bool has_part;

    public bool isAttached;

    public bool IsAttached
    {
        get { return isAttached; }
        set { isAttached = value; }
    }

    private void Start()
    {
        if (!caravan_attach_point)
            Debug.LogError("No reference to caravan attach point");
        Initiate();
    }

    private void Initiate()
    {
        GameObject[] parts = GameObject.FindGameObjectsWithTag("Part");
        parts_list = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();

        foreach (GameObject part in parts)
        {
            part.transform.parent = null;
            parts_list.Add(part);
        }
    }

    private void Update()
    {
        has_part = GetComponent<Character_interaction>().has_part;

        if (IsAttached) { AttachSelf(caravan_attach_point); return; }
        else DetachSelf();

        if (!GetComponent<Character_control>().player_active_bl) Automate();
    }

    private void Automate()
    {
        if (!GameObject.FindGameObjectWithTag("Caravan").GetComponent<Caravan>().isFullPart && (CheckPartsAvailability() || has_part))
        {
            states = States.FixingCaravan;
        }
        else
        {
            states = States.Idle;
        }

        switch (states)
        {
            case States.FixingCaravan:
                FixCaravan();
                break;
            case States.Idle:
                ReturnToCaravan();
                break;
        }
    }

    private void ReturnToCaravan()
    {
        agent.stoppingDistance = 0.0f;
        agent.destination = caravan_attach_point.position;
    }

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
        if (!GetComponent<Character_interaction>().has_part)
        {
            agent.stoppingDistance = 0.0f;
            agent.destination = FindClosestPart();
        }
    }

    private void AttachPartToCaravan()
    {
        if (GetComponent<Character_interaction>().has_part)
        {
            Caravan caravan = GameObject.FindGameObjectWithTag("Caravan").GetComponent<Caravan>();

            agent.stoppingDistance = 5.0f;
            agent.destination = caravan.transform.position;

            if (Vector3.Distance(transform.position, caravan.transform.position) <= agent.stoppingDistance * 1.5f)
                GetComponent<InteractWithCaravan>().AddToCaravan();
        }
    }

    private void AttachSelf(Transform destination)
    {
        GetComponent<Character_control>().enabled = false;
        transform.position = destination.position;
    }

    private void DetachSelf()
    {
        GetComponent<Character_control>().enabled = true;
    }
}