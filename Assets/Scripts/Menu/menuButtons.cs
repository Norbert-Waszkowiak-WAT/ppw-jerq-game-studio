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

    public TMP_Text playerNick;
    public TMP_InputField playerNickInputField;
    public GameObject changeName;
    public int maxNickLenght = 10;
    public lobbyListHandler lobbyListHandlerInstance;

    public GameObject changeLobbyNameInCreation;
    public TMP_Text lobbyNameInCreation;
    public TMP_InputField lobbyNameInCreationInputField;
    public int maxLobbyNameLenght = 10;

    public TMP_Text lobbyPulicityInCreation;

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
        LoadMaterialsToTransferObject();
        DontDestroyOnLoad(dataTransferObject);
        //SceneManager.LoadScene("Multiplayer Scene");
    }

    public void LoadMaterialsToTransferObject()
    {
        List<Material> materials = dataTransferObject.GetComponent<dataTransfer>().materials;
        materials = new List<Material>();
        materials.Add(addMaterialsToPlayerInMenu.mat1);
        materials.Add(addMaterialsToPlayerInMenu.mat2);
        materials.Add(addMaterialsToPlayerInMenu.mat3);
        dataTransferObject.GetComponent<dataTransfer>().materials = materials;
    }

    public void StartSingleplayer()
    {
        SceneManager.LoadScene("Singleplayer Scene");
    }

    public void StartMultiplayer()
    {
        SceneManager.LoadScene("Multiplayer Scene");
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
    }

    private void Start()
    {
        playerNick.text = "(ur not) KIWI" + Random.Range(0, 1000);
        lobbyListHandlerInstance.playerName = playerNick.text;
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

    public void ChangePlayerNick()
    {
        changeName.active = true;
    }

    public void SavePlayerNick()
    {
        if (playerNickInputField.text == "" || playerNickInputField.text.Length > maxNickLenght)
        {
            CancelPlayerNick();
            return;
        }
        playerNick.text = playerNickInputField.text;
        playerNickInputField.text = "";
        changeName.active = false;
        lobbyListHandlerInstance.playerName = playerNick.text;
        //lobbyHandlerInstance.UpdatePlayerName(playerNick.text);
    }

    public void CancelPlayerNick()
    {
        playerNickInputField.text = "";
        changeName.active = false;
    }

    public void ChangeLobbyNameInCreation()
    {
        changeLobbyNameInCreation.active = true;
    }

    public void CancelChangeLobbyNameInCreation()
    {
        changeLobbyNameInCreation.active = false;
    }

    public void SaveLobbyNameInCreation()
    {   
        if (lobbyNameInCreationInputField.text == "" || lobbyNameInCreationInputField.text.Length > maxLobbyNameLenght)
        {
            CancelChangeLobbyNameInCreation();
            return;
        }
        lobbyNameInCreation.text = lobbyNameInCreationInputField.text;
        lobbyNameInCreationInputField.text = "";
        changeLobbyNameInCreation.active = false;
    }

    public void ChangePublicityCreateLobby()
    {
        if (lobbyPulicityInCreation.text == "Public")
        {
            lobbyPulicityInCreation.text = "Private";
        }
        else
        {
            lobbyPulicityInCreation.text = "Public";
        }
    }
}
