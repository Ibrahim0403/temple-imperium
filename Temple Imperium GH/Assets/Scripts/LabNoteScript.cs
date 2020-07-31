using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabNoteScript : MonoBehaviour
{
    public GameObject NoteHUD;

    public bool openedNote;
    public bool NoteAvailable;

    void Start()
    {
        NoteAvailable = false;
        openedNote = false;
    }

    void Update()
    {
        if (NoteAvailable) //opening notes code
        {
            if (Input.GetKeyDown(KeyCode.I) && openedNote)
            {
                CloseNoteHUD();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Time.timeScale = 0f;
                NoteHUD.SetActive(true);
                openedNote = true;
            }
        }
    }

    void CloseNoteHUD()
    {
        Time.timeScale = 1f;
        NoteHUD.SetActive(false);
        openedNote = false;
    }
}
