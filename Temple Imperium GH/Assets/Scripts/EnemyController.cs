using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    public bool speedSlowed;
    public bool revertSpeed;

    public GameObject[] enemies;

    public static bool speedChanged;
    public static bool hasOtherStarStone;

    private EnemyController enemyController;
    private CloseRangeScript closeRangeScript;
    private TargetScript targetScript;

    private float defaultSpeed;

    Transform character;
    NavMeshAgent agent;

    void Start()
    {
        speedChanged = false;
        revertSpeed = false;
        hasOtherStarStone = false;
        character = PlayerManager.instance.player.transform; //get playerg
        agent = GetComponent<NavMeshAgent>();
        defaultSpeed = agent.speed;
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Vector3.Distance(character.position, transform.position); //gets distance between player and enemy

        if (distance <= lookRadius) //check if player should be chased
        {
            agent.SetDestination(character.position); //chase player
        }

        if (speedChanged) //increased speed
        {
            ChangeSpeed();
        }

        if (speedSlowed)
        {
            SpeedDecrease();
        }

        if (revertSpeed || hasOtherStarStone)
        {
            ResetSpeed();
        }

        if (StoneChargeScript.enemyChargePoison)
        {
            DamageBoost();
        }

        if (!StoneChargeScript.enemyChargePoison)
        {
            DamageRevert();
        }
    }

    void ChangeSpeed()
    {
        foreach (GameObject enemy in enemies)
        {
            enemyController = enemy.GetComponent<EnemyController>();
            enemyController.agent.speed += 1;
        }
        speedChanged = false;
    }

    void ResetSpeed()
    {
        foreach (GameObject enemy in enemies)
        {
            enemyController = enemy.GetComponent<EnemyController>();
            enemyController.agent.speed = enemyController.defaultSpeed;
            enemyController.revertSpeed = false;
        }
        hasOtherStarStone = false;
    }

    void SpeedDecrease()
    {
        agent.speed = 0.5f;
        speedSlowed = false;
    }

    void DamageBoost()
    {
        foreach (GameObject enemy in enemies)
        {
            closeRangeScript = enemy.GetComponent<CloseRangeScript>();
            closeRangeScript.hasDamageBoost = true;
            ProjectileScript.increaseDamage = true;
        }
    }

    void DamageRevert()
    {
        foreach (GameObject enemy in enemies)
        {
            closeRangeScript = enemy.GetComponent<CloseRangeScript>();
            closeRangeScript.hasDamageBoost = false;
            ProjectileScript.increaseDamage = false;           
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
