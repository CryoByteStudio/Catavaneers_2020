using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    public Transform cam_tf;

    public Transform caravan_tf;

    private Vector3 camera_Offset;

    [Range(0.01f, 1.0f)]
    public float smooth_Factor_float = 0.5f;

    public bool look_At_Cavaneer = false;

    private Cycle_Manager _cycle_Manager;

    private float cam_Speed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        camera_Offset = transform.position - caravan_tf.position;
        _cycle_Manager = GameObject.Find("UI").GetComponent<Cycle_Manager>();
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        Vector3 new_Pos = caravan_tf.position + camera_Offset;


        if (look_At_Cavaneer)
            transform.LookAt(caravan_tf);
        if (_cycle_Manager.is_PVP == true)
        {
            Vector3 pos = cam_tf.position;
            if (cam_tf.position.x < 120)
            {
                pos.x += cam_Speed * Time.deltaTime;
            }

            cam_tf.position = pos;
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, new_Pos, smooth_Factor_float);

        }
    }
}
