namespace MindFlow.Puzzles
{
    /// <summary>
    /// Builds puzzle parameters for a specific definition and difficulty level.
    /// </summary>
    public interface IPuzzleVariantGenerator
    {
        PuzzleParameters Generate(PuzzleDefinition definition, int difficultyLevel);
    }
}
