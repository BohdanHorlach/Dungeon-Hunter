using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private float speedTransition;


    private void Awake()
    {
        slider.maxValue = healthHandler.MaxHealthValue;
        slider.value = healthHandler.CurrentHelth;
    }


    private void OnEnable()
    {
        healthHandler.DamageReceived += SetHealth;
    }


    private void OnDisable()
    {
        healthHandler.DamageReceived -= SetHealth;
    }


    private void SetHealth()
    {
        StartCoroutine(UpdateSlider(healthHandler.CurrentHelth));
    }


    private IEnumerator UpdateSlider(float newHelth)
    {
        float elapsed = 0.0f;
        float startExp = slider.value;

        while (elapsed < speedTransition)
        {
            float duration = elapsed / speedTransition;
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(startExp, newHelth, duration);
            yield return null;
        }

        slider.value = newHelth;
    }
}