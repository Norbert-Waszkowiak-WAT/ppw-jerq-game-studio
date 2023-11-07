using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class Unsubscribe
    {
        public UInt32 code;
        public string reason;

        [Preserve]
        public Unsubscribe() {}
    }
}
