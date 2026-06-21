using System;
using System.Collections.Generic;
using MindFlow.Puzzles;
using MindFlow.Shared.Events;
using MindFlow.Shared.Time;

namespace MindFlow.Metrics
{
    /// <summary>
    /// Collects puzzle metrics from events while keeping gameplay code free of research storage details.
    /// </summary>
    public sealed class MetricsCollector : IDisposable
    {
        private readonly MindFlowEventBus eventBus;
        private readonly ITimeProvider timeProvider;
        private readonly Dictionary<string, MutableMetricsSession> activeSessions = new Dictionary<string, MutableMetricsSession>();

        public MetricsCollector(MindFlowEventBus eventBus, ITimeProvider timeProvider)
        {
            this.eventBus = eventBus;
            this.timeProvider = timeProvider;
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            eventBus.PuzzleSessionStarted += HandleSessionStarted;
            eventBus.PuzzleActionRecorded += HandleActionRecorded;
            eventBus.PuzzleSessionRestarted += HandleSessionRestarted;
            eventBus.PuzzleSessionCompleted += HandleSessionCompleted;
            eventBus.PuzzleSessionAbandoned += HandleSessionAbandoned;
        }

        private void Unsubscribe()
        {
            eventBus.PuzzleSessionStarted -= HandleSessionStarted;
            eventBus.PuzzleActionRecorded -= HandleActionRecorded;
            eventBus.PuzzleSessionRestarted -= HandleSessionRestarted;
            eventBus.PuzzleSessionCompleted -= HandleSessionCompleted;
            eventBus.PuzzleSessionAbandoned -= HandleSessionAbandoned;
        }

        private void HandleSessionStarted(PuzzleSessionStartedEvent eventData)
        {
            activeSessions[eventData.Session.sessionId] = new MutableMetricsSession(eventData.Session, timeProvider.RealtimeSinceStartup);
        }

        private void HandleActionRecorded(PuzzleActionRecordedEvent eventData)
        {
            if (!activeSessions.TryGetValue(eventData.SessionId, out MutableMetricsSession metricsSession))
            {
                return;
            }

            if (eventData.ActionType == PuzzleActionType.Attempt)
            {
                metricsSession.Attempts++;
            }
            else if (eventData.ActionType == PuzzleActionType.Error)
            {
                metricsSession.Errors++;
            }
        }

        private void HandleSessionRestarted(PuzzleSessionRestartedEvent eventData)
        {
            if (activeSessions.TryGetValue(eventData.SessionId, out MutableMetricsSession metricsSession))
            {
                metricsSession.Restarts++;
            }
        }

        private void HandleSessionCompleted(PuzzleSessionCompletedEvent eventData)
        {
            FinalizeSession(eventData.SessionId, completed: true, abandoned: false);
        }

        private void HandleSessionAbandoned(PuzzleSessionAbandonedEvent eventData)
        {
            FinalizeSession(eventData.SessionId, completed: false, abandoned: true);
        }

        private void FinalizeSession(string sessionId, bool completed, bool abandoned)
        {
            if (!activeSessions.TryGetValue(sessionId, out MutableMetricsSession metricsSession))
            {
                return;
            }

            activeSessions.Remove(sessionId);
            PuzzleMetricsRecord record = metricsSession.ToRecord(
                timeProvider.RealtimeSinceStartup,
                DateTime.UtcNow.ToString("o"),
                completed,
                abandoned);

            eventBus.Publish(record);
        }

        private sealed class MutableMetricsSession
        {
            private readonly PuzzleSession session;
            private readonly float startedAtSeconds;

            public MutableMetricsSession(PuzzleSession session, float startedAtSeconds)
            {
                this.session = session;
                this.startedAtSeconds = startedAtSeconds;
            }

            public int Attempts { get; set; }
            public int Errors { get; set; }
            public int Restarts { get; set; }

            public PuzzleMetricsRecord ToRecord(float endedAtSeconds, string endedAtUtc, bool completed, bool abandoned)
            {
                return new PuzzleMetricsRecord
                {
                    sessionId = session.sessionId,
                    puzzleId = session.puzzleId,
                    puzzleType = session.puzzleType,
                    difficultyLevel = session.difficultyLevel,
                    complexity = session.complexity,
                    variantSeed = session.variantSeed,
                    variantLabel = session.variantLabel,
                    resolutionTimeSeconds = Math.Max(0f, endedAtSeconds - startedAtSeconds),
                    attempts = Attempts,
                    errors = Errors,
                    restarts = Restarts,
                    abandoned = abandoned,
                    completed = completed,
                    startedAtUtc = session.startedAtUtc,
                    endedAtUtc = endedAtUtc
                };
            }
        }
    }
}
