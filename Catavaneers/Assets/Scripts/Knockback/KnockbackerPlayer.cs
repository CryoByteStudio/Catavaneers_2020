using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackerPlayer : MonoBehaviour
{
    public float knockbackforce;
    public Transform kbpoint;
    
    
    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Enemy")
        {

            Knockbackable kb = c.GetComponent<Knockbackable>();
            if (!kb)
            {
                Debug.LogError("No kb script found attacked to" + c.name);
                //Do nothing if there is no knockback script(not knockable)
            }
            else
            {
                if (!kb.isknockback)//check if it is currently being knocked back, if not continue.
                {
                    
                        kb.ApplyKnockBack(kbpoint, knockbackforce);
                    
                    
                }
                }

            }
        }
    }

