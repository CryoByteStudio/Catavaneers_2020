using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    //find player in game

    public GameObject player_In_Cam;

    public Transform player_tf;

    [SerializeField]
    private Transform attach_point;
 
    [SerializeField]
    private Transform cavarane;

    private Vector3 player_camera_Offset;

    //set up for how do you want the camera follow the player

    [Range(0.01f, 1.0f)]
    public float smooth_Factor_float = 0.5f;

    // if camera is not set to the player find the player transform again

    public bool look_At_Player = true;

    // find the camera in the game

    [SerializeField] private Camera player_Follow_cam;

    // Start is called before the first frame update
    void Start()
    {
        // off set betwwen the camera and player
        player_camera_Offset = transform.position - player_tf.position;
        player_Follow_cam.enabled = false;
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        // camera follow player
        Vector3 new_Pos = player_tf.position + player_camera_Offset;
        transform.position = Vector3.Slerp(transform.position, new_Pos, smooth_Factor_float);

        // find player if camera is off 
        if (look_At_Player)
            transform.LookAt(player_tf);

        //set the camera on/off based on the caravane postion
        if (player_tf.position.x > (cavarane.position.x + 30) || player_tf.position.x < (cavarane.position.x - 30))
        {
            player_Follow_cam.enabled = true;
        }
        else if (player_tf.position.z > (cavarane.position.z + 30) || player_tf.position.z < (cavarane.position.z - 30))
        {
            player_Follow_cam.enabled = true;
        }
        else
        {
            player_Follow_cam.enabled = false;
        }
    }

    public void NightStart()
    {
        player_tf.position = attach_point.position;
        transform_Player_At_Night = false;
    }

    
}
