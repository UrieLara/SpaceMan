using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    healthBar,
    manaBar
}
public class PlayerBar : MonoBehaviour
{
    Slider slider;
    public BarType type;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        switch (type)
        {
            case BarType.healthBar:
                slider.maxValue = PlayerController.MAX_HEALTH;
                slider.value = PlayerController.INITIAL_HEALT;
                break;
            
            case BarType.manaBar:
                slider.maxValue = PlayerController.MAX_MANA;
                slider.value = PlayerController.INITIAL_MANA;
                break;

        }
    }

    void Update()
    {
        switch (type)
        {
            case BarType.healthBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetHealth();
                break;

            case BarType.manaBar:
                slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetMana();
                break;

        }

        
    }
}
