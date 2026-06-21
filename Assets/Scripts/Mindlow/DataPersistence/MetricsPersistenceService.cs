using System;
using System.Collections.Generic;
using Mindlow.Metrics;
using Mindlow.Shared.Events;

namespace Mindlow.DataPersistence
{
    /// <summary>
    /// Saves metrics as they are recorded and exposes export operations.
    /// </summary>
    public sealed class MetricsPersistenceService : IDisposable
    {
        private readonly MindlowEventBus eventBus;
        private readonly IMetricsRepository repository;
        private readonly CsvMetricsExporter csvExporter;

        public MetricsPersistenceService(MindlowEventBus eventBus, IMetricsRepository repository, CsvMetricsExporter csvExporter)
        {
            this.eventBus = eventBus;
            this.repository = repository;
            this.csvExporter = csvExporter;
            eventBus.MetricsRecorded += HandleMetricsRecorded;
        }

        public void Dispose()
        {
            eventBus.MetricsRecorded -= HandleMetricsRecorded;
        }

        public IReadOnlyList<PuzzleMetricsRecord> LoadAll()
        {
            return repository.LoadAll();
        }

        public void ExportCsv(string filePath)
        {
            csvExporter.Export(filePath, repository.LoadAll());
        }

        private void HandleMetricsRecorded(PuzzleMetricsRecord record)
        {
            repository.Save(record);
        }
    }
}
