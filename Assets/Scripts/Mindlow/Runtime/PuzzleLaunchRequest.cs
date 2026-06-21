using Mindlow.Puzzles;
using UnityEngine;

namespace Mindlow.Runtime
{
    /// <summary>
    /// Example launcher that starts a configured puzzle definition through the runtime services.
    /// </summary>
    public sealed class PuzzleLaunchRequest : MonoBehaviour
    {
        [SerializeField] private MindlowResearchRuntime runtime;
        [SerializeField] private PuzzleDefinition puzzleDefinition;
        [SerializeField] private PuzzleBehaviour puzzleBehaviour;

        private void Start()
        {
            if (runtime == null || puzzleDefinition == null)
            {
                Debug.LogError("PuzzleLaunchRequest requires a runtime and a puzzle definition.", this);
                enabled = false;
                return;
            }

            PuzzleSession session = runtime.PuzzleSessions.StartSession(puzzleDefinition, runtime.CurrentDifficulty);
            if (puzzleBehaviour != null)
            {
                puzzleBehaviour.Initialize(session, runtime.PuzzleSessions);
            }
        }
    }
}
