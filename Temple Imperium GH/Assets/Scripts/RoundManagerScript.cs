using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;

    public int enemiesPerSpawner;
    public int activeRound;
    public int maxEnemiesPerRound;
    public int killCounter = 0;

    public float roundTime;
    public float seconds;
    public float minutes;

    public List<SpawnerScript> spawners = new List<SpawnerScript>();

    private bool roundEnded = true;
    private float roundTimeMax;

    void Start()
    {
        roundTime = 50f;
        roundTimeMax = roundTime;
        Debug.Log("Round Started");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            BeginRound();
        }

        if (roundTime <= 0f) //round time limit reached
        {
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
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
            roundTimeMax = roundTimeMax + 30f;
            roundTime = roundTimeMax;
        }
    }

    void BeginTimer()
    {
        seconds = (roundTime % 60);
        minutes = Mathf.Floor(roundTime / 60);
        roundTime -= Time.deltaTime;
    }
}
