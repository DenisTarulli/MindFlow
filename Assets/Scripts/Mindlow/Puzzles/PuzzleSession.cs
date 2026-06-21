using System;

namespace Mindlow.Puzzles
{
    /// <summary>
    /// Immutable data that identifies one puzzle play session.
    /// </summary>
    [Serializable]
    public sealed class PuzzleSession
    {
        public string sessionId;
        public string puzzleId;
        public string puzzleType;
        public int difficultyLevel;
        public int complexity;
        public int variantSeed;
        public string variantLabel;
        public string startedAtUtc;

        public PuzzleSession(
            string sessionId,
            PuzzleDefinition definition,
            PuzzleParameters parameters,
            string startedAtUtc)
        {
            this.sessionId = sessionId;
            puzzleId = definition.PuzzleId;
            puzzleType = definition.PuzzleType;
            difficultyLevel = parameters.difficultyLevel;
            complexity = parameters.complexity;
            variantSeed = parameters.variantSeed;
            variantLabel = parameters.variantLabel;
            this.startedAtUtc = startedAtUtc;
        }
    }
}
