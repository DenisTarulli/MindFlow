namespace MindFlow.AdaptiveDifficulty
{
    /// <summary>
    /// Event emitted when adaptation changes the active difficulty level.
    /// </summary>
    public readonly struct DifficultyChangedEvent
    {
        public DifficultyChangedEvent(int previousDifficulty, int newDifficulty, string reason)
        {
            PreviousDifficulty = previousDifficulty;
            NewDifficulty = newDifficulty;
            Reason = reason;
        }

        public int PreviousDifficulty { get; }
        public int NewDifficulty { get; }
        public string Reason { get; }
    }
}
