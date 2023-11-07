using System;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.Wire.Protocol.Internal
{
    // Reply should look like this in json:
    // {"id":1,"result":{"client":"42717820-8d47-4af0-96bc-07f2c36cd17d"}}
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    class Reply
    {
        public UInt32 id;
        public Error error;
        public ConnectResult connect;
        public SubscribeResult subscribe;
        public UnsubscribeResult unsubscribe;
        // missing fields that are not implemented/required by Wire
        public Push push;
        public PingResult ping;

        [JsonIgnore]
        public string originalString = "";

        [Preserve]
        public Reply() {}

        internal static Reply PingReply(UInt32 id)
        {
            var reply = new Reply();
            reply.id = id;
            reply.ping = new PingResult();
            return reply;
        }

        internal static Reply ErrorReply(UInt32 id, Error error)
        {
            var reply = new Reply();
            reply.id = id;
            reply.error = error;
            return reply;
        }

        internal static Reply SubscribeReply(UInt32 id, SubscribeResult result)
        {
            var reply = new Reply();
            reply.id = id;
            reply.subscribe = result;
            return reply;
        }

        internal static Reply UnsubscribeReply(UInt32 id)
        {
            var reply = new Reply();
            reply.id = id;
            reply.unsubscribe = new UnsubscribeResult();
            return reply;
        }

        internal static Reply ConnectReply(UInt32 id, ConnectResult result)
        {
            var reply = new Reply();
            reply.id = id;
            reply.connect = result;
            return reply;
        }

        internal static Reply PushReply(Push push)
        {
            var reply = new Reply();
            reply.push = push;
            return reply;
        }

        public static Reply FromJson(byte[] jsonData)
        {
            return FromJson(Encoding.UTF8.GetString(jsonData));
        }

        public static Reply FromJson(string jsonData)
        {
            var reply = JsonConvert.DeserializeObject<Reply>(jsonData);
            reply.originalString = jsonData;
            return reply;
        }

        public byte[] ToJson()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
        }

        public bool HasError()
        {
            return error != null && error.code != 0;
        }
    }
}
