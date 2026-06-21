# Mindlow

Mindlow is an experimental Unity 2D project focused on adaptive difficulty in puzzle-based experiences.

The project is designed as a research-oriented prototype: its main goal is to observe how players interact with puzzles, collect structured metrics, estimate performance through deterministic rules, and adjust puzzle difficulty without using machine learning.

## Purpose

Mindlow explores adaptive difficulty as a tool for puzzle design and academic analysis. Instead of relying on character movement, combat, exploration, or action-based mechanics, the project focuses on puzzle sessions as measurable research units.

The core research loop is:

```text
Player solves a puzzle
  -> gameplay metrics are collected
  -> performance is evaluated
  -> difficulty is adjusted
  -> data is persisted for later analysis
```

## Main Features

- Modular puzzle system with support for multiple puzzle types.
- Puzzle configuration through Unity `ScriptableObject` assets.
- Metrics collection for resolution time, attempts, errors, restarts, completions, and abandonments.
- Rule-based performance evaluation without machine learning.
- Adaptive difficulty levels from 1 to 5.
- JSON persistence and CSV export for research workflows.
- Decoupled architecture based on clear system responsibilities and local events.

## Architecture

Mindlow is organized around five main systems:

```text
PuzzleSystem
MetricsSystem
PerformanceEvaluationSystem
AdaptiveDifficultySystem
DataPersistenceSystem
```

Each system has a focused responsibility. Puzzle logic does not directly store metrics, persistence does not know how puzzles work, and difficulty adaptation depends on performance results rather than scene-specific behavior.

More detailed architecture notes are available in:

```text
Assets/Scripts/Mindlow/ARCHITECTURE.md
```

## Research Constraints

Mindlow intentionally avoids several traditional videogame systems in order to keep the research scope focused:

- No playable character.
- No movement system.
- No exploration loop.
- No combat system.
- No machine learning.

## Project Status

The project is in early development. The current foundation includes the first version of the adaptive puzzle architecture, metrics collection flow, deterministic performance evaluation, adaptive difficulty rules, and data persistence/export services.

Upcoming work will focus on concrete puzzle prototypes, debug/research UI, sample datasets, validation tests, and documentation of research variables.

## Built With

- Unity 2D
- C#
- Universal Render Pipeline 2D
