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
    public GameObject consoleMenu;

    public consoleHandler consoleHandler;


    public GameObject loadoutMenu;

    public GameObject weaponsLoadoutMenu;
    public GameObject abilitiesLoadoutMenu;


    public GameObject statsMenu;
    public GameObject levelMenu;
    public GameObject optionsMenu;
    public GameObject soundsMenu;
    public GameObject graphicsMenu;

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

    private List<int> menuHistory = new List<int>();

    public KeyCode changeToPreviousKey = KeyCode.Escape;

    public void Quit()
    {
        LogWriter.WriteLog("Quit");
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(changeToPreviousKey))
        {
            ChangeToPreviousMenu();
        }
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
        LogWriter.WriteLog("StartSingleplayer");
        SceneManager.LoadScene("Singleplayer Scene");
    }

    public void StartMultiplayer()
    {
        LogWriter.WriteLog("StartMultiplayer");
        SceneManager.LoadScene("Multiplayer Scene");
    }

    public void OpenUrl()
    {
        LogWriter.WriteLog("OpenUrl");
        Application.OpenURL(gitURL);
    }

    public void ChangeToCustomize()
    {
        LogWriter.WriteLog("ChangeToCustomize");
        DisableAllMenus(true);
        customizeMenu.active = true;
    }

    public void ChangeToMainMenu()
    {
        LogWriter.WriteLog("ChangeToMainMenu");
        DisableAllMenus(true);
        mainMenu.active = true;
    }

    public void ChangeToSingleplayerMenu()
    {
        LogWriter.WriteLog("ChangeToSingleplayerMenu");
        DisableAllMenus(true);
        singleplayerMenu.active = true;
    }

    public void ChangeToLobbyMenu()
    {
        LogWriter.WriteLog("ChangeToLobbyMenu");
        DisableAllMenus(true);
        lobbyMenu.active = true;
    }

    public void ChangeToLoadoutMenu()
    {
        LogWriter.WriteLog("ChangeToLoadoutMenu");
        DisableAllMenus(true);
        loadoutMenu.active = true;
    }    

    public void ChangeToStatsMenu()
    {
        LogWriter.WriteLog("ChangeToStatsMenu");
        DisableAllMenus(true);
        statsMenu.active = true;
    }

    public void ChangeToLevelMenu()
    {
        LogWriter.WriteLog("ChangeToLevelMenu");
        DisableAllMenus(true);
        levelMenu.active = true;
    }

    public void ChangeToSoundsMenu()
    {
        LogWriter.WriteLog("ChangeToSoundsMenu");
        DisableAllMenus(true);
        soundsMenu.active = true;
    }

    public void ChangeToGraphicsMenu()
    {
        LogWriter.WriteLog("ChangeToGraphicsMenu");
        DisableAllMenus(true);
        graphicsMenu.active = true;
    }

    public void ChangeToWeaponsLoadoutMenu()
    {
        LogWriter.WriteLog("ChangeToWeaponsLoadoutMenu");
        DisableAllMenus(true);
        weaponsLoadoutMenu.active = true;
    }

    public void ChangeToAbilitiesLoadoutMenu()
    {
        LogWriter.WriteLog("ChangeToAbilitiesLoadoutMenu");
        DisableAllMenus(true);
        abilitiesLoadoutMenu.active = true;
    }

    public void ChangeToConsoleMenu()
    {
        LogWriter.WriteLog("ChangeToConsoleMenu");
        DisableAllMenus(true);
        consoleMenu.active = true;
        consoleHandler.ReloadText();
    }

    public void ChangeToMultiplayerMenu()
    {
        LogWriter.WriteLog("ChangeToMultiplayerMenu");
        if (lobbyListHandler.joinedLobby != null)
        {
            ChangeToLobbyMenu();
            return;
        }
        DisableAllMenus(true);
        multiplayerMenu.active = true;
    }

    public void ChangeToOptionsMenu()
    {
        LogWriter.WriteLog("ChangeToOptionsMenu");
        DisableAllMenus(true);
        optionsMenu.active = true;
    }

    public void ChangeToPreviousMenu()
    {
        LogWriter.WriteLog("ChangeToPreviousMenu");
        if (menuHistory.Count == 0)
        {
            ChangeToMainMenu();
            return;
        }
        DisableAllMenus(false);
        ChangeIntToMenu(menuHistory[menuHistory.Count - 1]).active = true;
        menuHistory.RemoveAt(menuHistory.Count - 1);
    }

    public void DisableAllMenus(bool saveLast)
    {
        LogWriter.WriteLog("DisableAllMenus");
        if (saveLast)
        {
            if (playOptions.active)
            {
                menuHistory.Add(ChangeMenuToInt(playOptions));
            }
            else if (customizeMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(customizeMenu));
            }
            else if (singleplayerMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(singleplayerMenu));
            }
            else if (multiplayerMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(multiplayerMenu));
            }
            else if (lobbyMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(lobbyMenu));
            }
            else if (loadoutMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(loadoutMenu));
            }
            else if (statsMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(statsMenu));
            }
            else if (levelMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(levelMenu));
            }
            else if (optionsMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(optionsMenu));
            }
            else if (mainMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(mainMenu));
            }
            else if (soundsMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(soundsMenu));
            }
            else if (graphicsMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(graphicsMenu));
            }
            else if (weaponsLoadoutMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(weaponsLoadoutMenu));
            }
            else if (abilitiesLoadoutMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(abilitiesLoadoutMenu));
            }
            else if (consoleMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(consoleMenu));
            }
        }
        mainMenu.active = false;
        playOptions.active = false;
        customizeMenu.active = false;
        singleplayerMenu.active = false;
        multiplayerMenu.active = false;
        lobbyMenu.active = false;
        loadoutMenu.active = false;
        statsMenu.active = false;
        levelMenu.active = false;
        optionsMenu.active = false;
        soundsMenu.active = false;
        graphicsMenu.active = false;
        weaponsLoadoutMenu.active = false;
        abilitiesLoadoutMenu.active = false;
        consoleMenu.active = false;
    }

    private int ChangeMenuToInt(GameObject menu)
    {
        if (menu == playOptions)
        {
            return 1;
        }
        else if (menu == customizeMenu)
        {
            return 2;
        }
        else if (menu == singleplayerMenu)
        {
            return 3;
        }
        else if (menu == multiplayerMenu)
        {
            return 4;
        }
        else if (menu == lobbyMenu)
        {
            return 5;
        }
        else if (menu == loadoutMenu)
        {
            return 6;
        }
        else if (menu == statsMenu)
        {
            return 7;
        }
        else if (menu == levelMenu)
        {
            return 8;
        }
        else if (menu == optionsMenu)
        {
            return 9;
        }
        else if (menu == soundsMenu)
        {
            return 10;
        }
        else if (menu == graphicsMenu)
        {
            return 11;
        }
        else if (menu == weaponsLoadoutMenu)
        {
            return 12;
        }
        else if (menu == abilitiesLoadoutMenu)
        {
            return 13;
        }
        else if (menu == consoleMenu)
        {
            return 14;
        }
        return 0;
    }

    private GameObject ChangeIntToMenu(int intMenu)
    { 
        if (intMenu == 1)
        {
            return playOptions;
        }
        else if (intMenu == 2)
        {
            return customizeMenu;
        }
        else if (intMenu == 3)
        {
            return singleplayerMenu;
        }
        else if (intMenu == 4)
        {
            return multiplayerMenu;
        }
        else if (intMenu == 5)
        {
            return lobbyMenu;
        }
        else if (intMenu == 6)
        {
            return loadoutMenu;
        }
        else if (intMenu == 7)
        {
            return statsMenu;
        }
        else if (intMenu == 8)
        {
            return levelMenu;
        }
        else if (intMenu == 9)
        {
            return optionsMenu;
        }
        else if (intMenu == 10)
        {
            return soundsMenu;
        }
        else if (intMenu == 11)
        {
            return graphicsMenu;
        }
        else if (intMenu == 12)
        {
            return weaponsLoadoutMenu;
        }
        else if (intMenu == 13)
        {
            return abilitiesLoadoutMenu;
        }
        else if (intMenu == 14)
        {
            return consoleMenu;
        }   
        return mainMenu;
    }

    private void Start()
    {
        playerNick.text = System.IO.File.ReadAllText(Application.persistentDataPath + "/data.txt");
        lobbyListHandlerInstance.playerName = playerNick.text;
    }

    public void CreateNewMultiplayerGame()
    {
        LogWriter.WriteLog("CreateNewMultiplayerGame");
        dataTransferObject.GetComponent<dataTransfer>().createNewGame = true;
        ChangeToMultiplayer();
    }

    public void JoinMultiplayerGame()
    {
        LogWriter.WriteLog("JoinMultiplayerGame");
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
        LogWriter.WriteLog("SavePlayerNick: " + playerNick.text);
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
        LogWriter.WriteLog("SaveLobbyNameInCreation: " + lobbyNameInCreation.text);
        lobbyNameInCreationInputField.text = "";
        changeLobbyNameInCreation.active = false;
    }

    public void ChangePublicityCreateLobby()
    {
        if (lobbyPulicityInCreation.text == "Public")
        {
            LogWriter.WriteLog("ChangePublicityCreateLobby: Private");
            lobbyPulicityInCreation.text = "Private";
        }
        else
        {
            LogWriter.WriteLog("ChangePublicityCreateLobby: Public");
            lobbyPulicityInCreation.text = "Public";
        }
    }
}
