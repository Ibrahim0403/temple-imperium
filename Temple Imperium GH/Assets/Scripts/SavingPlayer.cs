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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Xposition = transform.position.x;
        Yposition = 2f;
        Zposition = transform.position.z;

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SavePosition.x = Xposition;
            SavePosition.y = Yposition;
            SavePosition.z = Zposition;

            PlayerStats.TempHealth = PlayerStats.playerHealth;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerStats.playerHealth = PlayerStats.TempHealth;
            transform.position = SavePosition;
        }
    }
}
