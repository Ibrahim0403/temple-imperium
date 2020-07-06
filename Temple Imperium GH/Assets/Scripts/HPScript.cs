using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    public Slider slider;

    public void SetHP(float hp)
    {
        slider.value = hp;
    }
    
    public void SetMaxHP(float hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }
}
