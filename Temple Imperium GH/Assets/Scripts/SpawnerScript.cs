using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemySmall;
    public GameObject enemyMedium;
    public GameObject enemyLarge;
    public RoundManagerScript roundManager;

    public static int currentRound;
    public static int spawnLargeEnemyRound;

    private int maxEnemiesToSpawn;
    private int mediumEnemiesToSpawn;
    private int largeEnemiesToSpawn;

    void Start()
    {
        spawnLargeEnemyRound = 3; //spawn large enemies at round 3
    }

    public void Spawn(int maxSpawn)
    {
        maxEnemiesToSpawn = maxSpawn;
        InvokeRepeating("SpawnEnemy", 0.5f, 3f); //keep spawning enemies
    }

    public void SpawnMediumEnemy(int maxSpawn)
    {
        mediumEnemiesToSpawn = maxSpawn;
        InvokeRepeating("SpawnRangedEnemy", 0.5f, 3f);
    }

    public void SpawnLargeEnemy(int maxSpawn)
    {
        largeEnemiesToSpawn = maxSpawn;
        InvokeRepeating("SpawnBoss", 0.5f, 3f);
    }

    void SpawnEnemy()
    {
            GameObject goEnemySmall = Instantiate(enemySmall, transform.position, Quaternion.identity);
            goEnemySmall.GetComponent<TargetScript>().Init(roundManager);
            maxEnemiesToSpawn -= 1;

        if (maxEnemiesToSpawn == 0)
        {
            CancelInvoke("SpawnEnemy");
        }
    }

    void SpawnRangedEnemy()
    {
        GameObject goEnemyMedium = Instantiate(enemyMedium, transform.position, Quaternion.identity);
        goEnemyMedium.GetComponent<TargetScript>().Init(roundManager);
        mediumEnemiesToSpawn -= 1;

        if (mediumEnemiesToSpawn == 0)
        {
            CancelInvoke("SpawnRangedEnemy");
        }
    }

    void SpawnBoss()
    {
        GameObject goEnemyLarge = Instantiate(enemyLarge, transform.position, Quaternion.identity);
        goEnemyLarge.GetComponent<TargetScript>().Init(roundManager);
        largeEnemiesToSpawn -= 1;
        spawnLargeEnemyRound += spawnLargeEnemyRound;

        if (largeEnemiesToSpawn == 0)
        {
            CancelInvoke("SpawnBoss");
        }
    }
}
