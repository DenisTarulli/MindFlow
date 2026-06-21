using System;
using MindFlow.PerformanceEvaluation;
using MindFlow.Shared.Events;
using UnityEngine;

namespace MindFlow.AdaptiveDifficulty
{
    /// <summary>
    /// Maintains the current difficulty and updates it when performance evaluations arrive.
    /// </summary>
    public sealed class AdaptiveDifficultyService : IDisposable
    {
        private readonly MindFlowEventBus eventBus;
        private readonly IDifficultyRuleSet ruleSet;

        public AdaptiveDifficultyService(MindFlowEventBus eventBus, IDifficultyRuleSet ruleSet, int initialDifficulty)
        {
            this.eventBus = eventBus;
            this.ruleSet = ruleSet;
            CurrentDifficulty = Mathf.Clamp(initialDifficulty, 1, 5);
            eventBus.PerformanceEvaluated += HandlePerformanceEvaluated;
        }

        public int CurrentDifficulty { get; private set; }

        public void Dispose()
        {
            eventBus.PerformanceEvaluated -= HandlePerformanceEvaluated;
        }

        private void HandlePerformanceEvaluated(PerformanceEvaluationResult evaluation)
        {
            int previousDifficulty = CurrentDifficulty;
            int nextDifficulty = ruleSet.SelectDifficulty(CurrentDifficulty, evaluation);

            if (nextDifficulty == previousDifficulty)
            {
                return;
            }

            CurrentDifficulty = nextDifficulty;
            eventBus.Publish(new DifficultyChangedEvent(previousDifficulty, nextDifficulty, evaluation.Band.ToString()));
        }
    }
}
