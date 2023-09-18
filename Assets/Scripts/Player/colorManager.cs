using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorManager : MonoBehaviour
{
    public Color playerColor;

    public GameObject[] frames;

    void Start()
    {
        Renderer frameRenderer;

        foreach (GameObject frame in frames)
        {
            frameRenderer = frame.GetComponent<Renderer>();
            frameRenderer.material.color = playerColor;
        }
    }
}
