using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    public GameObject playOptions;
    public GameObject settingsOptions;
    public GameObject customizeMenu;
    public GameObject mainMenu;

    public string gitURL = "https://github.com/Norbert-Waszkowiak-WAT/ppw-jerq-game-studio";
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

    public void OpenUrl()
    {
        Application.OpenURL(gitURL);
    }

    public void ChangeToCustomize()
    {
        mainMenu.active = false;
        customizeMenu.active = true;
    }

    public void ChangeToMainMenuFromCustomize()
    {
        mainMenu.active = true;
        customizeMenu.active = false;
    }
}
