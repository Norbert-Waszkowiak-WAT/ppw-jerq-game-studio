using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;
using UnityEngine;
using Unity.Services.Authentication;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System;
using Unity.Services.Core;

public class lobbyListHandler : MonoBehaviour
{
    public GameObject lobbyList;
    public GameObject errorList;
    public GameObject lobbyPrefab;
    public List<GameObject> lobbys;

    public float minRefreshTime = 2f;
    private float timeFromLastRefresh;
    private object m_LobbyEvents;
    private Lobby hostLobby;
    private Lobby joinedLobby;

    public string playerName;

    public TMP_Text lobbyNameText;
    public TMP_Text lobbyPublicity;

    public GameObject lobbyCreationMenu;

    public GameObject lobbySelector;
    public GameObject currentLobby;

    private float lobbyHeartBeatTimer = 15f;
    private float lobbyHeartBeatTimerMax = 15f;

    private float lobbyUpdateTimer = 5.1f;

    public GameObject playerList;
    public GameObject playerPrefab;

    public GameObject errorTemplate;

    private List<GameObject> players;
    private List<GameObject> errors;

    public TMP_Text lobbyName;
    public TMP_Text lobbyPlayers;
    public TMP_Text lobbyGameMode;

    public testRelayForLobby testRelayInstance;

    public GameObject dataTransfer;

    public menuButtons menuButtonsInstance;

