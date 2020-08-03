using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Ajaz Code
public class SettingsScript : MonoBehaviour
{

    public AudioMixer mixer;
    public void VolumeSlider(float volume)
    {
        mixer.SetFloat("Sound", volume); //code to influence the volume mixer allowing all audio associated to be made louder or quiet
    }
}
