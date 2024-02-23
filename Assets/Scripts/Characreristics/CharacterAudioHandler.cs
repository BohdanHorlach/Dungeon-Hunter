using UnityEngine;


public class CharacterAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioResources audioResources;
    [SerializeField] private HealthHandler healthHandler;


    private void OnEnable()
    {
        healthHandler.HealthChanged += PlayTakeHit;
        healthHandler.Death += PlayDeath;
    }


    private void OnDisable()
    {
        healthHandler.HealthChanged -= PlayTakeHit;
        healthHandler.Death -= PlayDeath;
    }


    private void PlayTakeHit()
    {
        audioSource.PlayOneShot(audioResources.TakeHitSound);
    }


    private void PlayDeath()
    {
        audioSource.PlayOneShot(audioResources.DeathSound);
    }


    private void PlayAttack()
    {
        audioSource.PlayOneShot(audioResources.AttackSound);
    }


    private void PlayStep()
    {
        audioSource.PlayOneShot(audioResources.StepSound);
    }
}