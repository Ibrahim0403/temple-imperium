using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    public GameObject prototypeWeapon;

    public GameObject InteractHUD;

    public CharacterController controller; //reference the controller to have access

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; //control what sphere checks for
    public bool isGrounded;
    public bool speedChanged;

    public Vector3 velocity;

    public static bool selectedPoison = false;
    public static bool selectedSpeed = false;
    public static bool selectedSnare = false;
    public static bool selectedFloat = false;
    public static bool selectedNothing = false;

    public static bool increasePoisonCharge = false;
    public static bool increaseSpeedCharge = false;
    public static bool increaseSnareCharge = false;
    public static bool increaseFloatCharge = false;

    GunScript primaryScript;
    GunScript secondaryScript;
    GunScript prototypeScript;

    // Start is called before the first frame update
    void Start()
    {
        speedChanged = false;

        primaryScript = primaryWeapon.GetComponent<GunScript>();

        secondaryScript = secondaryWeapon.GetComponent<GunScript>();

        prototypeScript = prototypeWeapon.GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(speed);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //move player in direction they are facing rather than global direction

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //physics formula for jumping
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); //applies gravity to player

        if (selectedPoison)
        {
            GivePoison();
            selectedPoison = false;
            increasePoisonCharge = true;
        }
        if (selectedSpeed)
        {
            GiveSpeed();
            selectedSpeed = false;
            increaseSpeedCharge = true;
        }
        if (selectedSnare)
        {
            GiveSnare();
            selectedSnare = false;
            increaseSnareCharge = true;
        }
        if (selectedFloat)
        {
            GiveFloat();
            selectedFloat = false;
            increaseFloatCharge = true;
        }
        if (selectedNothing)
        {
            ResetAll();
            selectedNothing = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AmmoBox")
        {
            primaryScript.RefillAmmo();
            secondaryScript.RefillAmmo();
            prototypeScript.RefillAmmo();

            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Generator")
        {
            InteractHUD.SetActive(true);
            GeneratorScript.hasInteracted = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Generator")
        {
            InteractHUD.SetActive(false);
            GeneratorScript.hasInteracted = false;
        }
    }

    void GivePoison()
    {
        prototypeScript.hasPoison = true;
        ResetSpeed();
        ResetSnare();
        ResetFloat();
        //Debug.Log("Got Poison");
    }

    void GiveSpeed()
    {
        if (!speedChanged)
        {
            speed += 5f;
            speedChanged = true;
            EnemyController.speedChanged = true;
        }
        ResetPoison();
        ResetSnare();
        ResetFloat();
        //Debug.Log("Got Speed");
    }

    void GiveSnare()
    {
        prototypeScript.hasSnare = true;
        ResetPoison();
        ResetSpeed();
        ResetFloat();
        //Debug.Log("Got Snare");
    }

    void GiveFloat()
    {
        ResetPoison();
        ResetSpeed();
        ResetSnare();
    }

    void ResetPoison()
    {
        prototypeScript.hasPoison = false;
        increasePoisonCharge = false;
    }

    void ResetSpeed()
    {
        if (speedChanged) 
        { 
            speed -= 5f;
            speedChanged = false;
            EnemyController.speedChanged = false;
            EnemyController.hasOtherStarStone = true;
            increaseSpeedCharge = false;
            //Debug.Log("Got Speed");
        }
    }

    void ResetSnare()
    {
        prototypeScript.hasSnare = false;
        increaseSnareCharge = false;
        //Debug.Log("Got Snare");
    }

    void ResetFloat()
    {
        increaseFloatCharge = false;
    }

    void ResetAll()
    {
        GeneratorScript.resetStoneHUD = true;
        ResetPoison();
        ResetSpeed();
        ResetSnare();
        ResetFloat();
    }
}
