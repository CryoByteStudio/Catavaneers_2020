using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] Transform attach_tf;

    [SerializeField] bool player_active_bl; //set in inspector to true or false acordingly if you want manual control of the player
    public float speed_fl=10f; // speed of the character

    [SerializeField] string horizontal_ctrl = "Horizontal_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly
    [SerializeField] string Vertical_ctrl = "Vertical_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly

    public string collider_part;

    Rigidbody RB;
    Player_Inventory p_inv;

    // Property field
    /*
    Purpose:                Finds out if attach point has "Part" object as child.
    Effects:                Return true if yes and false if no.
    Input/Output:           Input N/A. Output true/false.
    Global Variables Used:  No variable was altered.
    */
    public bool has_part
    {
        get
        {
            if (attach_tf.childCount != 0 && attach_tf.GetComponentInChildren<Part>())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //in case we like to use it
        //movement_clickToMove();

        if (player_active_bl) movement_arrowKeys();
        p_inv = GetComponent<Player_Inventory>();
    }

    /*
    Purpose:                Move character with mouse click or hold.
    Effects:                Move to mouse cursor upon click or hold.
    Input/Output:           Input mouse click. Output N/A.
    Global Variables Used:  NavMeshAgent.destination.
    */

    private void movement_clickToMove()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Move();
        }
    }

    /*
    Purpose:                Move with keystrokes and joysicks.
    Effects:                Move along the axis acording to the input given.
    Input/Output:           Input player control axis // Output N/A
    Global Variables Used:  Raw Axis input
    */
    private void movement_arrowKeys()
    {
        float h_fl = Input.GetAxis(horizontal_ctrl);
        transform.position += Vector3.right * speed_fl * h_fl * Time.deltaTime;

        float v_fl = Input.GetAxis(Vertical_ctrl);
        transform.position += Vector3.forward * speed_fl * v_fl * Time.deltaTime;


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
    Purpose:                Attach object to player's child transform attach_point as child and reposition to attach_point's position.
    Effects:                Part that was picked up is now child of player's child transform attach_point (Player is a grandparent!).
                            Will not be transfered to other player if collided while being held or attached to caravan
    Input/Output:           Input Collider c. Output N/A.
    Global Variables Used:  transform of Collider c's gameObject, Player_Inventory.CaravanPart,
    */
    void PickUpPart(Collider c)
    {
        Part part = c.GetComponent<Part>();

        if (!part.isOnCaravan && !part.isPickedUp)
        {
            c.GetComponent<Part>().AttachTo(transform.GetChild(0));
            p_inv.CaravanPart += 1; //I added this for the sake of inventory ~Silas
        }
    }

    /*
    Purpose:                Check for trigger if collided with object with tag "Part" and has_object is false to pick that object up.
    Effects:                PickUpPart().
    Input/Output:           Input Collider c. Output N/A.
    Global Variables Used:  See PickUpPart().
    */
    void OnTriggerEnter(Collider c)
    {
        if (!has_part && c.gameObject.tag == "Part")
        {
            PickUpPart(c);
        }
        else if (has_part)
        {
            Debug.Log(transform.name + " is holding a part");
        }
    }
}
