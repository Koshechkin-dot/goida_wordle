using UnityEngine;

public class AudioManager : MonoBehaviour, IService
{
    private AudioSource audioSource;
    private float volume = 0.6f;
    private bool IsMuted = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(audioSource);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, volume);
    }

    public void MuteSwitch()
    {
        IsMuted = !IsMuted;
        audioSource.mute = IsMuted;
    }
}
