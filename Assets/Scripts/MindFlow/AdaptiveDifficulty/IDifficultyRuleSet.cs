using MindFlow.PerformanceEvaluation;

namespace MindFlow.AdaptiveDifficulty
{
    /// <summary>
    /// Determines the next difficulty level from the current level and latest performance result.
    /// </summary>
    public interface IDifficultyRuleSet
    {
        int SelectDifficulty(int currentDifficulty, PerformanceEvaluationResult evaluation);
    }
}
