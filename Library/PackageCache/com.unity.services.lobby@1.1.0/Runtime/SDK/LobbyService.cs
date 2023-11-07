using System;
using System.Runtime.CompilerServices;
using Unity.Services.Lobbies.Internal;

[assembly: InternalsVisibleTo("Unity.Services.Lobbies.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Here is the first point and call for accessing the Lobby Package's features!
    /// Use the .Instance method to get a singleton of the ILobbyServiceSDK, and from there you can make various requests to the Lobby service API.
    /// </summary>
    public static class LobbyService
    {
        internal static ILobbyService service { get; set; }

        private static readonly Configuration configuration;

        static LobbyService()
        {
            configuration = new Configuration("https://lobby.services.api.unity.com/v1", 10, 4, null);
        }

        /// <summary>
        /// Provides the Lobby Service SDK interface for making service API requests.
        /// </summary>
        public static ILobbyService Instance
        {
            get
            {
                if (service == null)
                {
                    InitializeWrappedLobbyService();
                }
                return service;
            }

            internal set
            {
                service = value;
            }
        }

        private static void InitializeWrappedLobbyService()
        {
            var lobbyService = LobbyServiceSdk.Instance;
            if (lobbyService == null)
            {
                throw new InvalidOperationException($"Unable to get {nameof(ILobbyServiceSdk)} because Lobby API is not initialized. Make sure you call UnityServices.InitializeAsync().");
            }
            service = new WrappedLobbyService(lobbyService);
        }
    }

    /// <summary>
    /// This class is marked for deprecation. Please use LobbyService instead.
    /// </summary>
    public static class Lobbies
    {
        /// <summary>
        /// This class is marked for deprecation. Please use the following code instead: var sdkConfiguration = (ILobbyServiceSDKConfiguration)Lobbies.Instance; sdkConfiguration.SetBasePath(basePath);
        /// </summary>
        /// <param name="basePath">The base path to be set for the configuration.</param>
        public static void SetBasePath(string basePath)
        {
            var lobbyServiceSdkConfiguration = (ILobbyServiceSDKConfiguration)Instance;
            lobbyServiceSdkConfiguration.SetBasePath(basePath);
        }

        /// <summary>
        /// Provides the Lobby Service SDK interface for making service API requests.
        /// </summary>
        public static ILobbyServiceSDK Instance
        {
            get
            {
                return (ILobbyServiceSDK)LobbyService.Instance;
            }
        }
    }
}
