using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Interface used for editing the configuration of the lobby service SDK.
    /// Primary usage is for testing purposes.
    /// </summary>
    public interface ILobbyServiceSDKConfiguration
    {
        /// <summary>
        /// Sets the base path in configuration.
        /// </summary>
        /// <param name="basePath">The base path to set in configuration.</param>
        void SetBasePath(string basePath);
    }
}
