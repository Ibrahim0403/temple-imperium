using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemySmall;
    public GameObject enemyMedium;
    public GameObject enemyLarge;
    public RoundManagerScript roundManager;

    private int maxEnemiesToSpawn;

    public void Spawn(int maxSpawn)
    {
        maxEnemiesToSpawn = maxSpawn;
        InvokeRepeating("SpawnEnemy", 0.5f, 2f); //keep spawning enemies
    }

    void SpawnEnemy()
    {
        GameObject goEnemySmall = Instantiate(enemySmall, transform.position, Quaternion.identity);
        goEnemySmall.GetComponent<TargetScript>().Init(roundManager);
        maxEnemiesToSpawn -= 1;

        if (maxEnemiesToSpawn == 1)
        {
            GameObject goEnemyMedium = Instantiate(enemyMedium, transform.position, Quaternion.identity);
            goEnemyMedium.GetComponent<TargetScript>().Init(roundManager);
            maxEnemiesToSpawn -= 1;
        }

        if (maxEnemiesToSpawn == 0)
        {
            CancelInvoke();
        }
    }
}
