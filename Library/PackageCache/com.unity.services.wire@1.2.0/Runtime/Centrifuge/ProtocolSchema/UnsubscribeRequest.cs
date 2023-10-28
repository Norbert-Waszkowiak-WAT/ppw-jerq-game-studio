using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class UnsubscribeRequest
    {
        public string channel;

        [Preserve]
        public UnsubscribeRequest() {}
    }
}
