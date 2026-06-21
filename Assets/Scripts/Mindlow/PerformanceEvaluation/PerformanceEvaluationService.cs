using System;
using Mindlow.Metrics;
using Mindlow.Shared.Events;

namespace Mindlow.PerformanceEvaluation
{
    /// <summary>
    /// Listens for completed metrics records and publishes performance evaluations.
    /// </summary>
    public sealed class PerformanceEvaluationService : IDisposable
    {
        private readonly MindlowEventBus eventBus;
        private readonly IPerformanceEvaluator evaluator;

        public PerformanceEvaluationService(MindlowEventBus eventBus, IPerformanceEvaluator evaluator)
        {
            this.eventBus = eventBus;
            this.evaluator = evaluator;
            eventBus.MetricsRecorded += HandleMetricsRecorded;
        }

        public void Dispose()
        {
            eventBus.MetricsRecorded -= HandleMetricsRecorded;
        }

        private void HandleMetricsRecorded(PuzzleMetricsRecord record)
        {
            eventBus.Publish(evaluator.Evaluate(record));
        }
    }
}
