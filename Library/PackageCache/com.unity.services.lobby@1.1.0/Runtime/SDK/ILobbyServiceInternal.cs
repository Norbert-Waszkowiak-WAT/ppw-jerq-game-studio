using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Unity.Services.Lobbies.Internal
{
    internal interface ILobbyServiceInternal
    {
        Task<Dictionary<string, Models.TokenData>> RequestTokensAsync(string lobbyId, params TokenRequest.TokenTypeOptions[] tokenOptions);

        Dictionary<string, Models.Lobby> GetLobbyCache();
    }
}
