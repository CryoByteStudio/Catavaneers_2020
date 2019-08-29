using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<GameObject> enemies;

    private void Start()
    {
        enemies = new List<GameObject>();
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public int GetEnemyCount()
    {
        return enemies.Count;
    }

    public List<GameObject> GetEnemiesList()
    {
        return enemies;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
        
        //enemies[enemy.GetComponent<EnemyController>().id].SetActive(false);
        Destroy(enemy);
    }
}
