using System;
using UnityEngine;


public class DetectorHitFromAttack : MonoBehaviour
{
    [SerializeField] private CharacterCharacteristics character;
    [SerializeField] private ExpHandler expHandler;
    [SerializeField] private string friendTag;
    [SerializeField] private bool isCountedExp = true;

    private Collider2D target;

    public event Action OnKickback;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == friendTag)
            return;

        target = collision;

        Hit();
    }


    private void Hit()
    {
        HealthHandler healthHandler;
        if (target.TryGetComponent<HealthHandler>(out healthHandler) != true)
            return;

        if (target.tag == "Shield")
        {
            OnKickback?.Invoke();
            return;
        }

        healthHandler.Death += Kill;
        Attack(healthHandler);
        healthHandler.Death -= Kill;
    }


    private void Attack(HealthHandler healthHandler)
    {
        healthHandler.TakeDamage(character.Damage);
    }


    private void Kill()
    {
        ExpHandler targetExpHandler;
        if(target.TryGetComponent<ExpHandler>(out targetExpHandler) == true && isCountedExp == true)
            expHandler.AddExp(targetExpHandler.Exp);
    }
}