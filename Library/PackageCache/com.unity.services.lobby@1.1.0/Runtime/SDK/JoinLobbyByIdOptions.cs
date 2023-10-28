using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Optional parameter class for Lobby Join By ID requests
    /// </summary>
    public class JoinLobbyByIdOptions
    {
        /// <summary>
        /// Information about a specific player joining the lobby.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// The password for the lobby. Only required when a lobby has a password.
        /// </summary>
        public string Password { get; set; }
    }
}
