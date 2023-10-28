namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Contains whether or not a particular change has occurred, and if it has, the value of the change.
    /// </summary>
    /// <typeparam name="T">The type of the value of the change.</typeparam>
    public struct ChangedLobbyValue<T>
    {
        /// <summary>
        /// The new value provided by the change.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// True if a change has occurred, false if there has been no change.
        /// Changed value may or may not be an added value
        /// </summary>
        public bool Changed { get; }

        /// <summary>
        /// True if the value is new, false otherwise.
        /// An Added value is necessarily a Changed value.
        /// </summary>
        public bool Added { get; internal set; }

        /// <summary>
        /// Creates a changed value.
        /// </summary>
        /// <param name="value">The new value provided by the change.</param>
        public ChangedLobbyValue(T value)
        {
            Value = value;
            Changed = true;
            Added = false;
        }
    }
}
