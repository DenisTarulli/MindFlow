using UnityEngine;

namespace MindFlow.PerformanceEvaluation
{
    /// <summary>
    /// Tunable weights used by the rule-based performance evaluator.
    /// </summary>
    [CreateAssetMenu(menuName = "MindFlow/Performance/Evaluation Settings", fileName = "PerformanceEvaluationSettings")]
    public sealed class PerformanceEvaluationSettings : ScriptableObject
    {
        [Header("Targets")]
        [SerializeField] private float targetResolutionTimeSeconds = 60f;
        [SerializeField] private int expectedAttempts = 3;
        [SerializeField] private int expectedErrors = 2;

        [Header("Penalties")]
        [SerializeField] private float timeWeight = 0.35f;
        [SerializeField] private float attemptWeight = 0.25f;
        [SerializeField] private float errorWeight = 0.25f;
        [SerializeField] private float restartPenalty = 0.1f;
        [SerializeField] private float abandonmentPenalty = 0.5f;

        public float TargetResolutionTimeSeconds => targetResolutionTimeSeconds;
        public int ExpectedAttempts => expectedAttempts;
        public int ExpectedErrors => expectedErrors;
        public float TimeWeight => timeWeight;
        public float AttemptWeight => attemptWeight;
        public float ErrorWeight => errorWeight;
        public float RestartPenalty => restartPenalty;
        public float AbandonmentPenalty => abandonmentPenalty;
    }
}
