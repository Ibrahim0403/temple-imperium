using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{

    public string ammoDisplay;

    private TextMeshProUGUI textMesH;

    void Start()
    {
        textMesH = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay = (GunScript.ammoMagazine + "/" + GunScript.ammoReserve); //displays ammo count
        textMesH.text = ammoDisplay;
    }
}
