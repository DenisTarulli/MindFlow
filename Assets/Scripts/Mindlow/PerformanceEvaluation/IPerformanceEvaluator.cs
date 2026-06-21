using Mindlow.Metrics;

namespace Mindlow.PerformanceEvaluation
{
    /// <summary>
    /// Evaluates a metrics record and produces a normalized performance result.
    /// </summary>
    public interface IPerformanceEvaluator
    {
        PerformanceEvaluationResult Evaluate(PuzzleMetricsRecord record);
    }
}
