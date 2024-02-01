using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showFps : MonoBehaviour
{
    public TMP_Text fpsText;

    int frameCount = 0;
    void Update()
    {
        frameCount++;
        if (frameCount == 10)
        {
            int currentFps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "fps: " + currentFps.ToString();
            frameCount = 0;
        }
    }
}
