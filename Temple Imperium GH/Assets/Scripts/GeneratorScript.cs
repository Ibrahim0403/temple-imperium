using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject GeneratorUI;
    public GameObject playerPoison, playerSpeed, playerSnare, enemyPoison, enemySpeed, enemySnare;

    public bool isOpen = false;

    public static bool hasInteracted = false;

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
    }

    public void PlayerSpeed()
    {
        PlayerMovement.selectedSpeed = true;
        playerPoison.SetActive(false);
        playerSpeed.SetActive(true);
        playerSnare.SetActive(false);
    }

    public void PlayerSnare()
    {
        PlayerMovement.selectedSnare = true;
        playerPoison.SetActive(false);
        playerSpeed.SetActive(false);
        playerSnare.SetActive(true);
    }

    public void PlayerFloat()
    {

    }
}
