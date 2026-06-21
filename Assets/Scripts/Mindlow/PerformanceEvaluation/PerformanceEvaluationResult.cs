namespace Mindlow.PerformanceEvaluation
{
    /// <summary>
    /// Rule-based estimate of player performance for a finished puzzle session.
    /// </summary>
    public readonly struct PerformanceEvaluationResult
    {
        public PerformanceEvaluationResult(string sessionId, float score, PerformanceBand band)
        {
            SessionId = sessionId;
            Score = score;
            Band = band;
        }

        public string SessionId { get; }
        public float Score { get; }
        public PerformanceBand Band { get; }
    }

    /// <summary>
    /// Coarse performance category used by adaptation rules.
    /// </summary>
    public enum PerformanceBand
    {
        Struggling,
        Stable,
        Excelling
    }
}
