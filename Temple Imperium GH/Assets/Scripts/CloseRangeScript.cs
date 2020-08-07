using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseRangeScript : MonoBehaviour
{
    public int meleeDamage;
    public int boostDamage;

    public bool hasDamageBoost;

    public float ROFincrease; //rate of fire increase

    private GameObject playerObject;

    private PlayerStats player;

    private bool hitPlayer;

    void Start()
    {
        //set variable values
        hasDamageBoost = false;
        meleeDamage = 10;
        boostDamage = 0;
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerStats>();
        hitPlayer = false;
    }

    void FixedUpdate()
    {
        if (hasDamageBoost) //boost enemy damage
        {
            boostDamage = 10;
        }
        else
        {
            boostDamage = 0;
        }

        if (StoneChargeScript.enemyChargeSnare)
        {
            ROFincrease = 1f;
        }
        else
        {
            ROFincrease = 0f;
        }

        RaycastHit hit;
        transform.LookAt(playerObject.transform);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3.1f)) //if ray hits something within range
        {
            if (hit.transform.tag == "Player")
            {
                //Debug.Log(hit.transform.gameObject);
                //Debug.Log("Melee");
                if (!hitPlayer)
                {
                    hitPlayer = true;
                    StartCoroutine(AttackPlayer()); //run coroutine to attack player
                }
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        player.Attack(meleeDamage + boostDamage);
        yield return new WaitForSeconds(2f - ROFincrease); //delay for next attack
        hitPlayer = false;
    }
}
