using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Ajaz Code
public class ShieldStatusUI : MonoBehaviour
{

    public string ShieldStatus; //used to display whether the shield can or cannot be used to the player
    public float cooldown = 20;
    public float abilityUp = 0;

    TextMeshProUGUI ShieldText;

    void Start()
    {
        //references the TextMeshPro object in Unity to allow the text to be altered using this script
        ShieldText = GetComponent<TextMeshProUGUI>();
        ShieldStatus = "Shield Available";
    }

    void Update()
    {
        if (Time.time > abilityUp) //Checks whether the ability's time conditions have been met
        {
            if (Input.GetKeyDown(KeyCode.U))
            //Changes the text to display that the shield is unavailable if it has been used and the time conditions are no longer met 
            {
                abilityUp = Time.time + cooldown;
                ShieldStatus = "Shield Unavailable";
                ShieldText.text = ShieldStatus;
            }
            else
            //changes the text to show that the ability is available is the conditions have been met
            {
                ShieldStatus = "Shield Available";
                ShieldText.text = ShieldStatus;
            }
        }

    }
}
