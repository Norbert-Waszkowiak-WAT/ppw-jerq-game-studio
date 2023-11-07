namespace Unity.Services.Lobbies
{
    /// <summary>
    /// An enum describing the current state of a Lobby Event subscription's connection status.
    /// </summary>
    public enum LobbyEventConnectionState
    {
        /// <summary>
        /// The lobby event subscription has reached an unknown state.
        /// </summary>
        Unknown,

        /// <summary>
        /// The lobby event subscription is currently unsubscribed.
        /// </summary>
        Unsubscribed,

        /// <summary>
        /// The lobby event subscription is currently trying to connect to the service.
        /// </summary>
        Subscribing,

        /// <summary>
        /// The lobby event subscription is currently connected, and ready to receive notifications.
        /// </summary>
        Subscribed,

        /// <summary>
        /// The lobby event subscription is currently connected, but for some reason is having trouble receiving notifications.
        /// </summary>
        Unsynced,

        /// <summary>
        /// The lobby event subscription is currently in an error state, and won't recover on its own.
        /// </summary>
        Error,
    }
}
