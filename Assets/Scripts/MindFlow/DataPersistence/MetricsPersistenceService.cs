using System;
using System.Collections.Generic;
using MindFlow.Metrics;
using MindFlow.Shared.Events;

namespace MindFlow.DataPersistence
{
    /// <summary>
    /// Saves metrics as they are recorded and exposes export operations.
    /// </summary>
    public sealed class MetricsPersistenceService : IDisposable
    {
        private readonly MindFlowEventBus eventBus;
        private readonly IMetricsRepository repository;
        private readonly CsvMetricsExporter csvExporter;

        public MetricsPersistenceService(MindFlowEventBus eventBus, IMetricsRepository repository, CsvMetricsExporter csvExporter)
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
