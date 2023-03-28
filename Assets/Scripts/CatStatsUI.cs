using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatStatsUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider hungerBar;

    private Cat _cat;

    // Start is called before the first frame update
    void Start()
    {
        _cat = FindObjectOfType<Cat>();

        _cat.HealthBar = healthBar;
        healthBar.maxValue = _cat.maxHealth;
        healthBar.value = _cat.CurrentHealth;

        _cat.HungerBar = hungerBar;
        hungerBar.maxValue = _cat.maxHunger;
        hungerBar.value = _cat.CurrentHunger;
    }
}
