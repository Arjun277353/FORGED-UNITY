using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEvent : MonoBehaviour
{
    [SerializeField] private AudioClip soundClip; 
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false; 
    }

    public void PlaySound()
    {
        if (soundClip == null)
        {
            Debug.LogWarning("Sound Clip is not assigned!");
            return;
        }

        audioSource.Stop();
        audioSource.PlayOneShot(soundClip);
    }
}
