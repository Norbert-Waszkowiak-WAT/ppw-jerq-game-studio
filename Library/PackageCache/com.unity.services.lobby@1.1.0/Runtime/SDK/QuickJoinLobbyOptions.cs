using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Optional parameters for Quick Joining a Lobby.
    /// Includes queryable fields for Lobby selection criterion.
    /// </summary>
    public class QuickJoinLobbyOptions 
    {
        /// <summary>
        /// Information about a specific player joining the lobby.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// A list of filters which can be used to narrow down which lobbies to attempt to join.
        /// </summary>
        public List<QueryFilter> Filter { get; set; }
    }
}