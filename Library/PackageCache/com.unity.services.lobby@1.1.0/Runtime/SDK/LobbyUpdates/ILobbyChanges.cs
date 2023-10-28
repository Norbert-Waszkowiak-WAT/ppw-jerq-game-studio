using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// An interface providing a set of changes to apply to a lobby model.
    /// </summary>
    public interface ILobbyChanges
    {
        /// <summary>
        /// Whether or not the lobby was deleted.
        /// True if the lobby was deleted. False if it is still available.
        /// </summary>
        bool LobbyDeleted { get; }

        /// <summary>
        /// The change to the lobby’s name, if it has changed.
        /// </summary>
        ChangedLobbyValue<string> Name { get; }

        /// <summary>
        /// The change for if the lobby is private, if it has changed.
        /// </summary>
        ChangedLobbyValue<bool> IsPrivate { get; }

        /// <summary>
        /// The change for if the lobby is locked, if it has changed.
        /// </summary>
        ChangedLobbyValue<bool> IsLocked { get; }

        /// <summary>
        /// The change for if HasPassword has changed.
        /// </summary>
        ChangedLobbyValue<bool> HasPassword { get; }

        /// The change to the available slots in the lobby, if it has changed.
        /// </summary>
        ChangedLobbyValue<int> AvailableSlots { get; }

        /// <summary>
        /// The change to the maximum number of players in the lobby, if it has changed.
        /// </summary>
        ChangedLobbyValue<int> MaxPlayers { get; }

        /// <summary>
        /// The changes to the lobby’s data, if it has changed.
        /// </summary>
        ChangedOrRemovedLobbyValue<Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>> Data { get; }

        /// <summary>
        /// A list of players that have left, if any.
        /// </summary>
        ChangedLobbyValue<List<int>> PlayerLeft { get; }

        /// <summary>
        /// A list of players that have joined, if any.
        /// </summary>
        ChangedLobbyValue<List<LobbyPlayerJoined>> PlayerJoined { get; }

        /// <summary>
        /// The changes to player’s data, if any have changed.
        /// </summary>
        ChangedLobbyValue<Dictionary<int, LobbyPlayerChanges>> PlayerData { get; }

        /// <summary>
        /// The changes to the lobby’s host ID, if it has changed.
        /// </summary>
        ChangedLobbyValue<string> HostId { get; }

        /// <summary>
        /// The changes to the lobby’s version, if it has changed.
        /// </summary>
        ChangedLobbyValue<int> Version { get; }

        /// <summary>
        /// The time the lobby changes occurred.
        /// </summary>
        ChangedLobbyValue<DateTime> LastUpdated { get; }

        /// <summary>
        /// Takes a lobby and a change applicator to update a given lobby in-place.
        /// If LobbyDeleted is true, no changes will be applied and a warning will be logged.
        /// </summary>
        /// <param name="lobby">The lobby model to apply the changes to.</param>
        void ApplyToLobby(Models.Lobby lobby);
    }
}
