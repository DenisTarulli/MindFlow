using System;
using Mindlow.Shared.Events;

namespace Mindlow.Puzzles
{
    /// <summary>
    /// Coordinates puzzle session lifecycle events without owning puzzle gameplay.
    /// </summary>
    public sealed class PuzzleSessionService
    {
        private readonly MindlowEventBus eventBus;
        private readonly IPuzzleVariantGenerator variantGenerator;

        public PuzzleSessionService(MindlowEventBus eventBus, IPuzzleVariantGenerator variantGenerator)
        {
            this.eventBus = eventBus;
            this.variantGenerator = variantGenerator;
        }

        /// <summary>
        /// Starts a puzzle session and broadcasts its context to interested systems.
        /// </summary>
        public PuzzleSession StartSession(PuzzleDefinition definition, int difficultyLevel)
        {
            PuzzleParameters parameters = variantGenerator.Generate(definition, difficultyLevel);
            PuzzleSession session = new PuzzleSession(
                Guid.NewGuid().ToString("N"),
                definition,
                parameters,
                DateTime.UtcNow.ToString("o"));

            eventBus.Publish(new PuzzleSessionStartedEvent(session));
            return session;
        }

        public void RecordAttempt(string sessionId)
        {
            eventBus.Publish(new PuzzleActionRecordedEvent(sessionId, PuzzleActionType.Attempt));
        }

        public void RecordError(string sessionId)
        {
            eventBus.Publish(new PuzzleActionRecordedEvent(sessionId, PuzzleActionType.Error));
        }

        public void Restart(string sessionId)
        {
            eventBus.Publish(new PuzzleSessionRestartedEvent(sessionId));
        }

        public void Complete(string sessionId)
        {
            eventBus.Publish(new PuzzleSessionCompletedEvent(sessionId));
        }

        public void Abandon(string sessionId)
        {
            eventBus.Publish(new PuzzleSessionAbandonedEvent(sessionId));
        }
    }
}
