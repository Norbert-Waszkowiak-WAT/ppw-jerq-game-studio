using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using Unity.Services.Wire.Internal;

namespace Unity.Services.Lobbies.Internal
{
    internal class LobbyWireTokenProvider : IChannelTokenProvider
    {
        string lobbyId;
        WrappedLobbyService lobbyService;

        internal LobbyWireTokenProvider(string lobbyId, WrappedLobbyService lobbyService)
        {
            if (lobbyId == null)
            {
                Logger.LogError($"{nameof(LobbyWireTokenProvider)} is invalid as its {nameof(lobbyId)} is null!");
            }
            if (lobbyService == null)
            {
                Logger.LogError($"{nameof(LobbyWireTokenProvider)} is invalid as its {nameof(lobbyService)} is null!");
            }
            this.lobbyId = lobbyId;
            this.lobbyService = lobbyService;
        }

        public async Task<ChannelToken> GetTokenAsync()
        {
            var tokens = await lobbyService.RequestTokensAsync(lobbyId, TokenRequest.TokenTypeOptions.WireJoin);
            if (tokens.TryGetValue("wireJoin", out var tokenData))
            {
                return new ChannelToken() { ChannelName = tokenData.Uri, Token = tokenData.TokenValue };
            }
            return new ChannelToken();
        }
    }
}
