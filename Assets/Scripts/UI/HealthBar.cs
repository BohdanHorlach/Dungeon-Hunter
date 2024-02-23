using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private HealthHandler healthHandler;


    private void Start()
    {
        slider.minValue = 0;
        slider.maxValue = healthHandler.MaxHealthValue;
        slider.value = healthHandler.CurrentHelth;
    }


    private void OnEnable()
    {
        healthHandler.HealthChanged += SetHealth;
        SetHealth();
    }


    private void OnDisable()
    {
        healthHandler.HealthChanged -= SetHealth;
    }


    private void SetHealth()
    {
        slider.value = healthHandler.CurrentHelth;
    }

}