#if UGS_LOBBY_VIVOX
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity.Services.Authentication.Internal;
using Unity.Services.Vivox.Internal;
using UnityEngine;

namespace Unity.Services.Lobbies.Internal
{
    internal class LobbyVivoxTokenProvider : IVivoxTokenProviderInternal
    {
        private const string k_VivoxJoinTokenKey = "vivoxJoin";

        private ILobbyServiceInternal m_LobbyService;

        private IEnvironmentId m_EnvironmentId;

        public LobbyVivoxTokenProvider(ILobbyServiceInternal lobbyService, IEnvironmentId environmentId)
        {
            m_LobbyService = lobbyService;
            m_EnvironmentId = environmentId;
        }

        public async Task<string> GetTokenAsync(string issuer = null, TimeSpan? expiration = null, string userUri = null, string action = null, string conferenceUri = null, string fromUserUri = null, string realm = null)
        {
            if (conferenceUri == null)
            {
                throw new InvalidOperationException($"Unable to get Token for null lobbyId[{conferenceUri}]!");
            }

            var environmentId = m_EnvironmentId.EnvironmentId;
            if (environmentId == null)
            {
                throw new InvalidOperationException("Unable to get environmentId! Therefore, we are unable to get the channel name!");
            }
            var lobbyId = ExtractChannelNameFromConferenceUri(conferenceUri);

            var response = await m_LobbyService.RequestTokensAsync(lobbyId, Models.TokenRequest.TokenTypeOptions.VivoxJoin);
            if (response == null)
            {
                throw new InvalidOperationException($"{nameof(m_LobbyService.RequestTokensAsync)} response was null!");
            }

            if (!response.TryGetValue(k_VivoxJoinTokenKey, out var tokenData))
            {
                var builder = new StringBuilder($"Failed to get Vivox Token for Lobby using key[{k_VivoxJoinTokenKey} Response contained the following tokens:\n");
                foreach (var token in response)
                {
                    builder.Append($"{{ \"{token.Key}\": {{ \"Uri\":\"{token.Value.Uri}\" \"TokenValue\":\"{token.Value.TokenValue}\" }} }}");
                }
                throw new InvalidOperationException(builder.ToString());
            }

            return tokenData.TokenValue;
        }

        internal static string ExtractChannelNameFromConferenceUri(string conferenceUri)
        {
            var matchGroups =
                new Regex("sip:confctl-(?<uriDesignator>[a-zA-Z0-9]+)-(?<issuer>[a-zA-Z0-9-]+).(?<channelName>[a-zA-Z0-9]+).(?<environmentId>[a-zA-Z0-9-]+)@(?<domain>[a-zA-Z0-9.]+)")
                    .Match(conferenceUri);
            var channelName = matchGroups.Groups["channelName"].Value;
            if (string.IsNullOrEmpty(channelName))
            {
                throw new InvalidOperationException($"Unable to parse channel name for lobby from conferenceUri[{conferenceUri}]! Expected the form: sip:confctl-{{GetUriDesignator(_type)}}-{{_issuer}}.{{_channelName}}.{{_environmentId}}@{{_domain}}");
            }
            return channelName;
        }
    }
}
#endif
