using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

/// <summary>
/// A class for you to provide the callbacks you want to be called from the lobby event subscription.
/// </summary>
public class LobbyEventCallbacks
{
    /// <summary>
    /// Event called when a change has occurred to a lobby on the server.
    /// </summary>
    public event Action<ILobbyChanges> LobbyChanged;

    /// <summary>
    /// Event called when a player join has occurred to a lobby on the server.
    /// </summary>
    public event Action<List<LobbyPlayerJoined>> PlayerJoined;

    /// <summary>
    /// Event called when a player leave has occurred to a lobby on the server.
    /// (See <see cref="LobbyPatcher.ApplyPatchesToLobby"/>)
    /// <returns>The indices of the players who left.</returns>
    /// </summary>
    public event Action<List<int>> PlayerLeft;

    /// <summary>
    /// Event called when lobby data changes on the server.
    /// </summary>
    public event Action<Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>> DataChanged;

    /// <summary>
    /// Event called when lobby data is removed on the server.
    /// Does not cover when the whole data object is completely removed (eg. set to null)
    /// For this case, use the DataChanged event instead
    /// </summary>
    public event Action<Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>> DataRemoved;

    /// <summary>
    /// Event called when lobby data is added on the server.
    /// </summary>
    public event Action<Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>> DataAdded;

    /// <summary>
    /// Event called when player data changes.
    /// The outer dictionary is indexed on player indices.
    /// The inner dictionary is indexed on the changed data key.
    /// </summary>
    public event Action<Dictionary<int, Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>> PlayerDataChanged;

    /// <summary>
    /// Event called when player data is removed on the server.
    /// Does not cover when the whole data object is completely removed (eg. set to null)
    /// For this case, use the PlayerDataChanged event instead
    /// </summary>
    public event Action<Dictionary<int, Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>> PlayerDataRemoved;

    /// <summary>
    /// Event called when player data is added on the server.
    /// </summary>
    public event Action<Dictionary<int, Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>> PlayerDataAdded;

    /// <summary>
    /// Event called when a lobby is deleted.
    /// </summary>
    public event Action LobbyDeleted;

    /// <summary>
    /// Event called when a kick has been received from the lobby event subscription.
    /// </summary>
    public event Action KickedFromLobby;

    /// <summary>
    /// Event called when the connection state of the lobby event subscription changes.
    /// </summary>
    public event Action<LobbyEventConnectionState> LobbyEventConnectionStateChanged;

    internal void InvokeLobbyChanged(ILobbyChanges changes)
    {
        LobbyChanged?.Invoke(changes);

        if (changes.LobbyDeleted)
        {
            LobbyDeleted?.Invoke();
        }

        if (changes.PlayerJoined.Changed || changes.PlayerJoined.Added)
        {
            PlayerJoined?.Invoke(changes.PlayerJoined.Value);
        }

        if (changes.PlayerLeft.Changed || changes.PlayerLeft.Added)
        {
            PlayerLeft?.Invoke(changes.PlayerLeft.Value);
        }

        if (changes.Data.Added || changes.Data.Changed || changes.Data.Removed)
        {
            var changedData = new Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>();
            var removedData = new Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>();
            var addedData = new Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>();

            if (changes.Data.Value == null)
            {
                DataRemoved?.Invoke(null);
                return;
            }

            foreach (var dataKey in changes.Data.Value.Keys)
            {
                if (changes.Data.Added || changes.Data.Value[dataKey].Added)
                {
                    addedData.Add(dataKey, changes.Data.Value[dataKey]);
                }
                else if (changes.Data.Removed || changes.Data.Value[dataKey].Removed)
                {
                    removedData.Add(dataKey, changes.Data.Value[dataKey]);
                }
                // Make sure the data has been only changed (not added nor removed)
                else if (changes.Data.Changed && changes.Data.Value[dataKey].Changed)
                {
                    changedData.Add(dataKey, changes.Data.Value[dataKey]);
                }
            }

            if (changedData.Count > 0)
                DataChanged?.Invoke(changedData);
            if (removedData.Count > 0)
                DataRemoved?.Invoke(removedData);
            if (addedData.Count > 0)
                DataAdded?.Invoke(addedData);
        }

        if (changes.PlayerData.Added || changes.PlayerData.Changed)
        {
            var changedPlayerData = new Dictionary<int, Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>();
            var removedPlayerData = new Dictionary<int, Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>();
            var addedPlayerData = new Dictionary<int, Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>();

            // For each player
            foreach (var playerKvp in changes.PlayerData.Value)
            {
                // If all the player data was removed, return an empty dictionary
                if (playerKvp.Value == null || playerKvp.Value.ChangedData.Value == null)
                {
                    removedPlayerData.Add(playerKvp.Key, null);
                    continue;
                }

                // For each data on the player
                foreach (var playerDataKvp in playerKvp.Value.ChangedData.Value)
                {
                    if (playerDataKvp.Value.Added)
                    {
                        if (!addedPlayerData.ContainsKey(playerKvp.Key))
                            addedPlayerData.Add(playerKvp.Key, new Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>());
                        addedPlayerData[playerKvp.Key].Add(playerDataKvp.Key, playerDataKvp.Value);
                    }
                    else if (playerDataKvp.Value.Removed)
                    {
                        if (!removedPlayerData.ContainsKey(playerKvp.Key))
                            removedPlayerData.Add(playerKvp.Key, new Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>());
                        removedPlayerData[playerKvp.Key].Add(playerDataKvp.Key, playerDataKvp.Value);
                    }
                    else if (playerDataKvp.Value.Changed)
                    {
                        if (!changedPlayerData.ContainsKey(playerKvp.Key))
                            changedPlayerData.Add(playerKvp.Key, new Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>());
                        changedPlayerData[playerKvp.Key].Add(playerDataKvp.Key, playerDataKvp.Value);
                    }
                }
            }

            if (changedPlayerData.Count > 0)
                PlayerDataChanged?.Invoke(changedPlayerData);
            if (removedPlayerData.Count > 0)
                PlayerDataRemoved?.Invoke(removedPlayerData);
            if (addedPlayerData.Count > 0)
                PlayerDataAdded?.Invoke(addedPlayerData);
        }
    }

    internal void InvokeKickedFromLobby()
    {
        KickedFromLobby?.Invoke();
    }

    internal void InvokeLobbyEventConnectionStateChanged(LobbyEventConnectionState state)
    {
        LobbyEventConnectionStateChanged?.Invoke(state);
    }
}
