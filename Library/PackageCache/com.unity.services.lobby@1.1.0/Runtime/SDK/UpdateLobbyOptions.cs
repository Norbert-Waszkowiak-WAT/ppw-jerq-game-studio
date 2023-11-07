using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Parameters to update on a given UpdateLobby request.
    /// </summary>
    public class UpdateLobbyOptions
    {
        /// <summary>
        /// The name of the lobby that should be displayed to users. All whitespace will be trimmed from the name.
        /// Minimum length: 1. Maximum length: 256.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The maximum number of players allowed in the lobby. Must be greater than or equal to the current number of players in the lobby.
        /// Minimum: 1. Maximum: 100.
        /// </summary>
        public int? MaxPlayers { get; set; }

        /// <summary>
        /// Indicates whether or not the lobby is publicly visible and will show up in query results.
        /// If the lobby is not publicly visible, the creator can share the LobbyCode with other users who can use it to join this lobby.
        /// </summary>
        public bool? IsPrivate { get; set; }

        /// <summary>
        /// Indicates whether or not the lobby is locked.
        /// </summary>
        public bool? IsLocked { get; set; }

        /// <summary>
        /// The new password for the target lobby. Updating to empty string "" will remove password protection from the lobby and set HasPassword to false.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Custom game-specific properties to add, update, or remove from the lobby (e.g. mapName or gameType).
        /// To remove an existing property, include it in data but set the property object to null. To update the value to null, set the value property of the object to null.
        /// </summary>
        public Dictionary<string, DataObject> Data { get; set; }

        /// <summary>
        /// The ID of the player to make the host of the lobby. As soon as this is updated the current host will no longer have permission to modify the lobby.
        /// </summary>
        public string HostId { get; set; }
    }
}
