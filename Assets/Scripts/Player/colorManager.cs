using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorManager : MonoBehaviour
{
    public Color playerColor;

    public GameObject[] frames;

    void Awake()
    {
        foreach (GameObject frame in frames)
        {
            if (frame.tag == "Trail")
            {
                TrailRenderer frameRenderer = frame.GetComponent<TrailRenderer>();
                if (frameRenderer != null)
                {
                    frameRenderer.material.color = playerColor;
                }
            } else
            {
                Renderer frameRenderer;
                frameRenderer = frame.GetComponent<Renderer>();
                if (frameRenderer != null )
                {
                    frameRenderer.material.color = playerColor;
                } 
            }  
        }
    }
}
