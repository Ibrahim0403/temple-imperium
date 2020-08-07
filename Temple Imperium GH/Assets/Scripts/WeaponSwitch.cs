using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int currentWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(true);
            weapon.gameObject.SetActive(false);
        }
        GetWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int priorWeapon = currentWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(currentWeapon >= transform.childCount - 1) //Loop through weapons
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon <= 0) //Loop the opposite way
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) //switch weapon with number keycodes
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            currentWeapon = 2;
        }



        if (priorWeapon != currentWeapon)
        {
            GetWeapon();
        }
    }

    void GetWeapon()
    {
        int index = 0; //current index set to 0 - primary weapon
        foreach (Transform weapon in transform)
        {
            if (index == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            index++;
        }
    }
}
