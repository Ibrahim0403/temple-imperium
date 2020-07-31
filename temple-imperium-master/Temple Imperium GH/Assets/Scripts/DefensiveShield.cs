using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ajaz Code
public class DefensiveShield : MonoBehaviour
{
    public float cooldownDefensive = 20;
    public float abilityUp = 0;
    public HPScript HealthBar;
    public float currentHP;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > abilityUp)
        {
            if (Input.GetKeyDown(KeyCode.U) && PlayerStats.playerHealth <= 100)
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
