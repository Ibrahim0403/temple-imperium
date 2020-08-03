using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject DamageScreen;

    public HPScript hpBar;

    public int pHealth = 100;

    public static int playerHealth = 100;

    //new code
    public static int ShieldHealth = 50;

    public static int TempHealth;
    //new code end

    private int maxHealth;

    private float timeCheck = 0.0f;

    private bool isRegenerating;

    void Start()
    {
        playerHealth = pHealth;
        maxHealth = playerHealth;
        hpBar.SetMaxHP(playerHealth);
        hpBar.PlayerHealthSetBar();
        DamageScreen.SetActive(false);
    }

    void Update()
    {
        if (playerHealth != maxHealth && !isRegenerating)
        {
            StartCoroutine(RegenHealth());
        }

 

        //Debug.Log(timeCheck);


    }

    public void Attack(int dmg) //damage player
    {
        DamageScreen.SetActive(true);
        playerHealth -= dmg;
        hpBar.SetHP(playerHealth);
        hpBar.UpdatePlayerBar();
        //Debug.Log("HEALTH: " + playerHealth);
        Invoke("DisplayDamage", 0.1f);
        timeCheck = Time.time;
    }

    void DisplayDamage()
    {
        DamageScreen.SetActive(false);
    }

    IEnumerator RegenHealth()
    {
        isRegenerating = true;
        while ((playerHealth < maxHealth) && Time.time > (timeCheck + 5.0f)) { //regenerate if not been attacked for 5 seconds
            playerHealth++;
            hpBar.SetHP(playerHealth);
            hpBar.UpdatePlayerBar();
            timeCheck = 0.0f;
            yield return new WaitForSeconds(0.1f);
        }
        isRegenerating = false;
    }
}
