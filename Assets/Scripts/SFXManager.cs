using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip bgm;

    public void PlayBGM()
    {

    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
