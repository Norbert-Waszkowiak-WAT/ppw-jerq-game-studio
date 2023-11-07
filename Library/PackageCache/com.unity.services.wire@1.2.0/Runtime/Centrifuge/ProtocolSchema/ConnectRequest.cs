using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class ConnectRequest
    {
        public string token;
        public Dictionary<string, SubscribeRequest> subs;

        [Preserve]
        public ConnectRequest()
        {
            subs = new Dictionary<string, SubscribeRequest>();
        }

        public ConnectRequest(string token) : this()
        {
            this.token = token;
        }

        public ConnectRequest(string token, Dictionary<string, SubscribeRequest> subscriptionRequests)
        {
            subs = subscriptionRequests;
            this.token = token;
        }
    }
}
