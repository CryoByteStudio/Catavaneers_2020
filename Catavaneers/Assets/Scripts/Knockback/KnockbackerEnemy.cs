using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackerEnemy : MonoBehaviour
{
    public float knockbackforce;
    public Transform kbpoint;

    
    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
           
            Knockbackable kb = c.GetComponent<Knockbackable>();
            if (!kb) {
                //Do nothing if there is no knockback script(not knockable)
                Debug.LogError("No kb script found attacked to" + c.name);
            }
            else
            {
                if (!kb.isknockback)// check if it is currently being knocked back, if not continue.
                {
                    Debug.Log("Knocking back");
                    kb.ApplyKnockBack(kbpoint, knockbackforce);
                }
            }
            
        }
    }
}
