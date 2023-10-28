using System;
using System.Text;
using System.Threading.Tasks;

using Unity.Services.Core;
using Unity.Services.Core.Threading.Internal;
using Unity.Services.Wire.Protocol.Internal;

namespace Unity.Services.Wire.Internal
{
    /// <summary>
    /// Subscription represents a subscription to a channel
    /// </summary>
    /// <typeparam name="TPayload"> The TPayload class representation of the payloads sent to your channel</typeparam>
    class Subscription : IChannel
    {
        public event Action<string> MessageReceived;
        public event Action<byte[]> BinaryMessageReceived;
        public event Action KickReceived;
        public event Action<SubscriptionState> NewStateReceived;
        public event Action<TaskCompletionSource<bool>> UnsubscribeReceived;
        public event Action<TaskCompletionSource<bool>> SubscribeReceived;
        public event Action<string> ErrorReceived;
        public event Action DisposeReceived;

        public string Channel;
        public bool IsConnected => SubscriptionState == SubscriptionState.Synced;

        public UInt64 Offset;
        public string Epoch;

        SubscriptionState m_State = SubscriptionState.Unsynced;
        public SubscriptionState SubscriptionState { get => m_State; private set => SetState(value); }

        IChannelTokenProvider m_TokenProvider;

        bool m_Disposed;

        string ChannelDisplay => string.IsNullOrEmpty(Channel) ? "unknown" : Channel;

        public Subscription(IChannelTokenProvider tokenProvider)
        {
            m_TokenProvider = tokenProvider;
            Offset = 0;
            m_Disposed = false;
        }

        public async Task<string> RetrieveTokenAsync()
        {
            ChannelToken tokenData;
            try
            {
                tokenData = await m_TokenProvider.GetTokenAsync();
            }
            catch (Exception e)
            {
                throw new RequestFailedException((int)WireErrorCode.TokenRetrieverFailed,
                    "Exception caught while running the token retriever.", e);
            }

            ValidateTokenData(tokenData.ChannelName, tokenData.Token);
            Channel = tokenData.ChannelName;
            return tokenData.Token;
        }

        internal void SetState(SubscriptionState state)
        {
            if (m_State != state)
            {
                m_State = state;
                NewStateReceived?.Invoke(m_State);
            }
        }

        private void ValidateTokenData(string channel, string token)
        {
            if (string.IsNullOrEmpty(channel))
            {
                throw new EmptyChannelException();
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new EmptyTokenException();
            }

            if (!string.IsNullOrEmpty(Channel) && Channel != channel)
            {
                throw new ChannelChangedException(channel, Channel);
            }
        }

        internal void ProcessPublication(Publication publication)
        {
            try
            {
                MessageReceived?.Invoke(publication.data.payload);
                BinaryMessageReceived?.Invoke(Encoding.UTF8.GetBytes(publication.data.payload));
            }
            finally
            {
                Offset = publication.offset;
            }
        }

        internal void OnUnsubscriptionComplete()
        {
            SubscriptionState = SubscriptionState.Unsubscribed;
        }

        public void OnKickReceived()
        {
            SubscriptionState = SubscriptionState.Unsubscribed;
            KickReceived?.Invoke();
        }

        public void OnConnectivityChangeReceived(bool connected)
        {
            SubscriptionState = connected ? SubscriptionState.Synced : SubscriptionState.Unsynced;
        }

        ~Subscription()
        {
            // Do nothing
        }

        public void Dispose()
        {
            Dispose(true);
        }

        // Deterministic calls to Dispose will trigger an unsubscribe.
        // Non deterministic calls to Dispose won't.
        internal void Dispose(bool disposing)
        {
            if (m_Disposed)
            {
                return;
            }

            m_Disposed = true;

            try
            {
                // we want to actually unsubscribe when Dispose is called in a deterministic way:
                if (disposing)
                {
                    UnsubscribeReceived?.Invoke(new TaskCompletionSource<bool>());
                }
                // when called from a finalizer, we only get rid of the memory
                else
                {
                    DisposeReceived?.Invoke();
                }
            }
            catch (Exception e)
            {
                ErrorReceived?.Invoke($"Exception raised during disposal of the Channel: ${e}");
            }

            m_TokenProvider = null;

            // cleaning up event listeners
            DisposeReceived = null;
            UnsubscribeReceived = null;
            MessageReceived = null;
            BinaryMessageReceived = null;
            NewStateReceived = null;
            KickReceived = null;
            SubscribeReceived = null;
            ErrorReceived = null;
        }

        public Task SubscribeAsync()
        {
            if (m_Disposed)
            {
                throw new ObjectDisposedException(ChannelDisplay);
            }
            Logger.Log($"Subscribing to {ChannelDisplay}");
            SubscriptionState = SubscriptionState.Subscribing;
            var completionSource = new TaskCompletionSource<bool>();
            SubscribeReceived?.Invoke(completionSource);
            return completionSource.Task;
        }

        public Task UnsubscribeAsync()
        {
            if (m_Disposed)
            {
                throw new ObjectDisposedException(ChannelDisplay);
            }
            Logger.Log($"Unsubscribing from {ChannelDisplay}");
            var completionSource = new TaskCompletionSource<bool>();
            UnsubscribeReceived?.Invoke(completionSource);
            return completionSource.Task;
        }

        internal void OnError(string reason)
        {
            SubscriptionState = SubscriptionState.Error;
            ErrorReceived?.Invoke(reason);
        }
    }
}
