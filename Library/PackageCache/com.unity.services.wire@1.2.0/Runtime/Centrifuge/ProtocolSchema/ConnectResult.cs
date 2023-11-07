using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class ConnectResult
    {
        public string client;
        public string version;
        public bool expires;
        public UInt32 ttl;
        public string data;
        public UInt32 ping;
        public bool pong;
        public Dictionary<string, SubscribeResult> subs;

        [Preserve]
        public ConnectResult()
        {
            subs = new Dictionary<string, SubscribeResult>();
        }
    }
}
