using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Ajaz Code
public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false; //Checks if the game is paused

    public GameObject PauseMenuUI; //References the game UI

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) //Used to pause the game, or resume the game based on whether the game is currently paused or not
        {
            if(Paused == true)
            {
                Resume();
            }
            else
            {
                Pause();

            }
        }
    }
    public void Resume() //Returns the game to running as normal outside of menus, with time resuming and the camera working
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Pause() //Stops the game's time and disables the camera, allowing the user to click on buttons with a cursor
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void MenuLoad() //Changes the scene backt to the main menu when the associated button is clicked
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void QuitGame() //Closes the game when the associated button is clicked
    {
        Application.Quit();
    }
}
