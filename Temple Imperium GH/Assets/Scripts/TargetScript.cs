using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public GameObject spawnAmmoBox;

    public float targetHealth = 50f;

    public HPScript hpBar;

    public int ammoBoxCount;
    public int ammoBoxRNG;

    private RoundManagerScript roundManager;

    public void Init(RoundManagerScript _roundManager)
    {
        roundManager = _roundManager;
    }

    void Start()
    {
        hpBar.SetMaxHP(targetHealth);
    }
    void Update()
    {
        ammoBoxCount = GameObject.FindGameObjectsWithTag("AmmoBox").Length;
    }

    public void RecieveDamage(float inflicted) //pass the amount of damage taken
    {
        targetHealth -= inflicted; //take damage off health
        hpBar.SetHP(targetHealth);

        if (targetHealth <= 0.0f)
        {
            ammoBoxRNG = Random.Range(1, 1);
            Vector3 deathPOS = this.gameObject.transform.position;
            TargetDie();
            if (ammoBoxCount == 1 && ammoBoxRNG == 1)
            {
                GameObject ammoBoxGO = Instantiate(spawnAmmoBox, deathPOS + (Vector3.up * 1), Quaternion.identity);
                Destroy(ammoBoxGO, 60f);
            }
        }
    }

    void TargetDie()
    {
        roundManager.KillEnemy();
        Destroy(this.gameObject);
    }
}
