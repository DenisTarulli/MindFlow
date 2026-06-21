using System;

namespace MindFlow.Puzzles
{
    /// <summary>
    /// Serializable parameters that describe a generated puzzle variant.
    /// </summary>
    [Serializable]
    public sealed class PuzzleParameters
    {
        public int difficultyLevel = 1;
        public int complexity = 1;
        public int variantSeed;
        public string variantLabel = "Default";
    }
}
