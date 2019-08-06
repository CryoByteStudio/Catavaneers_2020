using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public Transform player_tf;

    private Vector3 player_camera_Offset;

    [Range(0.01f, 1.0f)]
    public float smooth_Factor_float = 0.5f;

    public bool look_At_Player = false;

    [SerializeField] private Camera player_Follow_cam;

    // Start is called before the first frame update
    void Start()
    {
        player_camera_Offset = transform.position - player_tf.position;
        player_Follow_cam.enabled = false;
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        Vector3 new_Pos = player_tf.position + player_camera_Offset;

        transform.position = Vector3.Slerp(transform.position, new_Pos, smooth_Factor_float);

        if (look_At_Player)
            transform.LookAt(player_tf);
    }
}
