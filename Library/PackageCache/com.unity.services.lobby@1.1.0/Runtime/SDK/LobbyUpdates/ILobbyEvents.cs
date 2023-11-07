using System.Threading.Tasks;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// An interface for managing a lobby events subscription and the callbacks associated with it.
    /// </summary>
    public interface ILobbyEvents
    {
        /// <summary>
        /// The callbacks associated with the lobby events subscription.
        /// </summary>
        LobbyEventCallbacks Callbacks { get; }

        /// <summary>
        /// Subscribes or re-subscribes to the events for the lobby associated with this lobby events.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        Task SubscribeAsync();

        /// <summary>
        /// Unsubscribes from the events for the lobby associated with this lobby events.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        Task UnsubscribeAsync();
    }
}
