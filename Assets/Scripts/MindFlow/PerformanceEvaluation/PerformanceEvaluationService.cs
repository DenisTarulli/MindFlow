using System;
using MindFlow.Metrics;
using MindFlow.Shared.Events;

namespace MindFlow.PerformanceEvaluation
{
    /// <summary>
    /// Listens for completed metrics records and publishes performance evaluations.
    /// </summary>
    public sealed class PerformanceEvaluationService : IDisposable
    {
        private readonly MindFlowEventBus eventBus;
        private readonly IPerformanceEvaluator evaluator;

        public PerformanceEvaluationService(MindFlowEventBus eventBus, IPerformanceEvaluator evaluator)
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
