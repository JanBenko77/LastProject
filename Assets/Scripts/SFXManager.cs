using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    
    public void PlaySound(AudioClip clip)
    {
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(clip);
    }
}
