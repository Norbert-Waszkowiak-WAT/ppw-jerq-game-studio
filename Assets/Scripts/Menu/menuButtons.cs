using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    public GameObject playOptions;
    public GameObject settingsOptions;
    public GameObject customizeMenu;
    public GameObject mainMenu;
    public GameObject singleplayerMenu;
    public GameObject multiplayerMenu;

    public GameObject dataTransferObject;
    public addMaterialsToPlayerInMenu addMaterialsToPlayerInMenu;

    public lobbyHandler lobbyHandlerInstance;

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
        List<Material> materials = dataTransferObject.GetComponent<dataTransfer>().materials;
        materials = new List<Material>();
        materials.Add(addMaterialsToPlayerInMenu.mat1);
        materials.Add(addMaterialsToPlayerInMenu.mat2);
        materials.Add(addMaterialsToPlayerInMenu.mat3);
        dataTransferObject.GetComponent<dataTransfer>().materials = materials;
        DontDestroyOnLoad(dataTransferObject);
        //SceneManager.LoadScene("Multiplayer Scene");
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
        multiplayerMenu.active = false;
    }

    public void ChangeToMainMenu()
    {
        mainMenu.active = true;
        customizeMenu.active = false;
        singleplayerMenu.active = false;
        multiplayerMenu.active = false;
    }

    public void ChangeToSingleplayerMenu()
    {
        mainMenu.active = false;
        playOptions.active = false;
        customizeMenu.active = false;
        singleplayerMenu.active = true;
        multiplayerMenu.active = false;
    }

    public void ChangeToMultiplayerMenu()
    {
        mainMenu.active = false;
        playOptions.active = false;
        customizeMenu.active = false;
        singleplayerMenu.active = false;
        multiplayerMenu.active = true;
        lobbyHandlerInstance.StartUnityServices();
    }

    private void Start()
    {
        lobbyHandlerInstance.StartUnityServices();
    
    }

    public void CreateNewMultiplayerGame()
    {
        dataTransferObject.GetComponent<dataTransfer>().createNewGame = true;
        ChangeToMultiplayer();
    }

    public void JoinMultiplayerGame()
    {
        dataTransferObject.GetComponent<dataTransfer>().createNewGame = false;
        ChangeToMultiplayer();
    }
}
