using System;
using UnityEngine;


public class HealthHandler : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics characteristics;

    private float maxHealthValue;
    private float helth;


    public event Action HealthChanged;
    public event Action Death;
    public float CurrentHelth { get { return helth; } }
    public float MaxHealthValue { get { return maxHealthValue; } }


    private void Awake()
    {
        helth = characteristics.Health;
        maxHealthValue = characteristics.Health;
    }


    public void TakeDamage(float damage)
    {
        helth = Mathf.Clamp(helth - damage, 0, maxHealthValue);

        HealthChanged?.Invoke();

        if (helth == 0)
            Death?.Invoke();
    }


    public void Revive()
    {
        helth = maxHealthValue;
        HealthChanged?.Invoke();
    }
}