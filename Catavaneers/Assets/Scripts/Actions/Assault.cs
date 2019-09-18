using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : MonoBehaviour
{
    EnemyManager enemyManager;

    InteractWithCaravan interact;

    static float damageDealtToCaravan;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        interact = GetComponent<InteractWithCaravan>();
    }

    public void DealDamage(Transform target, float amount)
    {
        Health health = target.GetComponent<Health>();

        if (health)
        {
            if (target.gameObject.tag != "Caravan")
                health.Reduce(amount);
            else
            {
                damageDealtToCaravan += amount;

                float healthValue = 0f;

                if (interact.PartToBeRemoved())
                    healthValue = interact.PartToBeRemoved().healthValue;

                if (damageDealtToCaravan >= healthValue)
                {
                    interact.RemoveFromCaravan();
                    damageDealtToCaravan = 0;
                }
            }
        }
    }
}
