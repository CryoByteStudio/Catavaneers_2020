using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Tooltip("true if this is a jump pad.")]
    [SerializeField] bool isLaunchPad;
    [Tooltip("true for a Catnip Trap that will reverse controls")]
    [SerializeField] bool IsCatNip;
    [Tooltip("Has the trap been triggered? (should probably start as false.)")]
    [SerializeField] bool IsTriggered;
    [Tooltip("true if this traps effect is over time, false if it is instantaneous.")]
    [SerializeField] bool IsOverTime;
    [Tooltip("How long is this traps effect? only works if IsOverTime is true.")]
    [SerializeField] float trap_duration;
    [Tooltip("This is the magnitude of the traps effect.")]
    [SerializeField] float trap_impact;

    float timer;
    CharacterStats playerreference;
    Character_control cref;

    


    // Update is called once per frame
    void Update()
    {
        if (IsOverTime) {
            //check trap is triggered
            if (IsTriggered)
            {
                //checks if the timer has elapsed
                if (Time.time > timer)
                {
                    //Reset the trap


                    if (IsCatNip)
                    {
                        //reset the player speed
                        playerreference.ModifySpeed(trap_impact);
                    }
                    else
                    {
                        //reset the player speed
                        playerreference.ModifySpeed(-trap_impact);
                    }
                  

                    IsTriggered = false;

                    //#TODO Add destroy functionality
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOverTime)
        {
           
            //reset player speed
            if (IsCatNip)
            {
                playerreference.ModifySpeed(trap_impact);
            }else if (isLaunchPad)
            {

            }
            else
            {
                playerreference.ModifySpeed(-trap_impact);
            }
            //reset trap
            IsTriggered = false;
        }

    }

    private void OnTriggerEnter(Collider collision)
    {

        
        //Only handle collision if trap is not already triggered.
        if (!IsTriggered)
        {
            //check if collided object was a player
            if (collision.gameObject.tag == "Player")
             
            {
                //get a reference to the character controller and stats on collided player
                playerreference = collision.gameObject.GetComponent<CharacterStats>();
                cref = collision.gameObject.GetComponent<Character_control>();

                if (IsCatNip)
                {
                    playerreference.ModifySpeed(-trap_impact);
                }
              
                else 
                {
                    playerreference.ModifySpeed(trap_impact);
                }
                
                //set trap as triggered so it cant be triggered mutliple times at once.
                IsTriggered = true;
                
                //Set timer to equal current time our trap duration
                timer = Time.time + trap_duration;
              
               

            }
        }
        

        //else if...tag==enemy
        //..modify enemy speed value
    }
}
