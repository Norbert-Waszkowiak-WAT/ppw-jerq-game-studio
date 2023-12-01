using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapZoom : MonoBehaviour
{
    public KeyCode zoomInKey = KeyCode.M;
    public Camera minimapCamera;

    private Vector3 originalPosition; // Removed the initializer

    private bool zoomedIn = false;

    private void Start()
    {
        originalPosition = transform.position; // Initialize originalPosition here.
    }

    void Update()
    {
        if (Input.GetKeyDown(zoomInKey))
        {
            ToggleZoom();
        }
    }

    //write function toogle zoom
    private void ToggleZoom()
    {
        if (zoomedIn)
        {
            // Zoom out to the original position and size.
            
            transform.localScale = new Vector3(1f, 1f, 1f);
            minimapCamera.orthographicSize -= 50f;
            zoomedIn = false;
            transform.position = originalPosition;
        }
        else
        {
            // Zoom in by moving the camera closer and adjusting the orthographic size.
            Vector3 zoomedPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
            transform.localScale = new Vector3(4f, 4f, 4f);
            transform.position = zoomedPosition;
            minimapCamera.orthographicSize += 50f; // Adjust the size as needed.
            zoomedIn = true;
        }
    }
}
