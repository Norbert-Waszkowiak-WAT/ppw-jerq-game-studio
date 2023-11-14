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
    public GameObject lobbyMenu;
    public GameObject loadoutMenu;
    public GameObject statsMenu;
    public GameObject levelMenu;

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
        DisableAllMenus();
        customizeMenu.active = true;
    }

    public void ChangeToMainMenu()
    {
        DisableAllMenus();
        mainMenu.active = true;
    }

    public void ChangeToSingleplayerMenu()
    {
        DisableAllMenus();
        singleplayerMenu.active = true;
    }

    public void ChangeToLobbyMenu()
    {
        DisableAllMenus();
        lobbyMenu.active = true;
    }

    public void ChangeToLoadoutMenu()
    {
        DisableAllMenus();
        loadoutMenu.active = true;
    }    

    public void ChangeToStatsMenu()
    {
        DisableAllMenus();
        statsMenu.active = true;
    }

    public void ChangeToLevelMenu()
    {
        DisableAllMenus();
        levelMenu.active = true;
    }

    public void ChangeToMultiplayerMenu()
    {
        mainMenu.active = false;
        playOptions.active = false;
        customizeMenu.active = false;
        singleplayerMenu.active = false;
        multiplayerMenu.active = true;
        lobbyMenu.active = false;
        loadoutMenu.active = false;
        statsMenu.active = false;
        levelMenu.active = false;
    }

    public void DisableAllMenus()
    {
        mainMenu.active = false;
        playOptions.active = false;
        customizeMenu.active = false;
        singleplayerMenu.active = false;
        multiplayerMenu.active = false;
        lobbyMenu.active = false;
    }

    private void Start()
    {
        playerNick.text = System.IO.File.ReadAllText(Application.persistentDataPath + "/data.txt");
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
        lobbyListHandlerInstance.playerName = playerNickInputField.text;
        Debug.Log(playerNick.text);
        playerNickInputField.text = "";
        changeName.active = false;
        //lobbyHandlerInstance.UpdatePlayerName(playerNick.text);
        SaveToFile(playerNick.text);
    }

    void SaveToFile(string playerNick)
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/data.txt", playerNick);
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
