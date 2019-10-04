using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour
{

    public Transform caravan_tf;

    private Vector3 camera_Offset;

    [Range(0.01f, 1.0f)]
    public float smooth_Factor_float = 0.5f;

    public bool look_At_Cavaneer = false;

    // Start is called before the first frame update
    void Start()
    {
        camera_Offset = transform.position - caravan_tf.position;
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        Vector3 new_Pos = caravan_tf.position + camera_Offset;

        transform.position = Vector3.Slerp(transform.position, new_Pos, smooth_Factor_float);

        if (look_At_Cavaneer)
            transform.LookAt(caravan_tf);
    }
}
