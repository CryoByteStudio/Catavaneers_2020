using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character_control : MonoBehaviour
{
    [SerializeField] public bool player_active_bl; //set in inspector to true or false acordingly if you want manual control of the player

    public float speed_fl=10f; // speed of the character
    

    [SerializeField] string horizontal_ctrl_str = "Horizontal_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly
    [SerializeField] string Vertical_ctrl_str = "Vertical_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly




    Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //in case we like to use it
        //movement_clickToMove();

        if (player_active_bl) movement_arrowKeys();
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
        float h_fl = Input.GetAxis(horizontal_ctrl_str);//X axis
        transform.position += Vector3.right * speed_fl * h_fl * Time.deltaTime;
        

        float v_fl = Input.GetAxis(Vertical_ctrl_str);//Z axis
        transform.position += Vector3.forward * speed_fl * v_fl * Time.deltaTime;


        //Edit by Sasha: Added rotation to the player gameobject based on what direction the input is in.

        //Vertical(v_fl) = X axis, Horizontal(h_fl) = Z axis

        if (v_fl > 0 && h_fl==0)//if x+
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if(v_fl<0 && h_fl == 0)//if x-
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }else if(v_fl==0 &&h_fl<0)//if Z-
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (v_fl == 0 && h_fl > 0)//if Z+
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }else if (v_fl > 0 && h_fl > 0)//ifz+x+
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (v_fl < 0 && h_fl < 0)//ifz-x-
        {
            transform.rotation = Quaternion.Euler(0, -135, 0);
        }
        else if (v_fl < 0 && h_fl > 0)//ifz-x+
        {
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else if (v_fl > 0 && h_fl < 0)//ifz+x-
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
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


}
