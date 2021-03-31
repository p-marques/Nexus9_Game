using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
