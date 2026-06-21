using System;
using Mindlow.AdaptiveDifficulty;
using Mindlow.Metrics;
using Mindlow.PerformanceEvaluation;
using Mindlow.Puzzles;

namespace Mindlow.Shared.Events
{
    /// <summary>
    /// Local event hub used to decouple research systems without relying on global singletons.
    /// </summary>
    public sealed class MindlowEventBus
    {
        public event Action<PuzzleSessionStartedEvent> PuzzleSessionStarted;
        public event Action<PuzzleSessionCompletedEvent> PuzzleSessionCompleted;
        public event Action<PuzzleSessionAbandonedEvent> PuzzleSessionAbandoned;
        public event Action<PuzzleSessionRestartedEvent> PuzzleSessionRestarted;
        public event Action<PuzzleActionRecordedEvent> PuzzleActionRecorded;
        public event Action<PuzzleMetricsRecord> MetricsRecorded;
        public event Action<PerformanceEvaluationResult> PerformanceEvaluated;
        public event Action<DifficultyChangedEvent> DifficultyChanged;

        public void Publish(PuzzleSessionStartedEvent eventData)
        {
            PuzzleSessionStarted?.Invoke(eventData);
        }

        public void Publish(PuzzleSessionCompletedEvent eventData)
        {
            PuzzleSessionCompleted?.Invoke(eventData);
        }

        public void Publish(PuzzleSessionAbandonedEvent eventData)
        {
            PuzzleSessionAbandoned?.Invoke(eventData);
        }

        public void Publish(PuzzleSessionRestartedEvent eventData)
        {
            PuzzleSessionRestarted?.Invoke(eventData);
        }

        public void Publish(PuzzleActionRecordedEvent eventData)
        {
            PuzzleActionRecorded?.Invoke(eventData);
        }

        public void Publish(PuzzleMetricsRecord record)
        {
            MetricsRecorded?.Invoke(record);
        }

        public void Publish(PerformanceEvaluationResult result)
        {
            PerformanceEvaluated?.Invoke(result);
        }

        public void Publish(DifficultyChangedEvent eventData)
        {
            DifficultyChanged?.Invoke(eventData);
        }
    }
}
