using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileSpeed = 10f;

    public int ROFincrease; //rate of fire increase
    public int destroyIncrease;
    public int projectileDamage;
    public int boostDamage;

    public static bool increaseDamage;

    void Start()
    {
        projectileDamage = 10;
        boostDamage = 0;
    }

    void Update()
    {
        if (StoneChargeScript.enemyChargeSnare) //inrease enemey rate of fire when enemy has snare charge
        {
            ROFincrease = 5;
            destroyIncrease = 1;
        }
        else
        {
            ROFincrease = 0;
            destroyIncrease = 0;
        }

        if (increaseDamage)
        {
            boostDamage = 10;
        }
        else
        {
            boostDamage = 0;
        }

        transform.Translate(0, 0, (projectileSpeed + ROFincrease) * Time.deltaTime); //move projectile
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if(player != null){
            player.Attack(projectileDamage + boostDamage); //damage player when projectile collides with player
        }
        Destroy(this.gameObject, (3 - destroyIncrease));
    }
}
