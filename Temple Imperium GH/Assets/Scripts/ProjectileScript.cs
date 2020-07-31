using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileSpeed = 10f;
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
        if (increaseDamage)
        {
            boostDamage = 10;
        }
        else
        {
            boostDamage = 0;
        }

        transform.Translate(0, 0, projectileSpeed * Time.deltaTime); //move projectile
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if(player != null){
            player.Attack(projectileDamage + boostDamage);
        }
        Destroy(this.gameObject, 3f);
    }
}
