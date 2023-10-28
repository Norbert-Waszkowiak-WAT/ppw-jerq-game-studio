using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Paramters to update on a given UpdatePlayer request.
    /// </summary>
    public class UpdatePlayerOptions 
    {
        /// <summary>
        /// Connection information for connecting to a relay with this player.
        /// </summary>
        public string ConnectionInfo { get; set; }

        /// <summary>
        /// Custom game-specific properties to add, update or remove from the player (e.g. role or skill).
        /// To remove an existing property, include it in data but set property object to null.
        /// To update the value to null, set the value property of the object to null.
        /// </summary>
        public Dictionary<string, PlayerDataObject> Data { get; set; }

        /// <summary>
        /// An ID that associates this player in this lobby with a persistent connection.
        /// When a disconnect notification is received, this value is used to identify the associated player in a lobby to mark them as disconnected.
        /// </summary>
        public string AllocationId { get; set; }
    }
}