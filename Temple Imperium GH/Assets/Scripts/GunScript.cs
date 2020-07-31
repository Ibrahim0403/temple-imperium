using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public float gunDamage = 10f;
    public float gunRange = 10f;
    public float gunROF = 10f; //rate of fire
    public float reloadTime = 1f;
    public float defaultDamage;

    public Camera fpsCamera; //reference the fps camera

    public GameObject muzzleFlash;
    public GameObject hitEffect;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    public float bulletPOS = 1.0f;

    public bool hasPoison = false;
    public bool hasSnare = false;

    public int ammoMagCurrent = 30;
    public int ammoReserveCurrent = 120;
    int ammoMagMax;
    int ammoReserveMax;

    //access in other scripts
    public static int ammoMagazine;
    public static int ammoReserve;

    public static bool hasRefilled = false;

    private float nextShot = 0f;
    private bool isReloading = false;
    private bool isEmpty;

    public Animator animator;

    void Start()
    {
        defaultDamage = gunDamage;
        ammoMagMax = ammoMagCurrent;
        ammoReserveMax = ammoReserveCurrent;
    }

    void OnEnable()
    {
        if (ammoMagCurrent <= 0 && ammoReserveCurrent >= ammoMagMax) //deplete ammo
        {
            StartCoroutine(Reload(3)); //start reload coroutine
        }
        else if (ammoMagCurrent <= 0 && ammoReserveCurrent < ammoMagMax)
        {
            StartCoroutine(Reload(1));
        }
        else
        {
            isReloading = false;
            animator.SetBool("Reloading", false);
        }
        animator.SetBool("Aiming", false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(nextShot);
        ammoMagazine = ammoMagCurrent;
        ammoReserve = ammoReserveCurrent;
        if (hasRefilled)
        {
            RefillAmmo();
            hasRefilled = false;
        }
        if (Input.GetButton("Fire2")){ //aiming weapon in
            animator.SetBool("Aiming", true);
        }
        else
        {
            animator.SetBool("Aiming", false);
        }
        if (ammoMagCurrent == 0 && ammoReserveCurrent == 0)
        {
            isReloading = true;
            animator.SetBool("Reloading", true);
            isEmpty = true;
        }
        else
        {
            if (isEmpty) //if gun is completely empty
            {
                StartCoroutine(Reload(3));
                isEmpty = false;
            }
            if (isReloading)
                return;

            if (Input.GetButtonDown("Fire1") && Time.time >= nextShot) //check if fire button is pressed and next shot is ready
            {
                nextShot = Time.time + 1f / gunROF;

                if (ammoReserveCurrent != 0 || ammoMagCurrent != 0)
                {
                    GunShoot(); //call shoot function
                }
            }
            if (ammoReserveCurrent > 0)
            {
                if (Input.GetKeyDown(KeyCode.R) && ammoMagCurrent != ammoMagMax) //deplete ammo with reload button
                {
                    if ((ammoMagMax - ammoMagCurrent) > ammoReserveCurrent)
                    {
                        StartCoroutine(Reload(1));
                    }
                    else
                    {
                        StartCoroutine(Reload(2));
                    }
                }
            }
        }

        if (StoneChargeScript.enemyChargeFloat) //increase enemy health by reducing weapon damage
        {
            gunDamage = defaultDamage / 1.5f;
        }
        else
        {
            gunDamage = defaultDamage;
        }
    }

    void GunShoot()
    {
        muzzleFlash.SetActive(true);

        RaycastHit hit;

        GameObject instantiateBullet = Instantiate(bullet, transform.position + (Vector3.up * bulletPOS), Quaternion.identity) as GameObject; //instantiate another bullet
        Rigidbody instantiateBulletRB = instantiateBullet.GetComponent<Rigidbody>();
        instantiateBulletRB.velocity = -transform.right * bulletSpeed;

        Invoke("DisplayMuzzle", 0.05f); //display muzzle flash
        Destroy(instantiateBullet, 7f);

        if (ammoMagCurrent > 0)
        {
            ammoMagCurrent -= 1; //reduce ammo
        }


        if (ammoMagCurrent <= 0 && ammoReserveCurrent >= ammoMagMax) //deplete ammo
        {
            StartCoroutine(Reload(3)); //start reload coroutine
        } else if (ammoMagCurrent <= 0 && ammoReserveCurrent < ammoMagMax)
        {
            StartCoroutine(Reload(1));
        }

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, gunRange)) //if ray hits something within range
        {

            TargetScript target = hit.transform.GetComponent<TargetScript>();
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();

            if (target != null)
            {
                target.RecieveDamage(gunDamage);
                if (hasPoison) //applies poison
                {
                        StartCoroutine(ApplyPoison(target));
                }
            }

            if (enemy != null)
            {
                //Debug.Log("got script");
                if (hasSnare) //applies snare
                {
                    StartCoroutine(ApplySnare(enemy));
                }
            }

            GameObject hitGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); //display particle face upwards
            Destroy(hitGO, 2f);
        }
    }

    IEnumerator Reload(int situation)
    {
        if (ammoReserveCurrent != 0)
        {
            isReloading = true;
            animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(reloadTime);
            animator.SetBool("Reloading", false);
            isReloading = false;
        }

        switch (situation) //different types of situation when reloading
        {
            case 1:
                ammoMagCurrent += ammoReserveCurrent;
                ammoReserveCurrent -= ammoReserveCurrent;
                break;
            case 2:
                ammoReserveCurrent -= (ammoMagMax - ammoMagCurrent);
                ammoMagCurrent += (ammoMagMax - ammoMagCurrent);
                break;
            case 3:
                ammoMagCurrent = ammoMagMax;
                ammoReserveCurrent -= ammoMagMax;
                break;
        }
    }

    IEnumerator ApplyPoison(TargetScript target)
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(2);
            target.RecieveDamage(5);
            //Debug.Log("Damage");
        }
    }

    IEnumerator ApplySnare(EnemyController enemy)
    {
        enemy.speedSlowed = true;
        yield return new WaitForSeconds(3);
        enemy.revertSpeed = true;
    }

    public void RefillAmmo()
    {
        ammoReserveCurrent += ammoReserveMax - ammoReserveCurrent;
    }

    void DisplayMuzzle()
    {
        muzzleFlash.SetActive(false);
    }
}
