using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Ajaz Code
public class WeaponUI : MonoBehaviour
{
    public string WeaponType;

    TextMeshProUGUI WeaponText;

    void Start()
    {
        //References the TextMeshPro object in Unity, to allow the text to be changed from within this script
        WeaponText = GetComponent<TextMeshProUGUI>();
        WeaponType = "Primary";
    }

    void Update()
    {
        //Changes the text displayed under the ammo to fit whatever weapon type is currently being used
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponType = "Primary";
            WeaponText.text = WeaponType;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponType = "Secondary";
            WeaponText.text = WeaponType;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponType = "Prototype";
            WeaponText.text = WeaponType;
        }
    }
}
