using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //singleton code below needed to make sure game functions e.g. enemies follow for the entire game

    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player; 
}
