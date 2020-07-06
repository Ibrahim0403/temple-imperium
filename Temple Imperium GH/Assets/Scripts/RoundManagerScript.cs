using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManagerScript : MonoBehaviour
{
    public int enemiesPerSpawner;
    public int activeRound;
    public int maxEnemiesPerRound;
    public int killCounter = 0;

    public float roundTime = 60f;

    public List<SpawnerScript> spawners = new List<SpawnerScript>();

    private bool roundEnded = true;
    private float roundTimeMax;

    void Start()
    {
        roundTimeMax = roundTime;
        Debug.Log("Round Started");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            BeginRound();
        }

        if (roundTime <= 0f)
        {
            Debug.Log("Time ended");
        }

        BeginTimer();
    }

    void BeginRound()
    {

        if (roundEnded)
        {
            Debug.Log("Round: " + activeRound.ToString());
            enemiesPerSpawner = enemiesPerSpawner + 1;
            maxEnemiesPerRound = enemiesPerSpawner * spawners.Count;

            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].Spawn(enemiesPerSpawner);
            }

            roundEnded = false;
        }
    }

    public void KillEnemy()
    {
        maxEnemiesPerRound -= 1;
        killCounter += 1;
        CountEnemies();
    }

    void CountEnemies()
    {
        if (maxEnemiesPerRound == 0)
        {
            roundEnded = true;
            activeRound += 1;
            Debug.Log("Round ended");
            BeginRound();
            roundTimeMax = roundTimeMax + 10f;
            roundTime = roundTimeMax;
        }
    }

    void BeginTimer()
    {
        roundTime -= Time.deltaTime;
    }
}
