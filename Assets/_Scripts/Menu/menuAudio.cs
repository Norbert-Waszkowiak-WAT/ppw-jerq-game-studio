using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class menuAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip clickSound;
    public AudioClip rejectionSound;
    public AudioClip approvalSound;

    public AudioMixer audioMixer;

    public void playClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void playRejectionSound()
    {
        audioSource.PlayOneShot(rejectionSound);
    }

    public void playApprovalSound()
    {
        audioSource.PlayOneShot(approvalSound);
    }

    public void SetMasterVolume (float volume)
    {
        audioMixer.SetFloat("masterVolume", volume - 80);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume - 80);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume - 80);
    }
}
