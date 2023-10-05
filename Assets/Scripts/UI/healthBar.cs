using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;
    
    public void SetMaxHealth(float health)
    {
        healthText.text = health.ToString();
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        healthText.text = health.ToString();
        slider.value = health;
    }
}
