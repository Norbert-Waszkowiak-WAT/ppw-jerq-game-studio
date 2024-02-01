using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class optionsHandler : MonoBehaviour
{
    private Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        int index = 0;

        foreach (Resolution resolution in resolutions)
        {
            options.Add(resolution.width + " x " + resolution.height);
            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = index;
            }
            index++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetQuality(int qualityIndex)
    {
        LogWriter.WriteLog("Quality set to " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        LogWriter.WriteLog("Fullscreen set to " + isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        
        Resolution resolution = resolutions[resolutionIndex];
        LogWriter.WriteLog("Resolution set to " + resolution.width + " x " + resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
