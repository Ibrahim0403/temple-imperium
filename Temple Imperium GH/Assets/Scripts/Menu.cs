using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Ajaz Code
public class Menu : MonoBehaviour
{
    public void StartGame ()
    {
        //Code to load the next scene, in this case the main game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame ()
    {
        Debug.Log("Application closed");
        //Code to close the game window
        Application.Quit();
    }
}
