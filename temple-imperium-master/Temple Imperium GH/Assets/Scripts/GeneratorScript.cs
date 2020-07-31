using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject GeneratorUI;
    public GameObject playerPoison, playerSpeed, playerSnare, playerFloat, enemyPoison, enemySpeed, enemySnare, enemyFloat;

    public bool isOpen = false;

    public static bool hasInteracted = false;
    public static bool resetStoneHUD = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInteracted)
        {
            if (Input.GetKeyDown(KeyCode.I) && isOpen)
            {
                CloseUI();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                GeneratorUI.SetActive(true);
                isOpen = true;
            }
        }
        else if (!hasInteracted)
        {
            CloseUI();
        }

        if (StoneChargeScript.enemyChargePoison)
        {
            enemyPoison.SetActive(true);
        }
        else
        {
            enemyPoison.SetActive(false);
        }

        if (StoneChargeScript.enemyChargeSpeed)
        {
            enemySpeed.SetActive(true);
        }
        else
        {
            enemySpeed.SetActive(false);
        }

        if (StoneChargeScript.enemyChargeSnare)
        {
            enemySnare.SetActive(true);
        }
        else
        {
            enemySnare.SetActive(false);
        }

        if (StoneChargeScript.enemyChargeFloat)
        {
            enemyFloat.SetActive(true);
        }
        else
        {
            enemyFloat.SetActive(false);
        }

        if (resetStoneHUD)
        {
            ResetHUD();
            resetStoneHUD = false;
        }
    }

    void CloseUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GeneratorUI.SetActive(false);
        isOpen = false;
    }

    public void PlayerPoison()
    {
        PlayerMovement.selectedPoison = true;
        playerPoison.SetActive(true);
        playerSpeed.SetActive(false);
        playerSnare.SetActive(false);
        playerFloat.SetActive(false);
    }

    public void PlayerSpeed()
    {
        PlayerMovement.selectedSpeed = true;
        playerPoison.SetActive(false);
        playerSpeed.SetActive(true);
        playerSnare.SetActive(false);
        playerFloat.SetActive(false);
    }

    public void PlayerSnare()
    {
        PlayerMovement.selectedSnare = true;
        playerPoison.SetActive(false);
        playerSpeed.SetActive(false);
        playerSnare.SetActive(true);
        playerFloat.SetActive(false);
    }

    public void PlayerFloat()
    {
        PlayerMovement.selectedFloat = true;
        playerPoison.SetActive(false);
        playerSpeed.SetActive(false);
        playerSnare.SetActive(false);
        playerFloat.SetActive(true);
    }

    void ResetHUD()
    {
        PlayerMovement.selectedPoison = false;
        PlayerMovement.selectedSpeed = false;
        PlayerMovement.selectedSnare = false;
        PlayerMovement.selectedFloat = false;
        playerPoison.SetActive(false);
        playerSpeed.SetActive(false);
        playerSnare.SetActive(false);
        playerFloat.SetActive(false);
    }
}
