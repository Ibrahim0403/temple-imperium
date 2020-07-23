using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Ajaz Code
public class ShieldStatusUI : MonoBehaviour
{
    public string ShieldStatus;
    public float cooldown = 20;
    public float abilityUp = 0;

    TextMeshProUGUI ShieldText;

    // Start is called before the first frame update
    void Start()
    {
        ShieldText = GetComponent<TextMeshProUGUI>();
        ShieldStatus = "Shield Available";
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > abilityUp)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                abilityUp = Time.time + cooldown;
                ShieldStatus = "Shield Unavailable";
                ShieldText.text = ShieldStatus;
            }
            else
            {
                ShieldStatus = "Shield Available";
                ShieldText.text = ShieldStatus;
            }
        }

    }
}
