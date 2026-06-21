namespace MindFlow.Puzzles
{
    /// <summary>
    /// Minimal contract for concrete puzzle implementations managed by the puzzle system.
    /// </summary>
    public interface IPuzzle
    {
        void Initialize(PuzzleSession session, PuzzleSessionService sessionService);
    }
}
