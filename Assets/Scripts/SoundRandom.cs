using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandom : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource soundPlayer;

    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();

        if (soundPlayer == null)
        {
            // If no AudioSource is found, create a new one
            soundPlayer = gameObject.AddComponent<AudioSource>();
        }

        PlayRandomSound();
    }

    void PlayRandomSound()
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip randomClip = audioClips[randomIndex];

            soundPlayer.clip = randomClip;
            soundPlayer.Play();
        }
    }
}
