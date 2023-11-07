using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class ClientInfo
    {
        public string user;
        public string client;
        public byte[] conn_info;
        public byte[] chan_info;

        [Preserve]
        public ClientInfo() {}
    }
}
