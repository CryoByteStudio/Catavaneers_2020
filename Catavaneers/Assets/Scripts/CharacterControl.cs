using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] Transform attach_tf;

    public string collider_part;
    public bool has_object;

    Player_Inventory p_inv;

    void Start()
    {
        has_object = false;
        p_inv = GetComponent<Player_Inventory>();
    }

    /*
    Purpose:                Move character with mouse click or hold.
    Effects:                Move to mouse cursor upon click or hold.
    Input/Output:           Input mouse click. Output N/A.
    Global Variables Used:  NavMeshAgent.destination.
    */
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Move();
        }
    }

    /*
    Purpose:                Move character using NavMeshAgent.
    Effects:                Move to raycast hit point determined by mouse cursor position.
    Input/Output:           Input mouse cursor position upon click or hold (Update()). Output N/A.
    Global Variables Used:  NavMeshAgent.destination.
    */
    public void Move()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    /*
    Purpose:                Check for trigger if collided with object with tag "Part" and has_object is false to pick that object up.
    Effects:                PickUpPart().
    Input/Output:           Input Collider c. Output N/A.
    Global Variables Used:  has_object (Class CharacterControl).
    */
    void OnTriggerEnter(Collider c)
    {
        if (!has_object && c.gameObject.tag == "Part" && p_inv.CaravanPart < 1) //Sorry to mess with your Character Controller but i think we agreed on only one part at a time ~Silas
        {
            PickUpPart(c);
        }
    }

    /*
    Purpose:                Attach object to player's child transform attach_point as child and reposition to attach_point's position.
    Effects:                Part that was picked up is now child of player's child transform attach_point (Player is a grandparent!).
    Input/Output:           Input Collider c. Output N/A.
    Global Variables Used:  has_object (Class CharacterControl), transform of Collider c's gameObject.
    */
    void PickUpPart(Collider c)
    {
        c.GetComponent<Part>().AttachTo(transform.GetChild(0));
        has_object = true;
        p_inv.CaravanPart += 1; //I added this for the sake of inventory ~Silas
    }
}
