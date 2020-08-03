using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenadeScript : MonoBehaviour
{
    public GameObject Grenade;

    public float throwForce = 45f;
    public float grenadeCooldown = 45f;
    public float grenadeAvailable;

    // Update is called once per frame
    void Update()
    {
        if ((Time.time > grenadeAvailable) && Input.GetKeyDown(KeyCode.Y))
        {
            grenadeAvailable = Time.time + grenadeCooldown;
            GameObject grenadeGO = Instantiate(Grenade, transform.position, transform.rotation);
            Rigidbody grenadeRB = grenadeGO.GetComponent<Rigidbody>();
            grenadeRB.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }

        //Debug.Log(Time.time + " grenade available in: " + grenadeAvailable);
    }
}
