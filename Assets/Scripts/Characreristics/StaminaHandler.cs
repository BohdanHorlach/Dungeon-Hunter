using System;
using System.Collections;
using UnityEngine;


public class StaminaHandler : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics character;
    [SerializeField] private float recoveryRate;
    [SerializeField] private float pauseTime;

    private const float SECONDS_WAIT_TO_COROUTINE = 0.1f;
    private float stamina;
    private float maxStamina;


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
        StartCoroutine("Recovery");
    }


    private IEnumerator Recovery()
    {
        while(stamina < maxStamina)
        {
            stamina = Mathf.Clamp(stamina + recoveryRate, stamina, maxStamina);
            ChangeStamina?.Invoke(stamina);
            yield return new WaitForSeconds(SECONDS_WAIT_TO_COROUTINE);
        }
    }


    public void Action(float force)
    {
        StopCoroutine("Recovery");

        stamina = Mathf.Clamp(stamina - force, 0, stamina);
        Timer.StartTimer(pauseTime, StartRecovery);
        ChangeStamina?.Invoke(stamina);
    }
}