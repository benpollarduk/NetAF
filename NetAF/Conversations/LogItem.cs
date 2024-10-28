namespace NetAF.Conversations
{
    /// <summary>
    /// Provides a container for log items.
    /// </summary>
    /// <param name="participant">The participant.</param>
    /// <param name="line">The line.</param>
    public sealed class LogItem(Participant participant, string line)
    {
        #region Properties

        /// <summary>
        /// Get the participant.
        /// </summary>
        public Participant Participant { get; } = participant;

        /// <summary>
        /// Get the line.
        /// </summary>
        public string Line { get; } = line;

        #endregion
    }
}