namespace Unity.Services.Lobbies
{
    /// <summary>
    /// An enum providing the change type of a given change.
    /// </summary>
    public enum LobbyValueChangeType : byte
    {
        /// <summary>
        /// No changes have occurred.
        /// </summary>
        Unchanged = 0,

        /// <summary>
        /// The value has been changed.
        /// </summary>
        Changed = 1,

        /// <summary>
        /// The value has been removed.
        /// </summary>
        Removed = 2,

        /// <summary>
        /// The value has been added.
        /// </summary>
        Added = 3
    }

    /// <summary>
    /// Contains whether or not a particular change or removal has occurred. If a change has occurred, also provides the value of the change.
    /// </summary>
    /// <typeparam name="T">The type of the value of the change.</typeparam>
    public struct ChangedOrRemovedLobbyValue<T>
    {
        /// <summary>
        /// A helper for providing a removal of the value without having to call a constructor.
        /// </summary>
        public static readonly ChangedOrRemovedLobbyValue<T> RemoveThisValue = new ChangedOrRemovedLobbyValue<T>(default, LobbyValueChangeType.Removed);

        /// <summary>
        /// The new value provided by the change.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// True if the value has been removed, false if it hasn't been removed.
        /// Removed properties are mutually exclusive with Changed and Added properties.
        /// </summary>
        public bool Removed { get => ChangeType == LobbyValueChangeType.Removed; }

        /// <summary>
        /// True if a change has occurred, false if there has been no change.
        /// Removed and Changed properties are mutually exclusive.
        /// </summary>
        public bool Changed { get => ChangeType == LobbyValueChangeType.Changed || ChangeType == LobbyValueChangeType.Added; }

        /// <summary>
        /// True if the value was newly added, false otherwise.
        /// Removed and Added properties are mutually exclusive.
        /// </summary>
        public bool Added { get => ChangeType == LobbyValueChangeType.Added; }

        /// <summary>
        /// Whether this is a change, a removal, or an addition.
        /// </summary>
        public LobbyValueChangeType ChangeType { get; }

        /// <summary>
        /// Creates a changed or removed value.
        /// </summary>
        /// <param name="value">The new value provided by the change.</param>
        /// <param name="status">The status of this change.</param>
        public ChangedOrRemovedLobbyValue(T value, LobbyValueChangeType status)
        {
            Value = value;
            ChangeType = status;
        }
    }
}
