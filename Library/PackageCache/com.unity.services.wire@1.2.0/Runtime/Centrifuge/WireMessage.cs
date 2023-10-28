using UnityEngine.Scripting;

namespace Unity.Services.Wire.Internal
{
    // WireMessage is the Wire specific message envelope used in the centrifuge publication data payload
    class WireMessage
    {
        [Preserve]
        public WireMessage()
        {
        }

        public string payload;
        public string version;
    }
}
