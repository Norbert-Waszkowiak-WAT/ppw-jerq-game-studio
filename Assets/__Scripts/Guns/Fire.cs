using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject audioSource;
    public bool fired = false;

    void Update()
    {
        if (fired)
        {
            PlaySound();
            fired = ! fired;
        }
    }

    void PlaySound()
    {
        AudioSource audio = Instantiate(audioSource, audioSource.transform.position, audioSource.transform.rotation).GetComponent<AudioSource>();
        if (!audio.isPlaying)
        {
            audio.Play();
        }
        Destroy(audio.gameObject, audio.clip.length);
    }
}
