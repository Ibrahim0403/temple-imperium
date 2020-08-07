using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public GameObject spawnAmmoBox;

    public GameObject poisonCog;
    public GameObject speedCog;
    public GameObject snareCog;
    public GameObject floatCog;

    public float targetHealth;

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
            ammoBoxRNG = Random.Range(1, 5);
            Vector3 deathPOS = this.gameObject.transform.position;
            TargetDie();
            if (ammoBoxCount == 1 && ammoBoxRNG == 1)
            {
                GameObject ammoBoxGO = Instantiate(spawnAmmoBox, deathPOS + (Vector3.up * 1), Quaternion.identity);
                Destroy(ammoBoxGO, 60f);
            }

            if (PlayerMovement.increasePoisonCharge) //count kills during starstone effects
            {
                FixGeneratorScript.poisonKills += 1;
            }
            if (PlayerMovement.increaseSpeedCharge)
            {
                FixGeneratorScript.speedKills += 1;
            }
            if (PlayerMovement.increaseSnareCharge)
            {
                FixGeneratorScript.snareKills += 1;
            }
            if (PlayerMovement.increaseFloatCharge)
            {
                FixGeneratorScript.floatKills += 1;
            }

            if (FixGeneratorScript.poisonKills == 15) //have to get 15 kills to drop part
            {
                GameObject poisonCogGO = Instantiate(poisonCog, deathPOS + (Vector3.up * 1), Quaternion.identity);
                FixGeneratorScript.poisonKills += 1;
            }
            if (FixGeneratorScript.speedKills == 15)
            {
                GameObject speedCogGO = Instantiate(speedCog, deathPOS + (Vector3.up * 1), Quaternion.identity);
                FixGeneratorScript.speedKills += 1;
            }
            if (FixGeneratorScript.snareKills == 15)
            {
                GameObject snareCogGO = Instantiate(snareCog, deathPOS + (Vector3.up * 1), Quaternion.identity);
                FixGeneratorScript.snareKills += 1;
            }
            if (FixGeneratorScript.floatKills == 15)
            {
                GameObject floatCogGO = Instantiate(floatCog, deathPOS + (Vector3.up * 1), Quaternion.identity);
                FixGeneratorScript.floatKills += 1;
            }

            //Debug.Log(FixGeneratorScript.poisonKills);
        }
    }

    void TargetDie()
    {
        roundManager.KillEnemy();
        Destroy(this.gameObject);
    }
}
