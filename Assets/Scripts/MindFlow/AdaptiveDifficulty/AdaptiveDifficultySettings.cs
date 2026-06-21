using UnityEngine;

namespace MindFlow.AdaptiveDifficulty
{
    /// <summary>
    /// Tunable thresholds and bounds for the adaptive difficulty system.
    /// </summary>
    [CreateAssetMenu(menuName = "MindFlow/Adaptive Difficulty/Settings", fileName = "AdaptiveDifficultySettings")]
    public sealed class AdaptiveDifficultySettings : ScriptableObject
    {
        [SerializeField, Range(1, 5)] private int initialDifficulty = 3;
        [SerializeField, Range(1, 5)] private int minimumDifficulty = 1;
        [SerializeField, Range(1, 5)] private int maximumDifficulty = 5;
        [SerializeField, Range(0f, 1f)] private float increaseThreshold = 0.75f;
        [SerializeField, Range(0f, 1f)] private float decreaseThreshold = 0.45f;

        public int InitialDifficulty => initialDifficulty;
        public int MinimumDifficulty => minimumDifficulty;
        public int MaximumDifficulty => maximumDifficulty;
        public float IncreaseThreshold => increaseThreshold;
        public float DecreaseThreshold => decreaseThreshold;
    }
}
