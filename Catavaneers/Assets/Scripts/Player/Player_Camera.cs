using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    //find player in game

    public GameObject player_In_Cam;
    private Cycle_Manager _Cycle_Manager;

    [SerializeField]
    private GameObject attach_Pont;

    [SerializeField]
    private Transform cavarane;

    private Vector3 player_camera_Offset;

    private bool transform_Player_At_Night;

    //set up for how do you want the camera follow the player

    [Range(0.01f, 1.0f)]
    public float smooth_Factor_float = 0.5f;

    // if camera is not set to the player find the player transform again

    public bool look_At_Player = true;

    // find the camera in the game

    [SerializeField] private Camera player_Follow_cam;

    [SerializeField] private GameObject Camera_Ui_Follow_OFF;
    [SerializeField] private GameObject Camera_Ui_Follow_ON;

    // Start is called before the first frame update
    void Start()
    {
        // off set betwwen the camera and player
        player_camera_Offset = transform.position - player_In_Cam.transform.position;
        player_Follow_cam.enabled = false;
        _Cycle_Manager = GameObject.Find("UI").GetComponent<Cycle_Manager>();
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        // camera follow player
        Vector3 new_Pos = player_In_Cam.transform.position + player_camera_Offset;
        transform.position = Vector3.Slerp(transform.position, new_Pos, smooth_Factor_float);

        // find player if camera is off 
        if (look_At_Player)
            transform.LookAt(player_In_Cam.transform);

        //set the camera on/off based on the caravane postion
        if (_Cycle_Manager.is_day && _Cycle_Manager.is_PVP != true)
        {
            player_Follow_cam.enabled = true;
            Camera_Ui_Follow_OFF.SetActive(false);
            Camera_Ui_Follow_ON.SetActive(true);
            transform_Player_At_Night = true;
        }
        //else if (player_tf.position.z > (cavarane.position.z + 30) || player_tf.position.z < (cavarane.position.z - 30))
        //{
        //    player_Follow_cam.enabled = true;
        //    Camera_Ui_Follow_OFF.SetActive(false);
        //    Camera_Ui_Follow_ON.SetActive(true);
        //}
        else
        {
            player_Follow_cam.enabled = false;
            Camera_Ui_Follow_OFF.SetActive(true);
            Camera_Ui_Follow_ON.SetActive(false);
        }

        if (_Cycle_Manager.is_day != true && transform_Player_At_Night == true)
        {
            NightStart();
        }
    }

    public void NightStart()
    {
        player_In_Cam.transform.position = attach_Pont.transform.position;
        transform_Player_At_Night = false;
    }

    
}
