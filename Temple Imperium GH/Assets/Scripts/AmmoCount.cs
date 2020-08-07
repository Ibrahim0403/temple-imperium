using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{

    public static string ammoDisplay;
    public static string TempAmmo;

    private TextMeshProUGUI textMesH;

    void Start()
    {
        textMesH = GetComponent<TextMeshProUGUI>(); //get text component
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay = (GunScript.ammoMagazine + "/" + GunScript.ammoReserve); //displays ammo count
        textMesH.text = ammoDisplay;
    }
}
