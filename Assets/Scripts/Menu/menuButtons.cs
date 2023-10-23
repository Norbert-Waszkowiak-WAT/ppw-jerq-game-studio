using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    public GameObject playOptions;
    public GameObject settingsOptions;
    public GameObject customizeMenu;
    public GameObject mainMenu;
    public GameObject singleplayerMenu;

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

    public void StartSingleplayer()
    {
        SceneManager.LoadScene("Singleplayer Scene");
    }

    public void OpenUrl()
    {
        Application.OpenURL(gitURL);
    }

    public void ChangeToCustomize()
    {
        mainMenu.active = false;
        playOptions.active = false;
        customizeMenu.active = true;
        singleplayerMenu.active = false;
    }

    public void ChangeToMainMenu()
    {
        mainMenu.active = true;
        customizeMenu.active = false;
        singleplayerMenu.active = false;
    }

    public void ChangeToSingleplayerMenu()
    {
        mainMenu.active = false;
        playOptions.active = false;
        customizeMenu.active = false;
        singleplayerMenu.active = true;
    }
}
