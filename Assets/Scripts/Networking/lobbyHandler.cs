using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class lobbyHandler : MonoBehaviour
{
    private Lobby hostLobby;
    private Lobby joinedLobby;
    private float lobbyUpdateTimer = 5.1f;
    private string playerName;

    private void Start()
    {
        playerName = "KIWI" + Random.Range(0, 1000);
        Debug.Log("Player name: " + playerName);
        StartCoroutine(HandleLobbyHeartBeat());
    }

    private void Update()
    {
        HandleLobbyPullForUpdates();
    }

    private async void HandleLobbyPullForUpdates()
    {
        if (joinedLobby != null)
        {
            lobbyUpdateTimer -= Time.deltaTime;
            if (lobbyUpdateTimer < 0f)
            {
                lobbyUpdateTimer = 1.1f;
                Lobby lobby = await Lobbies.Instance.GetLobbyAsync(joinedLobby.Id);
                joinedLobby = lobby;
            }
        }
    }

    private IEnumerator HandleLobbyHeartBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            if (hostLobby != null)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    public async void CreateLobby()
    {
        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, "Deathmatch", DataObject.IndexOptions.S1) }
                }
            };
            string lobbyName = "My Lobby";
            int maxPlayers = 2;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);
            hostLobby = lobby;
            joinedLobby = lobby;
            Debug.LogError("Lobby created: " + lobby.Id + " " + lobby.Name + " " + lobby.LobbyCode);

            PrintPlayers(lobby);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public async void QuickJoinLobby()
    {
        try
        {
            await Lobbies.Instance.QuickJoinLobbyAsync();
        } catch (System.Exception e)
        {
            Debug.LogError(e);
        }
        
    }

    

    public async void ListLobbies()
    {
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
            Debug.Log(lobby.Id + " " + lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Data["GameMode"].Value);
        }
    }

    public void Awake()
    {
        StartUnityServices();
    }

    public async void StartUnityServices()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        else
        {
            Debug.Log("Already signed in " + AuthenticationService.Instance.PlayerId);
        }

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

    public async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            }; 
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);

            joinedLobby = lobby;

            Debug.Log("Joined lobby " + lobbyCode);

            PrintPlayers(joinedLobby);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void PrintPlayers()
    {
        PrintPlayers(joinedLobby);
    }

    public void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Players in lobby " + lobby.Name + " " + lobby.Data["GameMode"].Value);
        foreach (Player player in lobby.Players)
        { 
            Debug.Log(player.Id + " " + player.Data["PlayerName"].Value);
        }
    }

    public async void UpdateLobbyGameMode(string gameMode)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
            {
                { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, gameMode, DataObject.IndexOptions.S1) }
            }
            });

            joinedLobby = hostLobby;
            PrintPlayers(hostLobby);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
        }
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

    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public async void KickPlayer()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, joinedLobby.Players[1].Id);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }
    public async void MigrateLobbyHost()
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                HostId = joinedLobby.Players[1].Id
            });

            joinedLobby = hostLobby;
            PrintPlayers(hostLobby);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public async void DeleteLobby()
    {
        try
        {
            await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
        } catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }
}
