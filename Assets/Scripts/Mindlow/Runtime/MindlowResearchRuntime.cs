using System.IO;
using Mindlow.AdaptiveDifficulty;
using Mindlow.DataPersistence;
using Mindlow.Metrics;
using Mindlow.PerformanceEvaluation;
using Mindlow.Puzzles;
using Mindlow.Shared.Events;
using Mindlow.Shared.Time;
using UnityEngine;

namespace Mindlow.Runtime
{
    /// <summary>
    /// Unity composition root that wires research systems together for a scene.
    /// </summary>
    public sealed class MindlowResearchRuntime : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private PerformanceEvaluationSettings performanceSettings;
        [SerializeField] private AdaptiveDifficultySettings difficultySettings;

        [Header("Persistence")]
        [SerializeField] private string metricsJsonFileName = "mindlow_metrics.json";
        [SerializeField] private string metricsCsvFileName = "mindlow_metrics.csv";

        private MetricsCollector metricsCollector;
        private PerformanceEvaluationService performanceEvaluationService;
        private AdaptiveDifficultyService adaptiveDifficultyService;
        private MetricsPersistenceService metricsPersistenceService;

        public MindlowEventBus EventBus { get; private set; }
        public PuzzleSessionService PuzzleSessions { get; private set; }
        public int CurrentDifficulty => adaptiveDifficultyService.CurrentDifficulty;

        private void Awake()
        {
            if (performanceSettings == null || difficultySettings == null)
            {
                Debug.LogError("MindlowResearchRuntime requires performance and difficulty settings assets.", this);
                enabled = false;
                return;
            }

            EventBus = new MindlowEventBus();
            ITimeProvider timeProvider = new UnityTimeProvider();
            IPuzzleVariantGenerator variantGenerator = new DefaultPuzzleVariantGenerator();

            PuzzleSessions = new PuzzleSessionService(EventBus, variantGenerator);
            metricsCollector = new MetricsCollector(EventBus, timeProvider);

            IPerformanceEvaluator evaluator = new WeightedPerformanceEvaluator(performanceSettings);
            performanceEvaluationService = new PerformanceEvaluationService(EventBus, evaluator);

            IDifficultyRuleSet ruleSet = new ThresholdDifficultyRuleSet(difficultySettings);
            adaptiveDifficultyService = new AdaptiveDifficultyService(EventBus, ruleSet, difficultySettings.InitialDifficulty);

            string jsonPath = Path.Combine(Application.persistentDataPath, metricsJsonFileName);
            IMetricsRepository repository = new JsonMetricsRepository(jsonPath);
            metricsPersistenceService = new MetricsPersistenceService(EventBus, repository, new CsvMetricsExporter());
        }

        private void OnDestroy()
        {
            metricsCollector?.Dispose();
            performanceEvaluationService?.Dispose();
            adaptiveDifficultyService?.Dispose();
            metricsPersistenceService?.Dispose();
        }

        /// <summary>
        /// Exports the saved metrics to CSV and returns the generated path.
        /// </summary>
        public string ExportMetricsToCsv()
        {
            string csvPath = Path.Combine(Application.persistentDataPath, metricsCsvFileName);
            metricsPersistenceService.ExportCsv(csvPath);
            return csvPath;
        }
    }
}
