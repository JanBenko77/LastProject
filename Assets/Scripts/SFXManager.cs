using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip bgm;

    private void Start()
    {
        PlayBGM();
    }
    public void PlayBGM()
    {
        audioSource.volume = 0.25f;
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(clip);
    }
}
