using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtonsForESCMenu : MonoBehaviour
{ 
    public GameObject mainMenu;
    public GameObject lobbyMenu;

    public GameObject statsMenu;
    public GameObject levelMenu;
    public GameObject optionsMenu;
    public GameObject soundsMenu;
    public GameObject graphicsMenu;

    public TMP_Text playerNick;
    public TMP_InputField playerNickInputField;
    public GameObject changeName;
    public int maxNickLenght = 10;

    public string gitURL = "https://github.com/Norbert-Waszkowiak-WAT/ppw-jerq-game-studio";

    private List<int> menuHistory = new List<int>();

    public KeyCode changeToPreviousKey = KeyCode.Backslash;

    public void Quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(changeToPreviousKey))
        {
            ChangeToPreviousMenu();
        }
    }

    public void OpenUrl()
    {
        Application.OpenURL(gitURL);
    }

    public void ChangeToMainMenu()
    {
        DisableAllMenus(true);
        mainMenu.active = true;
    }

    public void ChangeToLobbyMenu()
    {
        DisableAllMenus(true);
        lobbyMenu.active = true;
    }  

    public void ChangeToStatsMenu()
    {
        DisableAllMenus(true);
        statsMenu.active = true;
    }

    public void ChangeToLevelMenu()
    {
        DisableAllMenus(true);
        levelMenu.active = true;
    }

    public void ChangeToSoundsMenu()
    {
        DisableAllMenus(true);
        soundsMenu.active = true;
    }

    public void ChangeToGraphicsMenu()
    {
        DisableAllMenus(true);
        graphicsMenu.active = true;
    }

    public void ChangeToOptionsMenu()
    {
        DisableAllMenus(true);
        optionsMenu.active = true;
    }

    public void ChangeToPreviousMenu()
    {
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
        if (saveLast)
        {
            if (lobbyMenu.active)
            {
                menuHistory.Add(ChangeMenuToInt(lobbyMenu));
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
        }
        mainMenu.active = false;
        lobbyMenu.active = false;
        statsMenu.active = false;
        levelMenu.active = false;
        optionsMenu.active = false;
        soundsMenu.active = false;
        graphicsMenu.active = false;
    }

    private int ChangeMenuToInt(GameObject menu)
    {
        if (menu == lobbyMenu)
        {
            return 1;
        }
        else if (menu == statsMenu)
        {
            return 2;
        }
        else if (menu == levelMenu)
        {
            return 3;
        }
        else if (menu == optionsMenu)
        {
            return 4;
        }
        else if (menu == soundsMenu)
        {
            return 5;
        }
        else if (menu == graphicsMenu)
        {
            return 6;
        }
        return 0;
    }

    private GameObject ChangeIntToMenu(int intMenu)
    { 
        if (intMenu == 1)
        {
            return lobbyMenu;
        }
        else if (intMenu == 2)
        {
            return statsMenu;
        }
        else if (intMenu == 3)
        {
            return levelMenu;
        }
        else if (intMenu == 4)
        {
            return optionsMenu;
        }
        else if (intMenu == 5)
        {
            return soundsMenu;
        }
        else if (intMenu == 6)
        {
            return graphicsMenu;
        }
        return mainMenu;
    }

    private void Start()
    {
        playerNick.text = System.IO.File.ReadAllText(Application.persistentDataPath + "/data.txt");
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
}
