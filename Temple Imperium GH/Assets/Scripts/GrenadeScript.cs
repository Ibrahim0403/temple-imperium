using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public GameObject grenadeEffect;

    public float delay = 2.5f;
    public float radius = 5f;
    public float force = 700f;
    public float countdown;

    public bool hasBlownUp = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasBlownUp)
        {
            hasBlownUp = true;
            BlowUp();
        }
    }

    void BlowUp()
    {
        GameObject effectGO = Instantiate(grenadeEffect, transform.position, transform.rotation);
        Destroy(effectGO, 2f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); //get all objects colliding with sphere

        foreach (Collider closeObject in colliders)
        {
            Rigidbody enemyRB = closeObject.GetComponent<Rigidbody>();

            if (enemyRB != null)
            {
                enemyRB.AddExplosionForce(force, transform.position, radius); //apply explosion force
            }

            TargetScript target = closeObject.GetComponent<TargetScript>();
            if(target != null)
            {
                target.RecieveDamage(45f);
            }
        }

        Destroy(gameObject);
    }
}
