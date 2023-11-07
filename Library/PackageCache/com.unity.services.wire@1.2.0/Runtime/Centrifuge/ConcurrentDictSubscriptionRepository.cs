using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.Services.Wire.Protocol.Internal;

namespace Unity.Services.Wire.Internal
{
    interface ISubscriptionRepository
    {
        event Action<int> SubscriptionCountChanged;
        bool IsAlreadySubscribed(Subscription sub);
        bool IsRecovering(Subscription sub);

        void OnSubscriptionComplete(Subscription sub, SubscribeResult result);
        Subscription GetSub(Subscription sub);
        Subscription GetSub(string channel);

        IEnumerable<KeyValuePair<string, Subscription>> GetAll();
        void RemoveSub(Subscription sub);

        void OnSocketClosed();
        void RecoverSubscriptions(Reply reply);
        bool IsEmpty { get; }

        void Clear();
    }

    class ConcurrentDictSubscriptionRepository : ISubscriptionRepository
    {
        public ConcurrentDictionary<string, Subscription> Subscriptions;

        public bool IsEmpty => Subscriptions.IsEmpty;

        public event Action<int> SubscriptionCountChanged;

        public ConcurrentDictSubscriptionRepository()
        {
            Subscriptions = new ConcurrentDictionary<string, Subscription>();
        }

        public void Clear()
        {
            Subscriptions.Clear();
        }

        public bool IsAlreadySubscribed(string alias)
        {
            return GetSub(alias)?.IsConnected ?? false;
        }

        public bool IsAlreadySubscribed(Subscription sub)
        {
            return IsAlreadySubscribed(sub.Channel);
        }

        public bool IsRecovering(Subscription sub)
        {
            if (string.IsNullOrEmpty(sub.Channel))
            {
                return false;
            }
            return Subscriptions.ContainsKey(sub.Channel) && !sub.IsConnected;
        }

        public void OnSubscriptionComplete(Subscription sub, SubscribeResult res)
        {
            if (res.offset != sub.Offset)
            {
                try
                {
                    foreach (var publication in res.publications)
                    {
                        sub.ProcessPublication(publication);
                    }
                    sub.Offset = res.offset;
                }
                catch (Exception e)
                {
                    Logger.LogException(e);
                }
            }

            var recovering = IsRecovering(sub);
            sub.OnConnectivityChangeReceived(true);

            if (!recovering)
            {
                Subscriptions.TryAdd(sub.Channel, sub);
                SubscriptionCountChanged?.Invoke(Subscriptions.Count);
            }
        }

        public Subscription GetSub(string channel)
        {
            if (string.IsNullOrEmpty(channel))
            {
                return null;
            }

            if (Subscriptions.ContainsKey(channel))
            {
                Subscriptions.TryGetValue(channel, out var sub);
                return sub;
            }

            return null;
        }

        public Subscription GetSub(Subscription sub)
        {
            return GetSub(sub.Channel);
        }

        public void RemoveSub(Subscription sub)
        {
            if (string.IsNullOrEmpty(sub.Channel))
            {
                return;
            }
            if (Subscriptions.ContainsKey(sub.Channel))
            {
                Subscriptions.TryRemove(sub.Channel, out _);
                sub.OnUnsubscriptionComplete();
                SubscriptionCountChanged?.Invoke(Subscriptions.Count);
            }
        }

        public void OnSocketClosed()
        {
            foreach (var iterator in Subscriptions)
            {
                iterator.Value.OnConnectivityChangeReceived(false);
            }
        }

        // recovering subscriptions after a reconnect
        // (subscriptions that are made through the connection command)
        public void RecoverSubscriptions(Reply reply)
        {
            var res = reply.connect;
            if (res.subs?.Count > 0)
            {
                foreach (var subIterator in res.subs)
                {
                    var sub = GetSub(subIterator.Key);
                    if (sub == null)
                    {
                        Logger.LogWarning($"Receiving a subscription result from an untracked subscription: ${subIterator.Key}");
                        continue;
                    }
                    OnSubscriptionComplete(sub, subIterator.Value);
                }
            }
        }

        public IEnumerable<KeyValuePair<string, Subscription>> GetAll()
        {
            return Subscriptions.ToArray();
        }
    }
}
