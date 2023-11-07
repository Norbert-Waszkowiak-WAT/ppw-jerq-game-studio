using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace Unity.Services.Wire.Internal
{
    static class BatchMessagesUtil
    {
        static IEnumerable<string> SplitMessages(string message)
        {
            var publications = message.Split(new string[] { "}\n{" }, StringSplitOptions.None);

            if (publications.Length > 1)
            {
                // String.Split removes the delimiter, we need to add the curly brackets back to parse json properly.
                FixJsonSplit(ref publications);
            }

            return publications;
        }

        // SplitMessages returns the list of messages received in the websocket buffer
        // sometimes, centrifuge can batch multiple messages into one single buffer, separated by a new line
        // ex:[Wire]: WS received message: {"result":{"channel":"$example!!!test","data":{"data":{"message":"test 6"},"offset":248}}}
        // {"result":{"channel":"$example!!!test","data":{"data":{"message":"test 1"},"offset":249}}}
        public static IEnumerable<string> SplitMessages(byte[] byteMessage)
        {
            // we have to use the }\n{ delimiter as the messages themselves could contains the \n symbol.
            return SplitMessages(Encoding.UTF8.GetString(byteMessage));
        }

        // FixJsonSplit fixes each individual messages created by splitMessages
        // splitMessages will split the messages and remove curly brackets at the beginning and enf of messages after split
        // ex: {"result":{"channel":"$example!!!test","data":{"data":{"message":"test 0"},"offset":248}}}
        // {"result":{"channel":"$example!!!test","data":{"data":{"message":"test 1"},"offset":249}}}
        // {"result":{"channel":"$example!!!test","data":{"data":{"message":"test 2"},"offset":250}}}
        // will become
        // [0] :  {"result":{"channel":"$example!!!test","data":{"data":{"message":"test 0"},"offset":248}}
        // [1] : "result":{"channel":"$example!!!test","data":{"data":{"message":"test 1"},"offset":249}}
        // [2] : "result":{"channel":"$example!!!test","data":{"data":{"message":"test 2"},"offset":250}}}
        static void FixJsonSplit(ref string[] pubs)
        {
            for (var i = 0; i < pubs.Length; i++)
            {
                if (i > 0) // first message didn't match the ending regex, therefore it doesn't miss the opening {
                {
                    pubs[i] = "{" + pubs[i];
                }

                if (i < pubs.Length - 1) // the last message doesn't match the regex neither, so no need to add the closing }
                {
                    pubs[i] += "}";
                }
            }
        }
    }
}
