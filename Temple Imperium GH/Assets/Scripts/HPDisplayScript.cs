using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPDisplayScript : MonoBehaviour
{
    public GameObject healthObject;

    public string healthDisplay;

    private TextMeshProUGUI textMesH;
    PlayerStats HealthScript;

    void Start()
    {
        textMesH = GetComponent<TextMeshProUGUI>();
        HealthScript = healthObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay = (PlayerStats.playerHealth.ToString()); //displays ammo count
        textMesH.text = healthDisplay;
    }
}
