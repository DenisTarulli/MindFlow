using System;

namespace MindFlow.Metrics
{
    /// <summary>
    /// Serializable research record produced after a puzzle session ends.
    /// </summary>
    [Serializable]
    public sealed class PuzzleMetricsRecord
    {
        public string sessionId;
        public string puzzleId;
        public string puzzleType;
        public int difficultyLevel;
        public int complexity;
        public int variantSeed;
        public string variantLabel;
        public float resolutionTimeSeconds;
        public int attempts;
        public int errors;
        public int restarts;
        public bool abandoned;
        public bool completed;
        public string startedAtUtc;
        public string endedAtUtc;
    }
}
