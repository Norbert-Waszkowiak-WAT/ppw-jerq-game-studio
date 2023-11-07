using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Services.Core.Telemetry.Internal;

namespace Unity.Services.Lobbies.Http
{
    class ApiTelemetryScopeFactory
    {
        private readonly IMetrics m_Metrics;

        public ApiTelemetryScopeFactory(IMetrics metrics)
        {
            m_Metrics = metrics;
        }

        public ApiTelemetryScope Instrument(string api)
        {
            return new ApiTelemetryScope(m_Metrics, api);
        }
    }


    sealed class ApiTelemetryScope : IDisposable
    {
        const string k_RequestLatencyMetric = "http_request_ms";
        const string k_ApiTag = "api";

        private readonly IMetrics m_Metrics;
        private readonly Dictionary<string, string> m_Tags;
        private readonly Stopwatch m_Stopwatch;

        private bool m_Disposed;

        public ApiTelemetryScope(IMetrics metrics, string api)
        {
            m_Metrics = metrics;
            m_Tags = new Dictionary<string, string> {{k_ApiTag, api}};
            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (m_Disposed) return;

            m_Disposed = true;

            if (disposing)
            {
                m_Stopwatch.Stop();
                m_Metrics.SendHistogramMetric(k_RequestLatencyMetric, m_Stopwatch.ElapsedMilliseconds, m_Tags);
            }
        }
    }
}
