using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ajaz Code
public class SavingPlayer : MonoBehaviour
{
    Vector3 SavePosition;
    public float Xposition;
    public float Yposition;
    public float Zposition;

    void Start()
    {
        
    }

    void Update()
    {
        //Saves the player's current position as variables
        Xposition = transform.position.x;
        Yposition = 2f;
        Zposition = transform.position.z;

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //Stores the values of the variables containing the player's position into temporary variables
            SavePosition.x = Xposition;
            SavePosition.y = Yposition;
            SavePosition.z = Zposition;

            //Stores the player's current health as a variable
            PlayerStats.TempHealth = PlayerStats.playerHealth;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //Changres the player's health to what it was last saved as
            PlayerStats.playerHealth = PlayerStats.TempHealth;
            //Changes the player's position to what it was last saved as
            transform.position = SavePosition;
        }
    }
}
