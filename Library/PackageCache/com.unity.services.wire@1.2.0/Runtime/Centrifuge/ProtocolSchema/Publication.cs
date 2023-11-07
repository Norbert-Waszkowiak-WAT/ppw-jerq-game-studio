using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Services.Wire.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class Publication
    {
        public WireMessage data;
        public ClientInfo info;
        public ulong offset;
        public Dictionary<string, string> tags;

        [Preserve]
        public Publication() {}
    }
}
