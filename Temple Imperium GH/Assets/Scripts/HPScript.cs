using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image colour;

    //All code below sets and displays health bars
    public void SetHP(float hp)
    {
        slider.value = hp;
    }
    
    public void SetMaxHP(float hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void PlayerHealthSetBar()
    {
        colour.color = gradient.Evaluate(1f);
    }

    public void UpdatePlayerBar()
    {
        colour.color = gradient.Evaluate(slider.normalizedValue);
    }
}
