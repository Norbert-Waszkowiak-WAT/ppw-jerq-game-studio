using System;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using Logger = Unity.Services.Wire.Internal.Logger;

namespace Unity.Services.Wire.Protocol.Internal
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class Command
    {
        public UInt32 id;
        public ConnectRequest connect;
        public SubscribeRequest subscribe;
        public UnsubscribeRequest unsubscribe;
        // missing requests here (not implemented/necessary for Wire)
        public PingRequest ping;

        [Preserve]
        public Command() {}
        public Command(PingRequest request)
        {
            id = CommandID.GenerateNewId();
            ping = request;
        }

        public Command(ConnectRequest request)
        {
            id = CommandID.GenerateNewId();
            connect = request;
        }

        public Command(SubscribeRequest request)
        {
            id = CommandID.GenerateNewId();
            subscribe = request;
        }

        public Command(UnsubscribeRequest request)
        {
            id = CommandID.GenerateNewId();
            unsubscribe = request;
        }

        public static Command FromJSON(byte[] data)
        {
            return JsonConvert.DeserializeObject<Command>(Encoding.UTF8.GetString(data));
        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this, Formatting.None));
        }

        public new string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        internal bool IsPing()
        {
            return ping != null;
        }

        public string GetMethod()
        {
            if (connect != null)
            {
                return "CONNECT";
            }

            if (subscribe != null)
            {
                return "SUBSCRIBE";
            }

            if (unsubscribe != null)
            {
                return "UNSUBSCRIBE";
            }

            if (ping != null)
            {
                return "PING";
            }

            return "UNKNOWN";
        }
    }
}
