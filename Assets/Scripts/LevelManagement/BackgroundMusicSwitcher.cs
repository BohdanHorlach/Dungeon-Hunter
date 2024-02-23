using UnityEngine;


public class BackgroundMusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;


    private void Start()
    {
        PlayNextClip();
    }


    private void Update()
    {
        if(audioSource.isPlaying == false)
            PlayNextClip();
    }


    private void PlayNextClip()
    {
        int index = UnityEngine.Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}