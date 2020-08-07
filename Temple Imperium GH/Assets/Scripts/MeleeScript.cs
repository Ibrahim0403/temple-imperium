using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public GameObject MeleeWeapon;

    public float meleeDamage;
    public float meleeRange;

    public static bool hasAttacked;
    public static bool isAttacking;

    public Camera fpsCamera;

    void Start()
    {
        hasAttacked = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasAttacked && !isAttacking)
        {
            isAttacking = true;
            MeleeWeapon.SetActive(true);
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, meleeRange)) //if ray hits something within range
        {
            TargetScript target = hit.transform.GetComponent<TargetScript>(); //get enemy that has been hit

            if (target != null)
            {
                target.RecieveDamage(meleeDamage);
                Debug.Log("yes");
            }
        }
        StartCoroutine(DisableMelee());
    }

    IEnumerator DisableMelee()
    {
        yield return new WaitForSeconds(0.75f);
        hasAttacked = false;
        isAttacking = false;
        MeleeWeapon.SetActive(false);
    }
}