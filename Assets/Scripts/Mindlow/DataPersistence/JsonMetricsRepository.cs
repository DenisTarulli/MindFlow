using System.Collections.Generic;
using System.IO;
using Mindlow.Metrics;
using UnityEngine;

namespace Mindlow.DataPersistence
{
    /// <summary>
    /// Stores metrics as a JSON file in a configurable directory.
    /// </summary>
    public sealed class JsonMetricsRepository : IMetricsRepository
    {
        private readonly string filePath;

        public JsonMetricsRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public IReadOnlyList<PuzzleMetricsRecord> LoadAll()
        {
            if (!File.Exists(filePath))
            {
                return new List<PuzzleMetricsRecord>();
            }

            string json = File.ReadAllText(filePath);
            MetricsRecordCollection collection = JsonUtility.FromJson<MetricsRecordCollection>(json);
            return collection?.records ?? new List<PuzzleMetricsRecord>();
        }

        public void Save(PuzzleMetricsRecord record)
        {
            List<PuzzleMetricsRecord> records = new List<PuzzleMetricsRecord>(LoadAll());
            records.Add(record);
            SaveAll(records);
        }

        public void SaveAll(IReadOnlyList<PuzzleMetricsRecord> records)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            MetricsRecordCollection collection = new MetricsRecordCollection();
            collection.records.AddRange(records);
            string json = JsonUtility.ToJson(collection, prettyPrint: true);
            File.WriteAllText(filePath, json);
        }
    }
}
