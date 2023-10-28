using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Contains information about a set of changes to a player.
    /// </summary>
    public class LobbyPlayerChanges
    {
        /// <summary>
        /// The index of the player.
        /// </summary>
        public int PlayerIndex { get; }

        /// <summary>
        /// The change to the player's connection information, if it has changed.
        /// </summary>
        public ChangedLobbyValue<string> ConnectionInfoChanged { get; internal set; }

        /// <summary>
        /// When the change to the player occurrred.
        /// </summary>
        public ChangedLobbyValue<DateTime> LastUpdatedChanged { get; internal set; }

        /// <summary>
        /// The changes to the player's data.
        /// </summary>
        public ChangedOrRemovedLobbyValue<Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>> ChangedData { get; internal set; }

        /// <summary>
        /// Creates a set of changes for a player.
        /// </summary>
        /// <param name="index">The index of the player.</param>
        public LobbyPlayerChanges(int index)
        {
            PlayerIndex = index;
            ConnectionInfoChanged = new ChangedLobbyValue<string>();
            LastUpdatedChanged = new ChangedLobbyValue<DateTime>();
            ChangedData = new ChangedOrRemovedLobbyValue<Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>();
        }
    }
}
