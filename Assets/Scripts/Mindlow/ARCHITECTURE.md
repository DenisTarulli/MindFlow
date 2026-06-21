# Mindlow Adaptive Puzzle Architecture

## Modules

`PuzzleSystem` owns puzzle definitions, generated parameters, session lifecycle, and puzzle-facing reporting methods.

`MetricsSystem` listens to puzzle events and produces serializable research records: resolution time, attempts, errors, restarts, completion, and abandonment.

`PerformanceEvaluationSystem` evaluates metrics with deterministic weighted rules. It does not use machine learning.

`AdaptiveDifficultySystem` updates the current difficulty from 1 to 5 through replaceable rule sets.

`DataPersistenceSystem` stores metrics as JSON and exports them to CSV for later analysis.

## Dependency Diagram

```text
Unity Scene
  -> MindlowResearchRuntime
      -> PuzzleSystem
      -> MetricsSystem
      -> PerformanceEvaluationSystem
      -> AdaptiveDifficultySystem
      -> DataPersistenceSystem

PuzzleSystem -> Shared Events
MetricsSystem -> Shared Events, PuzzleSystem event data
PerformanceEvaluationSystem -> MetricsSystem records
AdaptiveDifficultySystem -> PerformanceEvaluationSystem results
DataPersistenceSystem -> MetricsSystem records
```

The event bus is created by the runtime composition root, so systems stay testable and avoid global state.
