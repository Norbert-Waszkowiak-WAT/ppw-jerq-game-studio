using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class dataTransfer : MonoBehaviour
{
    public List<Material> materials;

    public string joinCode;

    public bool createNewGame;

    public string relayCode;

    public bool isHost;

    public List<int> selectedWeapons;

    public Lobby currentLobby;

    public bool lobbyQuit = false;
}
