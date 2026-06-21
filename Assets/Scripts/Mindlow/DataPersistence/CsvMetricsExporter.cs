using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Mindlow.Metrics;

namespace Mindlow.DataPersistence
{
    /// <summary>
    /// Exports metrics records to a CSV file suitable for spreadsheet or statistics tools.
    /// </summary>
    public sealed class CsvMetricsExporter
    {
        public void Export(string filePath, IReadOnlyList<PuzzleMetricsRecord> records)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("sessionId,puzzleId,puzzleType,difficultyLevel,complexity,variantSeed,variantLabel,resolutionTimeSeconds,attempts,errors,restarts,abandoned,completed,startedAtUtc,endedAtUtc");

            foreach (PuzzleMetricsRecord record in records)
            {
                builder
                    .Append(Escape(record.sessionId)).Append(',')
                    .Append(Escape(record.puzzleId)).Append(',')
                    .Append(Escape(record.puzzleType)).Append(',')
                    .Append(record.difficultyLevel).Append(',')
                    .Append(record.complexity).Append(',')
                    .Append(record.variantSeed).Append(',')
                    .Append(Escape(record.variantLabel)).Append(',')
                    .Append(record.resolutionTimeSeconds.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(record.attempts).Append(',')
                    .Append(record.errors).Append(',')
                    .Append(record.restarts).Append(',')
                    .Append(record.abandoned).Append(',')
                    .Append(record.completed).Append(',')
                    .Append(Escape(record.startedAtUtc)).Append(',')
                    .Append(Escape(record.endedAtUtc))
                    .AppendLine();
            }

            File.WriteAllText(filePath, builder.ToString());
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            string escaped = value.Replace("\"", "\"\"");
            return "\"" + escaped + "\"";
        }
    }
}
