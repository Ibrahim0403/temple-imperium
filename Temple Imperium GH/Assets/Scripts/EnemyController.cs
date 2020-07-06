using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    public bool speedSlowed;

    public bool revertSpeed;

    public static bool speedChanged;

    public static bool hasOtherStarStone;

    private float defaultSpeed;

    Transform character;
    NavMeshAgent agent;

    void Start()
    {
        speedChanged = false;
        revertSpeed = false;
        hasOtherStarStone = false;
        character = PlayerManager.instance.player.transform; //get player
        agent = GetComponent<NavMeshAgent>();
        defaultSpeed = agent.speed;
    }

    void Update()
    {
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
    }

    void ResetSpeed()
    {
        agent.speed = defaultSpeed;
        revertSpeed = false;
        hasOtherStarStone = false;
    }

    void ChangeSpeed()
    {
        agent.speed += 1;
        speedChanged = false;
    }

    void SpeedDecrease()
    {
        agent.speed = 0.5f;
        speedSlowed = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
