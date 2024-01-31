using System;
using System.Collections;
using UnityEngine;


public class StaminaHandler : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics character;
    [SerializeField] private float recoveryRate;
    [SerializeField] private float pauseTime;

    private float stamina;
    private float maxStamina;
    private Coroutine coroutine;


    public event Action<float> ChangeStamina;
    public float MaxStamina { get { return maxStamina; } }
    public float CurrentStamina { get { return stamina; } }


    private void Awake()
    {
        stamina = character.Stamina;
        maxStamina = character.Stamina;
    }


    private void StartRecovery()
    {
        coroutine = StartCoroutine(Recovery());
    }


    private IEnumerator Recovery()
    {
        while(stamina < maxStamina)
        {
            stamina = Mathf.Clamp(stamina + recoveryRate, stamina, maxStamina);
            ChangeStamina?.Invoke(stamina);
            Debug.Log(stamina);
            yield return null;
        }
    }


    public void Action(float force)
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        stamina = Mathf.Clamp(stamina - force, 0, stamina);
        Timer.StartTimer(pauseTime, StartRecovery);
        ChangeStamina?.Invoke(stamina);
    }
}