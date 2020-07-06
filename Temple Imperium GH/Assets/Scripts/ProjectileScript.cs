using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public int projectileDamage = 10;

    void Update()
    {
        transform.Translate(0, 0, projectileSpeed * Time.deltaTime); //move projectile

    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if(player != null){
            player.Attack(projectileDamage);
        }
        Destroy(this.gameObject, 3f);
    }
}
