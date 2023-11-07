using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Contains information about the player that joined.
    /// </summary>
    public struct LobbyPlayerJoined
    {
        /// <summary>
        /// The index the player joined at.
        /// </summary>
        public int PlayerIndex { get; }

        /// <summary>
        /// The player that joined.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Creates a set of information about a player that joined.
        /// </summary>
        /// <param name="index">The index the player joined at.</param>
        /// <param name="player">The player that joined.</param>
        public LobbyPlayerJoined(int index, Player player)
        {
            this.PlayerIndex = index;
            this.Player = player;
        }
    }
}
