using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private enum EnemyTypes
    {
        Mouse, Cat, Dog
    }

    Dictionary<EnemyTypes, GameObject> Enemies = new Dictionary<EnemyTypes, GameObject>();

    EnemyManager enemyManager;

    static int enemyCounter = 0;

    [SerializeField] public bool spawning;
    [SerializeField] bool random;
    [SerializeField] bool randomSpawnLocation;
    [SerializeField] EnemyTypes enemyType = new EnemyTypes();
    [SerializeField] GameObject Mouse;
    [SerializeField] GameObject Cat;
    [SerializeField] GameObject Dog;
    [SerializeField] int maxEnemyCount;
    [SerializeField] float spawnInterval;

    private float timeSinceSpawn = Mathf.Infinity;

    private void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        enemyManager = GetComponent<EnemyManager>();

        if (spawnInterval == 0.0f) spawnInterval = 3.0f;
        if (maxEnemyCount == 0) maxEnemyCount = 10;

        Enemies.Add(EnemyTypes.Mouse, Mouse);
        Enemies.Add(EnemyTypes.Cat, Cat);
        Enemies.Add(EnemyTypes.Dog, Dog);
    }

    private void Update()
    {
        if (CanSpawn())
            SpawnEnemy(random);
    }

    private void SpawnEnemy(bool random)
    {
        GameObject enemy = null;

        if (!random)
        {
            Instantiate(Enemies[enemyType], transform.position, transform.rotation);
        }
        else
        {
            enemyType = (EnemyTypes)Enum.GetValues(enemyType.GetType()).GetValue(UnityEngine.Random.Range(0, 3));

            enemy = Instantiate(Enemies[enemyType], transform.position, transform.rotation);
        }

        if (randomSpawnLocation)
        {
            RandomRelocateSpawner();
        }

        enemyManager.AddEnemy(enemy);
        enemyCounter++;
        timeSinceSpawn = 0.0f;
    }

    private bool CanSpawn()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= spawnInterval && enemyCounter < maxEnemyCount)
        {
            return true;
        }
        else return false;
    }

    private void RandomRelocateSpawner()
    {
        float x, y, z;

        float minX = -40f;
        float maxX =  40f;
        float minZ = -40f;
        float maxZ =  40f;

        do
        {
            x = UnityEngine.Random.Range(-45, 45);
            y = 1.5f;
            z = UnityEngine.Random.Range(-45, 45);
        }
        while (x > minX && x < maxX && z > minZ && z < maxZ);

        transform.position = new Vector3(x, y, z);
    }
}
