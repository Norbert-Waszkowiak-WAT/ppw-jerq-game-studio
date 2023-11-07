using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class SubscribeResult
    {
        public bool expires;
        public UInt32 ttl;
        public bool recoverable;
        public string epoch;
        public bool recovered;
        public ulong offset;
        public bool positioned;
        public string data;
        public Publication[] publications;

        [Preserve]
        public SubscribeResult()
        {
            publications = new Publication[] {};
        }
    }
}
