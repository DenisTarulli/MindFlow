using UnityEngine;

namespace Mindlow.Puzzles
{
    /// <summary>
    /// Authoring asset that describes a puzzle family and its default generation parameters.
    /// </summary>
    [CreateAssetMenu(menuName = "Mindlow/Puzzles/Puzzle Definition", fileName = "PuzzleDefinition")]
    public sealed class PuzzleDefinition : ScriptableObject
    {
        [SerializeField] private string puzzleId = "puzzle.default";
        [SerializeField] private string displayName = "Puzzle";
        [SerializeField] private string puzzleType = "Generic";
        [SerializeField] private PuzzleParameters defaultParameters = new PuzzleParameters();

        public string PuzzleId => puzzleId;
        public string DisplayName => displayName;
        public string PuzzleType => puzzleType;
        public PuzzleParameters DefaultParameters => defaultParameters;
    }
}
