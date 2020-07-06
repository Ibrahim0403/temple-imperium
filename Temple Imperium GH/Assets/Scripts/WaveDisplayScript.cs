using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplayScript : MonoBehaviour
{
    public GameObject roundManager;
    RoundManagerScript roundManagerScript;

    public string waveDisplay;

    private TextMeshProUGUI textMesH;


    // Start is called before the first frame update
    void Start()
    {
        textMesH = GetComponent<TextMeshProUGUI>();
        roundManagerScript = roundManager.GetComponent<RoundManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        waveDisplay = ("Round: " + roundManagerScript.activeRound + "\nEnemies Left: " + roundManagerScript.maxEnemiesPerRound + "\nKills: " + roundManagerScript.killCounter + "\nTime: " + Mathf.Round(roundManagerScript.roundTime)); //displays ammo count
        textMesH.text = waveDisplay;
    }
}
