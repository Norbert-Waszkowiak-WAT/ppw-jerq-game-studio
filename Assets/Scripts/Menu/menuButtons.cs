using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    public GameObject playOptions;
    public GameObject settingsOptions;
    public void Quit()
    {
        Application.Quit();
    }

    public void ShowHidePLayOptions()
    { 
        playOptions.active = !playOptions.active;
    }

    public void ShowHideSettingsOptions()
    {
        settingsOptions.active = !settingsOptions.active;
    }

    public void ChangeToMultiplayer()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
