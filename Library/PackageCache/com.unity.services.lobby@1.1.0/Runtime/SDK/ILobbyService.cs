using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Service for Lobbies.
    /// Provides user the ability to create, delete, update, and query Lobbies.
    /// Includes operations for interacting with given players in a Lobby context.
    /// </summary>
    public interface ILobbyService
    {
        /// <summary>
        /// Create a Lobby with a given name and specified player limit.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyName">Name of new lobby</param>
        /// <param name="maxPlayers">Player limit</param>
        /// <param name="options">Optional request parameters</param>
        /// <returns>Lobby data for the lobby that was just created</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> CreateLobbyAsync(string lobbyName, int maxPlayers, CreateLobbyOptions options = default);

        /// <summary>
        /// Create or join a Lobby with a given name and ID and specified player limit.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId">ID of the lobby to create/join</param>
        /// <param name="lobbyName">Name of the lobby to create/join</param>
        /// <param name="maxPlayers">Player limit</param>
        /// <param name="options">Optional request parameters</param>
        /// <returns>Lobby data for the lobby that was just created/joined</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> CreateOrJoinLobbyAsync(string lobbyId, string lobbyName, int maxPlayers, CreateLobbyOptions options = default);

        /// <summary>
        /// A subscription to the given lobby is created and the given callbacks are associated with it.
        /// The return ILobbyEvents interface can be used to unsubscribe and re-subscribe to the connection.
        /// The callbacks object provided will be used to provide the notifications from the subscription.
        /// </summary>
        /// <param name="lobbyId">The ID of the lobby you are subscribing to events for.</param>
        /// <param name="callbacks">The callbacks you provide, which will be called as notifications arrive from the subscription.</param>
        /// <returns>An interface to change the callbacks associated with the subscription, or to unsubscribe and re-subscribe to the lobby's events.</returns>
        Task<ILobbyEvents> SubscribeToLobbyEventsAsync(string lobbyId, LobbyEventCallbacks callbacks);

        /// <summary>
        /// Delete a Lobby by specifying a Lobby ID.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId">ID of the Lobby to delete</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task DeleteLobbyAsync(string lobbyId);

        /// <summary>
        /// Async Operation.
        /// Get currently joined lobbies.
        /// </summary>
        /// <returns>List of lobbies the active player has joined</returns>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<List<string>> GetJoinedLobbiesAsync();

        /// <summary>
        /// Retrieve data for a Lobby by specifying a Lobby ID.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId">ID of the Lobby to retrieve</param>
        /// <returns>Lobby data</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> GetLobbyAsync(string lobbyId);

        /// <summary>
        /// Send a heartbeat ping to keep the Lobby active.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId">ID of the Lobby to ping</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task SendHeartbeatPingAsync(string lobbyId);

        /// <summary>
        /// Join a Lobby using a given Lobby Invite Code.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyCode">Invite Code for target lobby.</param>
        /// <param name="options">Optional request parameters</param>
        /// <returns>Lobby data for the lobby joined</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> JoinLobbyByCodeAsync(string lobbyCode, JoinLobbyByCodeOptions options = default);

        /// <summary>
        /// Join a Lobby by specifying the Lobby ID.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId">ID of the Lobby to join</param>
        /// <param name="options">Optional request parameters</param>
        /// <returns>Lobby data for the lobby joined</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> JoinLobbyByIdAsync(string lobbyId, JoinLobbyByIdOptions options = default);

        /// <summary>
        /// Query and retrieve a list of lobbies that meet specified query parameters.
        /// Async operation.
        /// </summary>
        /// <param name="options">Query parameters</param>
        /// <returns>Query response that includes list of Lobbies meeting specified parameters</returns>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<QueryResponse> QueryLobbiesAsync(QueryLobbiesOptions options = default);

        /// <summary>
        /// Query available lobbies and join a randomly selected instance.
        /// Async operation.
        /// </summary>
        /// <param name="options">Optional parameters (includes queryable arguments)</param>
        /// <returns>Lobby data for the lobby joined</returns>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> QuickJoinLobbyAsync(QuickJoinLobbyOptions options = default);

        /// <summary>
        /// Remove a player from a specified Lobby.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId">Target Lobby ID to remove player from</param>
        /// <param name="playerId">Player ID to remove</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task RemovePlayerAsync(string lobbyId, string playerId);

        /// <summary>
        /// Update the specified Lobby with the given option parameters.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId">Lobby ID to update</param>
        /// <param name="options">Parameters to update</param>
        /// <returns>Lobby data of the updated Lobby</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> UpdateLobbyAsync(string lobbyId, UpdateLobbyOptions options);

        /// <summary>
        /// Update player lobby associated data with the given option parameters.
        /// Async operation.
        /// </summary>
        /// <param name="lobbyId"></param>
        /// <param name="playerId"></param>
        /// <param name="options"></param>
        /// <returns>Lobby data of the updated Lobby</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Unity.Services.Lobbies.LobbyServiceException"></exception>
        Task<Models.Lobby> UpdatePlayerAsync(string lobbyId, string playerId, UpdatePlayerOptions options);

        /// <summary>
        /// Reconnects to the lobby.
        /// </summary>
        /// <param name="lobbyId">The ID of the lobby to reconnect to.</param>
        /// <returns>The lobby you reconnected to.</returns>
        Task<Models.Lobby> ReconnectToLobbyAsync(string lobbyId);
    }

    /// <summary>
    /// This interface is marked for deprecation. Please use ILobbyService instead.
    /// </summary>
    public interface ILobbyServiceSDK : ILobbyService
    {
    }
}
