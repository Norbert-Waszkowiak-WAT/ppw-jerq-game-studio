using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class savingsettings : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/settings.json"))
        {
            Debug.Log("Loading settings");
            
            string json = File.ReadAllText(Application.persistentDataPath + "/settings.json");
            settings settings = JsonUtility.FromJson<settings>(json);
            masterVolumeSlider.value = settings.masterVolume;
            musicVolumeSlider.value = settings.musicVolume;
            sfxVolumeSlider.value = settings.sfxVolume;
            //resolutionDropdown.value = Array.IndexOf(Screen.resolutions, Screen.currentResolution);
            //Screen.SetResolution(int.Parse(settings.resolution.Split(" x ")[0]), int.Parse(settings.resolution.Split(" x ")[1]), settings.fullscreen);
            //StartCoroutine(visualizeResolution());
            qualityDropdown.value = settings.quality;
            fullscreenToggle.isOn = settings.fullscreen;

            LogWriter.WriteLog("Loading settings: " + "masterVolumeSlider.value = " + masterVolumeSlider.value + "musicVolumeSlider.value = " + musicVolumeSlider.value + "sfxVolumeSlider.value = " + sfxVolumeSlider.value + "resolutionDropdown.value = " + resolutionDropdown.value + "qualityDropdown.value = " + qualityDropdown.value + "fullscreenToggle.isOn = " + fullscreenToggle.isOn);
        }
    }

    /*IEnumerator visualizeResolution()
    {
        yield return null;
        int index = Array.IndexOf(Screen.resolutions, Screen.currentResolution);
        if (index != -1)
        {
            resolutionDropdown.value = index;
        }
    }*/

    public void SaveSettings(string nameOfGO)
    {
        Debug.Log("Saving settings");
        Debug.Log(nameOfGO);
        settings settings = new settings();
        settings.masterVolume = masterVolumeSlider.value;
        settings.musicVolume = musicVolumeSlider.value;
        settings.sfxVolume = sfxVolumeSlider.value;
        //settings.resolution = Screen.resolutions[resolutionDropdown.value].width.ToString() + " x " + Screen.resolutions[resolutionDropdown.value].height.ToString();
        settings.quality = qualityDropdown.value;
        settings.fullscreen = fullscreenToggle.isOn;
        LogWriter.WriteLog("Saving settings by: " + nameOfGO + ": " + "masterVolumeSlider.value = " + masterVolumeSlider.value + "musicVolumeSlider.value = " + musicVolumeSlider.value + "sfxVolumeSlider.value = " + sfxVolumeSlider.value + "resolutionDropdown.value = " + resolutionDropdown.value + "qualityDropdown.value = " + qualityDropdown.value + "fullscreenToggle.isOn = " + fullscreenToggle.isOn);
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }
}
public class settings
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public string resolution;
    public int quality;
    public bool fullscreen;
}