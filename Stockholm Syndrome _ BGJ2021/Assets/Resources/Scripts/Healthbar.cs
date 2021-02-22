using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    [Header ("Health UI")]
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth (float health, float overflowHealth)
    {
        slider.maxValue = overflowHealth;
        slider.value = health;

        fill.color = gradient.Evaluate((slider.normalizedValue));
    }

    public void SetHealthUI(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
