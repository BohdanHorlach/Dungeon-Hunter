using UnityEngine;


[CreateAssetMenu(fileName = "AudioResources", menuName = "Dungeon Hunter/AudioResources")]

public class AudioResources : ScriptableObject
{
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip takeHitSound;
    [SerializeField] private AudioClip deathSound;


    public AudioClip StepSound { get => stepSound; }
    public AudioClip AttackSound { get => attackSound; }
    public AudioClip TakeHitSound { get => takeHitSound; }
    public AudioClip DeathSound { get => deathSound; }
}