using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject StartScene;
    public GameObject EndScene;

    public int enemiesPerSpawner;
    public int mediumEnemiesCount;
    public int largeEnemiesCount;
    public int activeRound;
    public int maxEnemiesPerRound;
    public int killCounter = 0;

    public float seconds;
    public float minutes;

    public bool hasCompleted;
    public bool hasRestarted;

    public List<SpawnerScript> spawners = new List<SpawnerScript>();

    public static float roundTimeMax;
    public static float roundTime;
    public static float repairCooldown = 40f;
    public static float repairAvailable;

    private bool roundEnded = true;

    void Start()
    {
        hasRestarted = false;
        mediumEnemiesCount = 1;
        largeEnemiesCount = 1;
        roundTime = 60f;
        roundTimeMax = roundTime;
        StartScene.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Round Started");
    }

    void Update()
    {
        SpawnerScript.currentRound = activeRound;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            StartScene.SetActive(false);
            BeginRound();
        }

        if (hasCompleted)
        {
            Time.timeScale = 0f;
            EndScene.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (roundTime <= 0f || PlayerStats.playerHealth <= 0) //round time limit reached
        {
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            if (!hasRestarted)
            {
                Restart();
                hasRestarted = true;
            }
        }

        BeginTimer();
    }

    void Restart()
    {
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hasRestarted = false;
    }

        void BeginRound()
    {

        if (roundEnded)
        {
            Debug.Log("Round: " + activeRound.ToString());
            enemiesPerSpawner = enemiesPerSpawner + 1;
            maxEnemiesPerRound = enemiesPerSpawner * spawners.Count;

            for (int i = 0; i < spawners.Count; i++) //spawn different enemies
            {
                spawners[i].Spawn(enemiesPerSpawner);
            }

            if (activeRound > 1)
            {
                spawners[Random.Range(0, 3)].SpawnMediumEnemy(mediumEnemiesCount);
                maxEnemiesPerRound += mediumEnemiesCount;
                mediumEnemiesCount += 1;
            }

            if (activeRound == SpawnerScript.spawnLargeEnemyRound)
            {
                spawners[Random.Range(0, 3)].SpawnLargeEnemy(largeEnemiesCount);
                maxEnemiesPerRound += largeEnemiesCount;
                largeEnemiesCount += largeEnemiesCount;
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
