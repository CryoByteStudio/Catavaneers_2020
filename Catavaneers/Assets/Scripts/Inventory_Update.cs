using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Update : MonoBehaviour
{
    [SerializeField] private Player_Inventory Player_Inventory;

    [Header("Camera ON")]
    [SerializeField] private Text GP_Cam_On;
    [SerializeField] private Text Wood_Cam_On;
    [SerializeField] private Text Bandage_Cam_On;
    [SerializeField] private Image Caravan_Part_Cam_On;
    [SerializeField] private Image Trap1_Cam_On;
    [SerializeField] private Image Trap2_Cam_On;

    [Header("Camera OFF")]
    [SerializeField] private Text GP_Cam_Off;
    [SerializeField] private Text Wood_Cam_Off;
    [SerializeField] private Text Bandage_Cam_Off;
    [SerializeField] private Image Caravan_Part_Cam_Off;
    [SerializeField] private Image Trap1_Cam_Off;
    [SerializeField] private Image Trap2_Cam_Off;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_GP()
    {
        Player_Inventory.GP += 1;
        GP_Cam_On.text = "          X " + Player_Inventory.GP;
        GP_Cam_Off.text = "          X " + Player_Inventory.GP;
    }
    public void Update_Wood()
    {
        Player_Inventory.wood += 1;
        Wood_Cam_On.text = "          X " + Player_Inventory.wood;
        Wood_Cam_Off.text = "          X " + Player_Inventory.wood;
    }
    public void Update_Bandage()
    {
        Player_Inventory.Bandage += 1;
        Bandage_Cam_On.text = "          X " + Player_Inventory.Bandage;
        Bandage_Cam_Off.text = "          X " + Player_Inventory.Bandage;
    }
}
