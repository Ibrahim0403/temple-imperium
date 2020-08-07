using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    public GameObject prototypeWeapon;

    public GameObject InteractHUD;
    public GameObject cogInteractHUD;

    public CharacterController controller; //reference the controller to have access

    public float speed = 12f; //Current speed of the player
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;

    //AJAZ CODE - START
    public float basespeed = 12f; //Registers the walking speed
    public float doublespeed; //Registers the running speed

    AudioSource audioSource; //References the AudioSource component in Unity

    private bool isCrouching = false; //Checks if the player is in a crouching state

    public float Xposition;
    public float Yposition;
    public float Zposition;
    Vector3 SavePosition;

    //AJAZ CODE - END

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; //control what sphere checks for
    public bool isGrounded;
    public bool speedChanged;
    public bool floatChanged;

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
        //AJAZ CODE - START
        audioSource = GetComponent<AudioSource>();
        //AJAZ CODE - END

        speedChanged = false;

        primaryScript = primaryWeapon.GetComponent<GunScript>();

        secondaryScript = secondaryWeapon.GetComponent<GunScript>();

        prototypeScript = prototypeWeapon.GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(speed);
        doublespeed = basespeed + 4f;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        //AJAZ CODE - START
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && controller.height == 3f) //Allows the player to sprint if they are not in a crouching state
        {
            speed = doublespeed;
        }
        else
        {
            speed = basespeed;
        }

        if (controller.isGrounded == true && controller.velocity.magnitude > 1f && audioSource.isPlaying == false) //plays the ground collision sound when landing on the ground after a jump or crouching
        {
            audioSource.volume = Random.Range(0.8f, 1f);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.C)) //Changes the player to a crouching state if they are not in one, or reverts them back to normal if they are
        {
            if (isCrouching == false)
            {
                controller.height = 1.5f;
                isCrouching = true;
            }
            else
            {
                controller.height = 3f;
                isCrouching = false;
            }
        }


        //AJAZ CODE - END

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

        if (other.gameObject.tag == "LabNote")
        {
            InteractHUD.SetActive(true);
            other.gameObject.GetComponent<LabNoteScript>().NoteAvailable = true;
        }

        if (other.gameObject.tag == "PoisonCog")
        {
            cogInteractHUD.SetActive(true);
            FixGeneratorScript.pickPoisonCOG = true;
        }

        if (other.gameObject.tag == "SpeedCog")
        {
            cogInteractHUD.SetActive(true);
            FixGeneratorScript.pickSpeedCOG = true;
        }

        if (other.gameObject.tag == "SnareCog")
        {
            cogInteractHUD.SetActive(true);
            FixGeneratorScript.pickSnareCOG = true;
        }

        if (other.gameObject.tag == "FloatCog")
        {
            cogInteractHUD.SetActive(true);
            FixGeneratorScript.pickFloatCOG = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Generator")
        {
            InteractHUD.SetActive(false);
            GeneratorScript.hasInteracted = false;
        }

        if (other.gameObject.tag == "LabNote")
        {
            InteractHUD.SetActive(false);
            other.gameObject.GetComponent<LabNoteScript>().NoteAvailable = false;
        }

        if (other.gameObject.tag == "PoisonCog")
        {
            FixGeneratorScript.pickPoisonCOG = false;
            cogInteractHUD.SetActive(false);
        }

        if (other.gameObject.tag == "SpeedCog")
        {
            FixGeneratorScript.pickPoisonCOG = false;
            cogInteractHUD.SetActive(false);
        }

        if (other.gameObject.tag == "SnareCog")
        {
            cogInteractHUD.SetActive(false);
            FixGeneratorScript.pickSnareCOG = false;
        }

        if (other.gameObject.tag == "FloatCog")
        {
            cogInteractHUD.SetActive(false);
            FixGeneratorScript.pickFloatCOG = false;
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
            basespeed += 5f;
            speedChanged = true;
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
        if (!floatChanged)
        {
            jumpHeight += 3f;
            floatChanged = true;
        }
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
            basespeed -= 5f;
            speedChanged = false;
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
        if (floatChanged)
        {
            jumpHeight -= 3f;
            floatChanged = false;
            increaseFloatCharge = false;
        }
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
