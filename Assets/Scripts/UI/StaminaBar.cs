using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private StaminaHandler staminaHandler;

    private void Start()
    {
        slider.maxValue = staminaHandler.MaxStamina;
        slider.value = staminaHandler.CurrentStamina;
    }


    private void OnEnable()
    {
        staminaHandler.ChangeStamina += SetSliderValue;
    }


    private void OnDisable()
    {
        staminaHandler.ChangeStamina -= SetSliderValue;
    }


    private void SetSliderValue(float value)
    {
        slider.value = value;
    }
}