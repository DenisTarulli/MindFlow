using Mindlow.Metrics;
using UnityEngine;

namespace Mindlow.PerformanceEvaluation
{
    /// <summary>
    /// Deterministic evaluator that scores performance using configurable weighted penalties.
    /// </summary>
    public sealed class WeightedPerformanceEvaluator : IPerformanceEvaluator
    {
        private readonly PerformanceEvaluationSettings settings;

        public WeightedPerformanceEvaluator(PerformanceEvaluationSettings settings)
        {
            this.settings = settings;
        }

        public PerformanceEvaluationResult Evaluate(PuzzleMetricsRecord record)
        {
            float timeRatio = SafeRatio(record.resolutionTimeSeconds, settings.TargetResolutionTimeSeconds);
            float attemptRatio = SafeRatio(record.attempts, settings.ExpectedAttempts);
            float errorRatio = SafeRatio(record.errors, settings.ExpectedErrors);

            float penalty =
                Mathf.Clamp01(timeRatio - 1f) * settings.TimeWeight +
                Mathf.Clamp01(attemptRatio - 1f) * settings.AttemptWeight +
                Mathf.Clamp01(errorRatio - 1f) * settings.ErrorWeight +
                record.restarts * settings.RestartPenalty +
                (record.abandoned ? settings.AbandonmentPenalty : 0f);

            float score = Mathf.Clamp01(1f - penalty);
            return new PerformanceEvaluationResult(record.sessionId, score, ToBand(score));
        }

        private static float SafeRatio(float value, float target)
        {
            return target <= 0f ? 1f : value / target;
        }

        private static PerformanceBand ToBand(float score)
        {
            if (score < 0.45f)
            {
                return PerformanceBand.Struggling;
            }

            if (score > 0.75f)
            {
                return PerformanceBand.Excelling;
            }

            return PerformanceBand.Stable;
        }
    }
}
