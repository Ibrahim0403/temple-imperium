using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ajaz Code
public class DefensiveShield : MonoBehaviour
{
    public float cooldownDefensive = 20; //cooldown time, 20 seconds
    public float abilityUp = 0; //variable to check how long until the ability is back up
    public HPScript HealthBar; //uses the scripted health bar from another script
    public float currentHP;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > abilityUp) //Used to check whether the time conditions for the defensive ability are met
        {
            if (Input.GetKeyDown(KeyCode.U) && PlayerStats.playerHealth <= 100) //allows the player to activate shield with the U key only if the player is at 100 health or lower
            {
                PlayerStats.playerHealth += 50;
                abilityUp = Time.time + cooldownDefensive;
                print("defensive ability used");
                currentHP = PlayerStats.playerHealth;
                HealthBar.SetHP(currentHP);
                HealthBar.UpdatePlayerBar();
            }
        }
    }
}