    private async void UnityServicesStart()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            } catch(Exception e)
            {
                DisplayError(e);
            }
        }
        else
        {
            Debug.Log("Already signed in " + AuthenticationService.Instance.PlayerId);
        }
    }

    private void Update()
    {
        
        timeFromLastRefresh += Time.deltaTime;

        HandleLobbyPullForUpdates();
        HandleLobbyHeartBeat();
    }

    void ShowCurrentLobby()
    {
        currentLobby.SetActive(true);
        lobbySelector.SetActive(false);
        UpdateDisplayedLobby();
    }

    void HideCurrentLobby()
    {
        currentLobby.SetActive(false);
        lobbySelector.SetActive(true);
    }

    private async void HandleLobbyPullForUpdates()
    {
        if (joinedLobby == null)
        {
            return;
        }
        lobbyUpdateTimer -= Time.deltaTime;
        if (lobbyUpdateTimer < 0f)
        {
            lobbyUpdateTimer = 3.1f;
            Lobby lobby = await Lobbies.Instance.GetLobbyAsync(joinedLobby.Id);
            Debug.Log("Lobby updated: " + lobby.Id + " " + lobby.Name + " " + lobby.LobbyCode);
            if (lobby != null)
            {
                joinedLobby = lobby;
                UpdateDisplayedLobby();
            }
            //Debug.LogError(joinedLobby.Data["JoinCode"].Value);
            if (joinedLobby.Data["JoinCode"].Value != "0")
            {
                Debug.Log("Join code: " + joinedLobby.Data["JoinCode"].Value);
                //testRelayInstance.JoinRelay(joinedLobby.Data["JoinCode"].Value);
                dataTransfer.GetComponent<dataTransfer>().relayCode = joinedLobby.Data["JoinCode"].Value;
                menuButtonsInstance.LoadMaterialsToTransferObject();
                DontDestroyOnLoad(dataTransfer);
                menuButtonsInstance.StartMultiplayer();
                HideCurrentLobby();
            }
        }
    }

    /*void DisplayInternetError()
    {
        GameObject newInternetError = Instantiate(errorTemplate, playerList.transform);
        newInternetError.active = true;
        newInternetError.transform.SetParent(playerList.transform);
        newInternetError.transform.Find("ErrorName").GetComponent<TMPro.TMP_Text>().text = "Authentication Error";
        newInternetError.transform.Find("ErrorDescription").GetComponent<TMPro.TMP_Text>().text = authenticationError;
        players.Add(newInternetError);
    }

    void AddLobbyError(string e)
    {
        Debug.LogError("start");
        GameObject newLobbyErr = Instantiate(errorTemplate, playerList.transform);
        GameObject newLobbyError = Instantiate(errorTemplate, playerList.transform);
        newLobbyError.active = true;
        newLobbyError.transform.SetParent(playerList.transform);
        newLobbyError.transform.Find("ErrorName").GetComponent<TMPro.TMP_Text>().text = "Lobby Error";
        //newLobbyError.transform.Find("ErrorDescription").GetComponent<TMPro.TMP_Text>().text = e;
        players.Add(newLobbyError);

        Debug.LogError("end");
    } */

    void DisplayError(System.Exception e)
    {
        GameObject newError = Instantiate(errorTemplate, playerList.transform);
        newError.active = true;
        newError.transform.SetParent(errorList.transform);
        newError.transform.Find("ErrorName").GetComponent<TMPro.TMP_Text>().text = "TMP ERROR NAME:";
        newError.transform.Find("ErrorDescription").GetComponent<TMPro.TMP_Text>().text = e.Message;
        errors.Add(newError);
    }

    void UpdateDisplayedLobby()
    {
        Debug.Log(joinedLobby.Players.Count);
        foreach(GameObject player in players)
        {
            Destroy(player);
        }
        if (joinedLobby == null)
        {
            return;
        }
        foreach(Player player in joinedLobby.Players)
        {
            AddPlayer(player);
        }

        lobbyName.text = joinedLobby.Name;
        lobbyPlayers.text = joinedLobby.Players.Count.ToString() + "/" + joinedLobby.MaxPlayers.ToString();
        lobbyGameMode.text = joinedLobby.Data["GameMode"].Value;
    }

    void AddPlayer(Player player)
    {
        GameObject newPlayer = Instantiate(playerPrefab, playerList.transform);
        newPlayer.active = true;
        newPlayer.transform.SetParent(playerList.transform);
        newPlayer.transform.Find("PlayerNick").GetComponent<TMPro.TMP_Text>().text = player.Data["PlayerName"].Value;
        newPlayer.GetComponent<playerData>().playerId = player.Id;
        newPlayer.GetComponent<playerData>().playerName = player.Data["PlayerName"].Value;
        //get child named "Kick" and set it active if player is host
        if (player.Id != joinedLobby.HostId || player.Id == AuthenticationService.Instance.PlayerId)
        {
            newPlayer.transform.Find("Kick").gameObject.active = false;
            //Debug.LogError(player.Id);
        }
        players.Add(newPlayer);

        Debug.Log(AuthenticationService.Instance.PlayerId);
        Debug.Log(player.Id);
    }

    public void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Players in lobby " + lobby.Name + " " + lobby.Data["GameMode"].Value);
        foreach (Player player in lobby.Players)
        {
            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
        }
    }

    private async void HandleLobbyHeartBeat()
    {
        if (hostLobby != null)
        {
            lobbyHeartBeatTimer -= Time.deltaTime;
            if (lobbyHeartBeatTimer < 0f)
            {
                lobbyHeartBeatTimer = lobbyHeartBeatTimerMax;
                LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }
    public void Awake()
    {
        timeFromLastRefresh = minRefreshTime;
        lobbys = new List<GameObject>();
        players = new List<GameObject>();
        UnityServicesStart();
    }
    public async void RefreshLobbies()
    {
        if (timeFromLastRefresh < minRefreshTime)
        {
            return;
        }

        foreach (GameObject lobby in lobbys)
        {
            Destroy(lobby);
        }
        QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
        {
            Count = 25,
            /*Filters = new List<QueryFilter>
            {
                    new QueryFilter(QueryFilter.FieldOptions.S1, "Deathmach", QueryFilter.OpOptions.EQ)
            },*/
            Order = new List<QueryOrder>
                {
                    new QueryOrder(true, QueryOrder.FieldOptions.AvailableSlots)
                }
        };
        QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);

        Debug.Log(queryResponse.Results.Count);
        foreach (Lobby lobby in queryResponse.Results)
        {
            GameObject lobbyObject = Instantiate(lobbyPrefab, lobbyList.transform);
            lobbyObject.active = true;
            lobbyObject.transform.SetParent(lobbyList.transform);
            lobbyObject.transform.Find("Gamemode").GetComponent<TMPro.TMP_Text>().text = lobby.Data["GameMode"].Value;
            int maxPlayers = lobby.MaxPlayers;
            int availableSlots = lobby.AvailableSlots;
            int currentPlayers = maxPlayers - availableSlots;
            lobbyObject.transform.Find("Players").GetComponent<TMPro.TMP_Text>().text = currentPlayers.ToString() + "/" + maxPlayers.ToString();
            lobbyObject.transform.Find("LobbyName").GetComponent<TMPro.TMP_Text>().text = lobby.Name;
            lobbyObject.GetComponent<lobbyData>().lobbyId = lobby.Id;
            lobbyObject.GetComponent<lobbyData>().lobbyName = lobby.Name;
            lobbys.Add(lobbyObject);
            Debug.Log(lobby.Id + " " + lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Data["GameMode"].Value);
        }
        timeFromLastRefresh = 0f;
        
    }

    void HideLobbyCreation()
    {
        lobbyCreationMenu.active = false;
    }

    public async void CreateLobby()
    {
        bool isPrivate = false;
        if (lobbyPublicity.text == "Private")
        {
            isPrivate = true;
        }

        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, "Deathmatch", DataObject.IndexOptions.S1) },
                    { "JoinCode", new DataObject(DataObject.VisibilityOptions.Member, "0") }
                }
            };
            string lobbyName = lobbyNameText.text;
            int maxPlayers = 2;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);
            var callbacks = new LobbyEventCallbacks();
            callbacks.KickedFromLobby += OnKickedFromLobby;
            m_LobbyEvents = await Lobbies.Instance.SubscribeToLobbyEventsAsync(lobby.Id, callbacks);
            hostLobby = lobby;
            joinedLobby = lobby;
            Debug.LogError("Lobby created: " + lobby.Id + " " + lobby.Name + " " + lobby.LobbyCode);
            if (joinedLobby == null)
            {
                Debug.Log("joinedLobby is null");
            }
            else
            {
                Debug.Log("joinedLobby is not null");
            }
        }
        catch (System.Exception e)
        {
            DisplayError(e);
        }
        HideLobbyCreation();
        //ShowCurrentLobby();
        menuButtonsInstance.ChangeToLobbyMenu();
        UpdateDisplayedLobby();
    }

    private void OnKickedFromLobby()
    {
        HideCurrentLobby();
        throw new NotImplementedException();
    }

    public Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                { "PlayerName",new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member ,playerName)}
            }
        };
    }

    public async void UpdatePlayerName(string newPlayerName)
    {
        await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, newPlayerName) }
            }
        });
    }

    public void StartCreatinNewLobby()
    {
        lobbyCreationMenu.active = true;
    }

    public void CancelCreatinNewLobby()
    {
        lobbyCreationMenu.active = false;
    }

    public void JoinClickedLobby()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        string lobbyId = clickedObject.GetComponent<lobbyData>().lobbyId;
        JoinLobbyById(lobbyId);
        ShowCurrentLobby();
        menuButtonsInstance.ChangeToLobbyMenu();
        UpdateDisplayedLobby();
    }

    public async void JoinLobbyById(string lobbyId)
    {
        try
        {
            JoinLobbyByIdOptions joinLobbyByIdOptions = new JoinLobbyByIdOptions
            {
                Player = GetPlayer()
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId, joinLobbyByIdOptions);

            joinedLobby = lobby;

            Debug.Log("Joined lobby " + lobbyId);

            PrintPlayers(joinedLobby);
        }
        catch (System.Exception e)
        {
            DisplayError(e);
        }
    }

    public void KickClickedPlayer()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        string playerId = clickedObject.GetComponent<playerData>().playerId;
        KickPlayer(hostLobby, playerId);
    }

    public async void KickPlayer(Lobby lobby, string playerId)
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(lobby.Id, playerId);
            Debug.Log("Kicked player " + playerId);
        }
        catch (System.Exception e)
        {
            DisplayError(e);
        }
    }

    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (System.Exception e)
        {
            DisplayError(e);
        }
    }

    public async void DeleteLobby()
    {
        try
        {
            await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
        }
        catch (System.Exception e)
        {
            DisplayError(e);
        }
    }

    bool IsLobbyHost()
    {
        if (joinedLobby != null)
        { 
            if (AuthenticationService.Instance.PlayerId == joinedLobby.HostId) return true; 
        }
        return false;
    }

    public async void StartGame()
    {
        if (IsLobbyHost())
        {
            try
            {
                Debug.Log("Start Game");
                string relayCode = await testRelayInstance.CreateRelayForLobby();
                if (joinedLobby == null) Debug.Log("joinedLobby is null");
                Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                    {
                        { "JoinCode", new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
                    }
                });
                dataTransfer.GetComponent<dataTransfer>().isHost = true;
                joinedLobby = lobby;
            } catch(System.Exception e)
            {
                DisplayError(e);
            }
        }
    }

    public async void QuickJoinLobby()
    {
        try
        {
            await Lobbies.Instance.QuickJoinLobbyAsync();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

    }
}
