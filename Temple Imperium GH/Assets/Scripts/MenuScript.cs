using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Ajaz Code
public class MenuScript : MonoBehaviour
{
    public void StartGame ()
    {
        //Code to load the next scene, in this case the main game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PauseMenu.Paused = false;
    }

    public void ExitGame ()
    {
        //Code to close the game window
        Application.Quit();
    }
}
