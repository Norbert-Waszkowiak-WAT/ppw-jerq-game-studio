using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class Error
    {
        public CentrifugeErrorCode code;
        public string message;

        [Preserve]
        public Error() {}
    }
}
