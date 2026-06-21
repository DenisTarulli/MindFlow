namespace Mindlow.Puzzles
{
    /// <summary>
    /// Event emitted when a puzzle session starts.
    /// </summary>
    public readonly struct PuzzleSessionStartedEvent
    {
        public PuzzleSessionStartedEvent(PuzzleSession session)
        {
            Session = session;
        }

        public PuzzleSession Session { get; }
    }

    /// <summary>
    /// Event emitted when a puzzle session is solved.
    /// </summary>
    public readonly struct PuzzleSessionCompletedEvent
    {
        public PuzzleSessionCompletedEvent(string sessionId)
        {
            SessionId = sessionId;
        }

        public string SessionId { get; }
    }

    /// <summary>
    /// Event emitted when a puzzle session is abandoned.
    /// </summary>
    public readonly struct PuzzleSessionAbandonedEvent
    {
        public PuzzleSessionAbandonedEvent(string sessionId)
        {
            SessionId = sessionId;
        }

        public string SessionId { get; }
    }

    /// <summary>
    /// Event emitted when a puzzle session is restarted.
    /// </summary>
    public readonly struct PuzzleSessionRestartedEvent
    {
        public PuzzleSessionRestartedEvent(string sessionId)
        {
            SessionId = sessionId;
        }

        public string SessionId { get; }
    }

    /// <summary>
    /// Event emitted for atomic puzzle actions that contribute to metrics.
    /// </summary>
    public readonly struct PuzzleActionRecordedEvent
    {
        public PuzzleActionRecordedEvent(string sessionId, PuzzleActionType actionType)
        {
            SessionId = sessionId;
            ActionType = actionType;
        }

        public string SessionId { get; }
        public PuzzleActionType ActionType { get; }
    }
}
