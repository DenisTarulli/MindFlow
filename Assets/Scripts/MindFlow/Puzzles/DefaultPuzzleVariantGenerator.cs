using UnityEngine;

namespace MindFlow.Puzzles
{
    /// <summary>
    /// Conservative variant generator that can be replaced by puzzle-specific generators.
    /// </summary>
    public sealed class DefaultPuzzleVariantGenerator : IPuzzleVariantGenerator
    {
        public PuzzleParameters Generate(PuzzleDefinition definition, int difficultyLevel)
        {
            int clampedDifficulty = Mathf.Clamp(difficultyLevel, 1, 5);
            PuzzleParameters source = definition.DefaultParameters;

            return new PuzzleParameters
            {
                difficultyLevel = clampedDifficulty,
                complexity = Mathf.Max(source.complexity, clampedDifficulty),
                variantSeed = Random.Range(1, int.MaxValue),
                variantLabel = source.variantLabel
            };
        }
    }
}
