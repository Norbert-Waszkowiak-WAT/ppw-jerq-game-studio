using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    // Push can be sent to a client as part of Reply in case of bidirectional transport or
    // without additional wrapping in case of unidirectional transports.
    // ProtocolVersion2 uses channel and one of the possible concrete push messages.
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class Push
    {
        public string channel;
        public Publication pub;
        public Unsubscribe unsubscribe;
        // other types not supported/needed by Wire

        [Preserve]
        public Push() {}

        internal string GetPushType()
        {
            if (IsPub())
            {
                return "PUB";
            }

            if (IsUnsub())
            {
                return "UNSUB";
            }

            return "UNKNOWN";
        }

        internal bool IsUnsub()
        {
            return unsubscribe != null;
        }

        internal bool IsPub()
        {
            return pub != null;
        }
    }
}
