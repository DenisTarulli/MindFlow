using System.Collections.Generic;
using Mindlow.Metrics;

namespace Mindlow.DataPersistence
{
    /// <summary>
    /// Persists and retrieves metrics records for later research analysis.
    /// </summary>
    public interface IMetricsRepository
    {
        IReadOnlyList<PuzzleMetricsRecord> LoadAll();
        void Save(PuzzleMetricsRecord record);
        void SaveAll(IReadOnlyList<PuzzleMetricsRecord> records);
    }
}
