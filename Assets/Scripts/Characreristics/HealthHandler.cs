using System;
using UnityEngine;


public class HealthHandler : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics characteristics;

    private float maxHealthValue;
    private float helth;


    public event Action DamageReceived;
    public event Action<int> Death;
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

        DamageReceived?.Invoke();

        if (helth == 0)
            Death?.Invoke(characteristics.Exp);
    }
}