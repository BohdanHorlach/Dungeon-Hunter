using UnityEngine;


public class EnemyAnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private HealthHandler helthHandler;

    private void OnEnable()
    {
        helthHandler.DamageReceived += TakeDamage;
        helthHandler.Death += Death;
    }


    private void OnDisable()
    {
        helthHandler.DamageReceived -= TakeDamage;
        helthHandler.Death -= Death;
    }


    private void TakeDamage()
    {
        SetTrigger("TakeHit");
    }


    private void Death()
    {
        SetTrigger("Death");
    }


    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}