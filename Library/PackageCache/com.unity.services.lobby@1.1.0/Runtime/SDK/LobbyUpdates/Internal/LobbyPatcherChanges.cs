using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Unity.Services.Lobbies
{
    internal class LobbyPatcherChanges : ILobbyChanges
    {
        public bool LobbyDeleted { get; private set; }
        public ChangedLobbyValue<string> Name { get; private set; }
        public ChangedLobbyValue<bool> IsPrivate { get; private set; }
        public ChangedLobbyValue<bool> IsLocked { get; private set; }
        public ChangedLobbyValue<bool> HasPassword { get;  private set; }
        public ChangedLobbyValue<int> AvailableSlots { get; private set; }
        public ChangedLobbyValue<int> MaxPlayers { get; private set; }
        public ChangedOrRemovedLobbyValue<Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>> Data { get; private set; }
        public ChangedLobbyValue<List<int>> PlayerLeft { get; private set; }
        public ChangedLobbyValue<List<LobbyPlayerJoined>> PlayerJoined { get; private set; }
        public ChangedLobbyValue<Dictionary<int, LobbyPlayerChanges>> PlayerData { get; private set; }
        public ChangedLobbyValue<string> HostId { get; private set; }
        public ChangedLobbyValue<int> Version { get; private set; }
        public ChangedLobbyValue<DateTime> LastUpdated { get; private set; }

        public LobbyPatcherChanges(int version)
        {
            Version = LobbyValue.Changed<int>(version);
        }

        public void LobbyDeletedChange()
        {
            LobbyDeleted = true;
        }

        public void NameChange(string name)
        {
            Name = LobbyValue.Changed<string>(name);
        }

        public void IsPrivateChange(bool isPrivate)
        {
            IsPrivate = LobbyValue.Changed<bool>(isPrivate);
        }

        public void IsLockedChange(bool isLocked)
        {
            IsLocked = LobbyValue.Changed<bool>(isLocked);
        }

        public void HasPasswordChange(bool hasPassword)
        {
            HasPassword = LobbyValue.Changed<bool>(hasPassword);
        }

        public void AvailableSlotsChange(int availableSlots)
        {
            AvailableSlots = LobbyValue.Changed<int>(availableSlots);
        }

        public void MaxPlayersChange(int maxPlayers)
        {
            MaxPlayers = LobbyValue.Changed<int>(maxPlayers);
        }

        public void DataChange(string key, DataObject dataObject)
        {
            if (!Data.Changed)
            {
                Data = LobbyValue.ChangedNotRemoved(new Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>());
            }
            Data.Value[key] = LobbyValue.ChangedNotRemoved(dataObject);
        }

        public void DataAdded(string key, DataObject dataObject)
        {
            if (!Data.Added)
            {
                Data = LobbyValue.ChangeAdded(new Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>());
            }
            Data.Value[key] = LobbyValue.ChangeAdded(dataObject);
        }

        public void DataRemoveChange()
        {
            Data = LobbyValue.Removed<Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>>();
        }

        public void DataRemoveChange(string key)
        {
            if (!Data.Changed)
            {
                Data = LobbyValue.ChangedNotRemoved(new Dictionary<string, ChangedOrRemovedLobbyValue<DataObject>>());
            }
            Data.Value[key] = LobbyValue.Removed<DataObject>();
        }

        public void HostChange(string newHostId)
        {
            HostId = LobbyValue.Changed<string>(newHostId);
        }

        public void LastUpdatedChange(DateTime lastUpdated)
        {
            LastUpdated = LobbyValue.Changed<DateTime>(lastUpdated);
        }

        public void PlayerLeftChange(int index)
        {
            if (!PlayerLeft.Changed)
            {
                PlayerLeft = LobbyValue.Changed(new List<int>());
            }
            PlayerLeft.Value.Add(index);
        }

        public void PlayerJoinedChange(int index, Player player)
        {
            if (!PlayerJoined.Changed)
            {
                PlayerJoined = LobbyValue.Added<List<LobbyPlayerJoined>>(new List<LobbyPlayerJoined>());
            }
            PlayerJoined.Value.Add(new LobbyPlayerJoined(index, player));
        }

        public void PlayerDataChange(int index, string key, PlayerDataObject playerDataObject)
        {
            var playerDataChanged = PreparePlayerDataChange(index);
            if (!playerDataChanged.ChangedData.Changed)
            {
                playerDataChanged.ChangedData = LobbyValue.ChangedNotRemoved(new Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>());
            }
            playerDataChanged.ChangedData.Value[key] = LobbyValue.ChangedNotRemoved(playerDataObject);
        }

        public void PlayerDataAdded(int index, string key, PlayerDataObject playerDataObject)
        {
            var playerDataChanged = PreparePlayerDataAddition(index);
            if (!playerDataChanged.ChangedData.Added)
            {
                playerDataChanged.ChangedData = LobbyValue.ChangeAdded(new Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>());
            }
            playerDataChanged.ChangedData.Value[key] = LobbyValue.ChangeAdded(playerDataObject);
        }

        public void PlayerDataRemoveChange(int index)
        {
            var playerDataChanged = PreparePlayerDataChange(index);
            if (!playerDataChanged.ChangedData.Changed)
            {
                playerDataChanged.ChangedData = LobbyValue.Removed<Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>>();
            }
        }

        public void PlayerDataRemoveChange(int index, string key)
        {
            var playerDataChanged = PreparePlayerDataChange(index);
            if (!playerDataChanged.ChangedData.Changed)
            {
                playerDataChanged.ChangedData = LobbyValue.ChangedNotRemoved(new Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>());
            }
            playerDataChanged.ChangedData.Value[key] = LobbyValue.Removed<PlayerDataObject>();
        }

        public void PlayerConnectionInfoChange(int index, string connectionInfo)
        {
            var playerDataChanged = PreparePlayerDataChange(index);
            playerDataChanged.ConnectionInfoChanged = LobbyValue.Changed(connectionInfo);
        }

        public void PlayerLastUpdatedChange(int index, DateTime lastUpdated)
        {
            var playerDataChanged = PreparePlayerDataChange(index);
            playerDataChanged.LastUpdatedChanged = LobbyValue.Changed(lastUpdated);
        }

        public void ApplyToLobby(Models.Lobby lobby)
        {
            LobbyPatcher.ApplyPatchesToLobby(this, lobby);
        }

        private LobbyPlayerChanges PreparePlayerDataChange(int index)
        {
            if (!PlayerData.Changed)
            {
                PlayerData = LobbyValue.Changed(new Dictionary<int, LobbyPlayerChanges>());
            }
            if (!PlayerData.Value.TryGetValue(index, out var playerDataChanged))
            {
                playerDataChanged = new LobbyPlayerChanges(index);
                PlayerData.Value[index] = playerDataChanged;
            }
            return playerDataChanged;
        }

        private LobbyPlayerChanges PreparePlayerDataAddition(int index)
        {
            if (!PlayerData.Changed)
            {
                PlayerData = LobbyValue.Added(new Dictionary<int, LobbyPlayerChanges>());
            }
            if (!PlayerData.Value.TryGetValue(index, out var playerDataChanged))
            {
                playerDataChanged = new LobbyPlayerChanges(index);
                PlayerData.Value[index] = playerDataChanged;
            }
            return playerDataChanged;
        }
    }
}
