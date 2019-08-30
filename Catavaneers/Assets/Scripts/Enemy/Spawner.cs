using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnTypes
    {
        //different spawn type potential?
        Normal,
        Once,
        Wave,
        TimedWave
    }

    // different difficulty enemies i.e. boss enemies?
    public enum EnemyLevels
    {
        Normal
    }

    // type
    public enum EnemyTypes
    {
        Mouse, Cat, Dog
    }

    //level to be spawned
    public EnemyLevels Difficulty = EnemyLevels.Normal;

    public EnemyTypes enemyType = new EnemyTypes();

    // reference to player AI
    EnemyManager enemyManager;

    //Enemy Types to replace here
    [SerializeField] GameObject Mouse;
    [SerializeField] GameObject Cat;
    [SerializeField] GameObject Dog;
    private Dictionary<EnemyTypes, GameObject> Enemies = new Dictionary<EnemyTypes, GameObject>();

    //Number of enemies, how many are to be created and are created
    public int TotalEnemies = 15;
    private int NumEnemy = 0;
    private int SpawnedEnemy = 0;

    //Spawn states
    private bool WaveSpawn = false;
    public bool Spawn = true;
    public SpawnTypes SpawnType = SpawnTypes.Normal;
    //timed wave control
    public float WaveTimer = 60.0f;
    private float TimeTillWave = 0.0f;
    //Wave Control
    public int TotalWaves = 5;
    private int NumWaves = 0;
    //Spawn Controls
    public float SpawnDelay;
    public float SecondsPassed;

    public int SpawnID;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();

        SpawnID = Random.Range(1, 100);

        Enemies.Add(EnemyTypes.Mouse, Mouse);
        Enemies.Add(EnemyTypes.Cat, Cat);
        Enemies.Add(EnemyTypes.Dog, Dog);
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawn)
        {
            SecondsPassed += Time.deltaTime;
            if (SecondsPassed > SpawnDelay)
            {
                SecondsPassed = 0;
                Debug.Log("Spawned");

                switch (SpawnType)
                {
                    case SpawnTypes.Normal:
                        SpawnNormal();
                        break;
                    case SpawnTypes.Once:
                        SpawnOnce();
                        break;
                    case SpawnTypes.Wave:
                        SpawnWave();
                        break;
                    case SpawnTypes.TimedWave:
                        SpawnTimedWave();
                        break;
                }
            }
        }
        else
        {
           // Debug.Log("Need Time");
        }
    
    }

    private void SpawnTimedWave()
    {
        Debug.Log("Timed Wave");
        if (NumWaves <= TotalWaves)
        {
            TimeTillWave += Time.deltaTime;
            if (WaveSpawn)
            {
                SpawnEnemy();
            }
            if (TimeTillWave >= WaveTimer)
            {
                WaveSpawn = true;
                TimeTillWave = 0.0f;
                NumWaves++;
            }
            if (NumEnemy >= TotalEnemies)
            {
                WaveSpawn = false;
            }
        }
    }

    private void SpawnWave()
    {
        Debug.Log("Wave");
        if (NumWaves < TotalWaves + 1)
        {
            if (WaveSpawn)
            {
                SpawnEnemy();
            }
            if (NumEnemy == 0)
            {
                WaveSpawn = true;
                NumWaves++;
            }
            if (NumEnemy == TotalEnemies)
            {
                WaveSpawn = false;
            }
        }
    }

    private void SpawnOnce()
    {
        Debug.Log("Once");
        if (SpawnedEnemy >= TotalEnemies)
        {
            Spawn = false;
        }
        else
        {
            SpawnEnemy();
        }
    }

    private void SpawnNormal()
    {
        Debug.Log("Normal");
        if (NumEnemy < TotalEnemies)
        {
            //Spawns an enemy
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject Enemy = Instantiate(Enemies[enemyType], gameObject.transform.position, Quaternion.identity);
        Enemy.SendMessage("Bob", SpawnID);
            NumEnemy++;
        SpawnedEnemy++;
    }

    public void KillEnemy(int sID)
    {
        //if the enemy spawnID is a match to the spawnersID then remove an enemy count
        if (SpawnID == sID)
        {
            NumEnemy--;
        }
    }

    public void EnableSpawner(int sID)
    {   //Enables spawner based on SpawnerID
        if (SpawnID == sID)
        {
            Spawn = true;
        }
    }

    public void DisableSpawner(int sID)
    {   //fisables spawner based on SpawnerID
        if (SpawnID == sID)
        {
            Spawn = false;
        }
    }
    
    //allows the spawner to start via trigger event (will be changed to timed once timer is planned)
    public void EnableTrigger()
    {
        Spawn = true;
    }
}