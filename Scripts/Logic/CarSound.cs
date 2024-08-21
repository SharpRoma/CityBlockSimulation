using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static CarMovement;

public class CarSound : MonoBehaviour
{
    
    public AudioClip movelessSound;
    public AudioClip movingSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (CarMovement.roundedSpeed != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = movingSound;
                audioSource.Play();
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = movelessSound;
                audioSource.Play();
            }
        }
    }
}
