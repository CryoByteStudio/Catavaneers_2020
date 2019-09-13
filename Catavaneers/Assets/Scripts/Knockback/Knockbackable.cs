using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockbackable : MonoBehaviour
{
    [Tooltip("Initial position of object before knockback.")] 
    public Transform knockstart;
    [Tooltip("Final destination for object after knockback.")] 
    public Transform knockend;
    [Tooltip("The system time at the beginning of knockback.")] 
    public float starttime;
    [Tooltip("This is the total distance between the start point and end point of the knockback.(Gets changed by knockbacker)")] 
    public float knockbackdistance;
    [Tooltip("How quickly the knockback happens.(gets changed by the knockbacker)")]
    public float knockbackspeed = 1f;
    [Tooltip("True if knockback should be taking place. false to give player control back.")]
    public bool isknockback;
    [Tooltip("How close to the end of the knockback object must be before regaining control(used to prevent control locking)")]
    public float knockbackleeway;
    [Tooltip("True if this object can get knocked back(should probably be true if you attached this script to it.)")]
    public bool cangetknockback;
    [Tooltip("Maximum duration for a single knockback effect on this object(used to prevent control locking)")]
    public float knockbackmaxtime;

    
  

    private void Start()
    {
        //Just sets inital transforms to be equal to something.
        knockstart = transform;
        knockend = transform;
    }



    public void ApplyKnockBack(Transform endpoint, float force)// should be called by whatever object is knocking this object back on collision, or on attack, etc. See KnockBacker.cs or KnockBackerEnemy.cs for example
    {
        if (cangetknockback) {
            //Debug.Log("Knockback start");
            isknockback = true;//Object is being knocked back.
        //sets the current time to be the knockback start time.
        starttime = Time.time;
        

        //sets end point of the knockback
        knockend = endpoint;
        //sets the current position to be the starting position of the knockback effect.
        knockstart = transform;
        // magnitude(ultimately speed of the knockback
        force = knockbackspeed;
        //Gets the distance between the 2 points of knockback.
        knockbackdistance = Vector3.Distance(knockstart.position, knockend.position);
    }   
       
    }

    private void Update()
    {
        
        if (cangetknockback)
        {
            if (isknockback)
            {
                //How much distance has been covered through knockback this frame .
                float distcovered = (Time.time - starttime) * knockbackspeed;
                //How  much out of the total has been covered.
                float amountcovered = distcovered / knockbackdistance;
                //Sets the position of the object based on how far along it is.
                transform.position = Vector3.Lerp(knockstart.position, knockend.position, amountcovered);
                //Once the distance covered has reached a certain point, stop the knockback effect giving control back to player.

                if (knockbackdistance - distcovered < knockbackleeway)//End knockback if leeway reached.
                {
                    isknockback = false;
                   // Debug.Log("Knockback end");
                }
                if (starttime + knockbackmaxtime < Time.time)//End knockback if max time reached.
                {
                    isknockback = false;
                   // Debug.Log("Knockback end");
                }
            }
        }

        
        

  
    }
}
