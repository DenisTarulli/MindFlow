using Mindlow.PerformanceEvaluation;
using UnityEngine;

namespace Mindlow.AdaptiveDifficulty
{
    /// <summary>
    /// Simple rule set that increases or decreases difficulty using score thresholds.
    /// </summary>
    public sealed class ThresholdDifficultyRuleSet : IDifficultyRuleSet
    {
        private readonly AdaptiveDifficultySettings settings;

        public ThresholdDifficultyRuleSet(AdaptiveDifficultySettings settings)
        {
            this.settings = settings;
        }

        public int SelectDifficulty(int currentDifficulty, PerformanceEvaluationResult evaluation)
        {
            int nextDifficulty = currentDifficulty;

            if (evaluation.Score >= settings.IncreaseThreshold)
            {
                nextDifficulty++;
            }
            else if (evaluation.Score <= settings.DecreaseThreshold)
            {
                nextDifficulty--;
            }

            return Mathf.Clamp(nextDifficulty, settings.MinimumDifficulty, settings.MaximumDifficulty);
        }
    }
}
