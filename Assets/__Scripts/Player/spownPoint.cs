using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spownPoint : MonoBehaviour
{
    public Vector3 spownPosition;

    public float heightTreshold = 0.65f;

    public AudioSource audioSource;
    public AudioClip lavaDeathSound;

    private float timeSinceLastDeath = 0;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        timeSinceLastDeath += Time.deltaTime;
        if (rb.worldCenterOfMass.y < heightTreshold)
        {
            LogWriter.WriteLog("Player died");
            transform.position = spownPosition;
            if (timeSinceLastDeath > 1)
            {
                audioSource.PlayOneShot(lavaDeathSound);
                LogWriter.WriteLog("Player death sound");
                timeSinceLastDeath = 0;
            }
        }
    }
}
