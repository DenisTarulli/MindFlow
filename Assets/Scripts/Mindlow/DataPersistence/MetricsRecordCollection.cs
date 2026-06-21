using System;
using System.Collections.Generic;
using Mindlow.Metrics;

namespace Mindlow.DataPersistence
{
    /// <summary>
    /// JsonUtility-compatible wrapper for metrics records.
    /// </summary>
    [Serializable]
    public sealed class MetricsRecordCollection
    {
        public List<PuzzleMetricsRecord> records = new List<PuzzleMetricsRecord>();
    }
}
