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
        LogWriter.WriteLog("Click sound played");
        audioSource.PlayOneShot(clickSound);
    }

    public void playRejectionSound()
    {
        LogWriter.WriteLog("Rejection sound played");
        audioSource.PlayOneShot(rejectionSound);
    }

    public void playApprovalSound()
    {
        LogWriter.WriteLog("Approval sound played");
        audioSource.PlayOneShot(approvalSound);
    }

    public void SetMasterVolume (float volume)
    {
        LogWriter.WriteLog("Master volume set to " + volume);
        audioMixer.SetFloat("masterVolume", volume - 80);
    }

    public void SetMusicVolume(float volume)
    {
        LogWriter.WriteLog("Music volume set to " + volume);
        audioMixer.SetFloat("musicVolume", volume - 80);
    }

    public void SetEffectsVolume(float volume)
    {
        LogWriter.WriteLog("Effects volume set to " + volume);
        audioMixer.SetFloat("effectsVolume", volume - 80);
    }
}
