﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    //find player in game

    public Transform player_tf;
    private Cycle_Manager _Cycle_Manager;

    [SerializeField]
    private Transform attach_point;
 
    [SerializeField]
    private Transform cavarane;

    private Vector3 player_camera_Offset;

    private bool transform_Player_At_Night;

    //set up for how do you want the camera follow the player

    [Range(0.01f, 1.0f)]
    public float smooth_Factor_float = 0.5f;

    // if camera is not set to the player find the player transform again

    public bool look_At_Player = false;

    // find the camera in the game

    [SerializeField] private Camera player_Follow_cam;

    [SerializeField] private GameObject Camera_Ui_Follow_OFF;
    [SerializeField] private GameObject Camera_Ui_Follow_ON;

    // Start is called before the first frame update
    void Start()
    {
        // off set betwwen the camera and player
        player_camera_Offset = transform.position - player_tf.position;
        player_Follow_cam.enabled = false;
        _Cycle_Manager = GameObject.Find("UI").GetComponent<Cycle_Manager>();
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
        if (_Cycle_Manager.is_day)
        {
            transform_Player_At_Night = true;
            player_Follow_cam.enabled = true;
            Camera_Ui_Follow_OFF.SetActive(false);
            Camera_Ui_Follow_ON.SetActive(true);
        }
        //else if (_Cycle_Manager.is_day != true)
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

        if (_Cycle_Manager.is_day != true &&  transform_Player_At_Night == true)
        {
            NightStart();
        }

    }

    public void NightStart()
    {
        player_tf.position = attach_point.position;
        transform_Player_At_Night = false;
    }
}
