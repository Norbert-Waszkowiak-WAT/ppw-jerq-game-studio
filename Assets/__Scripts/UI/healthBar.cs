using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;

    public Color high;
    public Color low;

    public Image healthBarImage;
    
    public void SetMaxHealth(float health)
    {
        healthText.text = health.ToString();
        slider.maxValue = health;
        slider.value = health;
        healthBarImage.color = high;
    }

    public void SetHealth(float health)
    {
        healthText.text = health.ToString();
        slider.value = health;
        healthBarImage.color = Lerp(low, high, health/slider.maxValue);
    }
    public static Color Lerp(Color startColor, Color endColor, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        float r = Mathf.Lerp(startColor.r, endColor.r, t);
        float g = Mathf.Lerp(startColor.g, endColor.g, t);
        float b = Mathf.Lerp(startColor.b, endColor.b, t);
        return new Color(r, g, b);
    }

}
