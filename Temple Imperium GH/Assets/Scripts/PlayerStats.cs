using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject DamageScreen;

    public int playerHealth = 100;

    void Start()
    {
        DamageScreen.SetActive(false);
    }

    public void Attack(int dmg)
    {
        DamageScreen.SetActive(true);
        playerHealth -= dmg;
        Debug.Log("HEALTH: " + playerHealth);
        Invoke("DisplayDamage", 0.1f);
    }

    void DisplayDamage()
    {
        DamageScreen.SetActive(false);
    }
}
