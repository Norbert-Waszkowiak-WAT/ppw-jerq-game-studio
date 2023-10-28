using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Scheduler.Internal;
using Unity.Services.Core.Threading.Internal;
using Unity.Services.Core.Telemetry.Internal;
using UnityEngine;

namespace Unity.Services.Wire.Internal
{
    class WireServiceProvider : IInitializablePackage
    {
        const string k_CloudEnvironmentKey = "com.unity.services.core.cloud-environment";
        const string k_StagingEnvironment = "staging";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            // Pass an instance of this class to Core
            var generatedPackageRegistry =
                CoreRegistry.Instance.RegisterPackage(new WireServiceProvider());
            // And specify what components it requires, or provides.
            generatedPackageRegistry
                .DependsOn<IAccessToken>()
                .DependsOn<IPlayerId>()
                .DependsOn<IActionScheduler>()
                .DependsOn<IUnityThreadUtils>()
                .DependsOn<IMetricsFactory>()
                .DependsOn<IProjectConfiguration>()
                .ProvidesComponent<IWire>();
        }

        public async Task Initialize(CoreRegistry registry)
        {
            // threading
            var actionScheduler = registry.GetServiceComponent<IActionScheduler>();
            if (actionScheduler == null)
            {
                throw new MissingComponentException("IActionScheduler component not initialized.");
            }
            var threadUtils = registry.GetServiceComponent<IUnityThreadUtils>();
            if (threadUtils == null)
            {
                throw new MissingComponentException("IUnityThreadUtils component not initialized.");
            }

            // authentication
            var accessTokenWire = registry.GetServiceComponent<IAccessToken>();
            if (accessTokenWire == null)
            {
                throw new MissingComponentException("IAccessToken component not initialized.");
            }
            var playerId = registry.GetServiceComponent<IPlayerId>();
            if (playerId == null)
            {
                throw new MissingComponentException("IPlayerId component not initialized.");
            }

            // telemetry
            var metricsFactory = registry.GetServiceComponent<IMetricsFactory>();
            var metrics = metricsFactory.Create("com.unity.services.wire");
            metrics.SendSumMetric("wire_init", 1);

            // project config
            var projectCfg = registry.GetServiceComponent<IProjectConfiguration>();
            if (projectCfg == null)
            {
                throw new MissingComponentException("IProjectConfiguration component not initialized.");
            }

            var client = new Client(GetConfiguration(accessTokenWire, projectCfg), actionScheduler, metrics, threadUtils);

            playerId.PlayerIdChanged += client.OnIdentityChanged;

            if (!string.IsNullOrEmpty(accessTokenWire.AccessToken))
            {
                await client.ResetAsync(true);
            }

            registry.RegisterServiceComponent<IWire>(client);
        }

        internal Configuration GetConfiguration(IAccessToken token, IProjectConfiguration projectCfg)
        {
            // for backward compatibility, we still check for build flags
            // if previous user was using build flags, it will keep working,
            // but a warning will display so that they know why we selected
            // the cloud environment.

            var wireAddr = "wss://wire.unity3d.com/v2/ws";

            #if WIRE_STAGING

            Logger.LogWarning("You are switching the cloud environment using the build flags " +
                "(WIRE_STAGING, WIRE_TEST). Please consider using the project configuration instead.");
            Logger.Log("Wire will use the STAGING environment.");
            wireAddr = "wss://wire-stg.unity3d.com/v2/ws";

            #elif WIRE_TEST

            Logger.LogWarning("You are switching the cloud environment using the build flags" +
                "(WIRE_STAGING, WIRE_TEST). Please consider using the project configuration instead.");
            Logger.Log("Wire will use the TEST environment.");
            wireAddr = "wss://wire-test.unity3d.com/v2/ws";

            #else

            var cloudEnvironment = projectCfg?.GetString(k_CloudEnvironmentKey);
            if (cloudEnvironment == k_StagingEnvironment)
            {
                Logger.Log("Wire will use the STAGING environment.");
                wireAddr = "wss://wire-stg.unity3d.com/v2/ws";
            }

            #endif

            return new Configuration
            {
                token = token,
                address = wireAddr,
            };
        }
    }
}
