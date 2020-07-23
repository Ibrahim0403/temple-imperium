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

    // Start is called before the first frame update
    void Start()
    {
        WeaponText = GetComponent<TextMeshProUGUI>();
        WeaponType = "Primary";
    }

    // Update is called once per frame
    void Update()
    {
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
