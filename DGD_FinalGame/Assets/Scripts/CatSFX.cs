using UnityEngine;

public class CatSFX : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip jumpClip;
    public AudioClip hurtClip;

    void Awake()
    {
        if (sfxSource == null) sfxSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        if (sfxSource != null && jumpClip != null)
            sfxSource.PlayOneShot(jumpClip);
    }

    public void PlayHurt()
    {
        if (sfxSource != null && hurtClip != null)
            sfxSource.PlayOneShot(hurtClip);
    }
}