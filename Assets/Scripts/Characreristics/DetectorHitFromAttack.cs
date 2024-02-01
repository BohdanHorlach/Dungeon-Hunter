using System;
using UnityEngine;


public class DetectorHitFromAttack : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics character;
    [SerializeField] private string friendTag;
    [SerializeField] private bool isCountedExp = true;


    public event Action OnKickback;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == friendTag)
            return;

        if (collision.tag == "Shield")
        {
            OnKickback?.Invoke();
            return;
        }

        HealthHandler healthHandler;

        if (collision.TryGetComponent<HealthHandler>(out healthHandler) != true)
            return;


        healthHandler.Death += Kill;
        Attack(healthHandler);
        healthHandler.Death -= Kill;
    }


    private void Attack(HealthHandler healthHandler)
    {
        healthHandler.TakeDamage(character.Damage);
    }


    private void Kill(int exp)
    {
        if(isCountedExp == true)
            character.Exp += exp;
    }
}