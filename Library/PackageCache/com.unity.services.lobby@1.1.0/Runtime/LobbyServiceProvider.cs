using UnityEngine;
using System.Threading.Tasks;

using Unity.Services.Lobbies.Apis.Lobby;

using Unity.Services.Lobbies.Http;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Telemetry.Internal;
using Unity.Services.Authentication.Internal;
using Unity.Services.Wire.Internal;
using Unity.Services.Vivox.Internal;
using Unity.Services.Lobbies.Internal;

namespace Unity.Services.Lobbies
{
    internal class LobbyServiceProvider : IInitializablePackage
    {
        const string k_PackageName = "com.unity.services.lobby";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            // Pass an instance of this class to Core
            var generatedPackageRegistry =
                CoreRegistry.Instance.RegisterPackage(new LobbyServiceProvider())
                    .DependsOn<IAccessToken>()
                    .DependsOn<IMetricsFactory>()
                    .OptionallyDependsOn<IWire>()
                    .OptionallyDependsOn<IEnvironmentId>()
                    .OptionallyDependsOn<IVivox>();
        }

        public Task Initialize(CoreRegistry registry)
        {
            var httpClient = new HttpClient();

            var accessTokenLobby = registry.GetServiceComponent<IAccessToken>();
            var wire = registry.GetServiceComponent<IWire>();
            if (wire == null)
            {
                Logger.LogWarning($"The {nameof(IWire)} component is not available. LobbyEvents functionality unavailable.");
            }

            if (accessTokenLobby != null)
            {
                var metricsFactory = registry.GetServiceComponent<IMetricsFactory>();
                var metrics = metricsFactory.Create(k_PackageName);

                LobbyServiceSdk.Instance = new InternalLobbyService(httpClient, registry.GetServiceComponent<IAccessToken>(), wire, metrics);
#if UGS_LOBBY_VIVOX
                var vivox = registry.GetServiceComponent<IVivox>();
                if (vivox == null)
                {
                    Logger.LogWarning($"Version define UGS_LOBBY_VIVOX is defined, but the {nameof(IVivox)} component is not available. This means you do not have the Vivox package in your project!");
                }
                else
                {
                    var environmentId = registry.GetServiceComponent<IEnvironmentId>();
                    vivox.RegisterTokenProvider(new LobbyVivoxTokenProvider((ILobbyServiceInternal)LobbyService.Instance, environmentId));
                }
#endif
            }

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// InternalLobbyService
    /// </summary>
    internal class InternalLobbyService : ILobbyServiceSdk
    {
        /// <summary>
        /// Constructor for InternalLobbyService
        /// </summary>
        /// <param name="httpClient">The HttpClient for InternalLobbyService.</param>
        /// <param name="accessToken">The Authentication token for the service.</param>
        public InternalLobbyService(HttpClient httpClient, IAccessToken accessToken = null, IWire subscriptionFactory = null, IMetrics metrics = null)
        {
            LobbyApi = new LobbyApiClient(httpClient, accessToken);

            Configuration = new Configuration("https://lobby.services.api.unity.com/v1", 10, 4, null);

            Wire = subscriptionFactory;

            Metrics = metrics;
        }

        /// <summary> Instance of ILobbyApiClient interface</summary>
        public ILobbyApiClient LobbyApi { get; set; }

        /// <summary> Configuration properties for the service.</summary>
        public Configuration Configuration { get; set; }

        /// <summary> Instance of the wire component for the service.</summary>
        public IWire Wire { get; set; }

        /// <summary> Instance of the metrics component for the service.</summary>
        public IMetrics Metrics { get; }
    }
}
