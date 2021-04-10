namespace Arex388.Carmine {
    /// <summary>
    /// Response status.
    /// </summary>
    public enum ResponseStatus :
        byte {
        /// <summary>
        /// The request was cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The request failed.
        /// </summary>
        Failure,

        /// <summary>
        /// The request succeeded.
        /// </summary>
        Success,

        /// <summary>
        /// The request timed out.
        /// </summary>
        TimeOut
    }
}