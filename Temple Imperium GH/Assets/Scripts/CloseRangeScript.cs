using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloseRangeScript : MonoBehaviour
{

    public int meleeDamage = 5;

    private GameObject playerObject;

    private PlayerStats player;

    private bool hitPlayer;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerStats>();
        hitPlayer = false;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        transform.LookAt(playerObject.transform);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3.1f)) //if ray hits something within range
        {
            if (hit.transform.tag == "Player")
            {
                //Debug.Log(hit.transform.gameObject);
                Debug.Log("Melee");
                if (!hitPlayer)
                {
                    hitPlayer = true;
                    StartCoroutine(AttackPlayer());
                }
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        player.Attack(meleeDamage);
        yield return new WaitForSeconds(2f); //delay for next attack
        hitPlayer = false;
    }
}
