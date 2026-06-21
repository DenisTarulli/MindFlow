# MindFlow Setup Guide

This guide is for local project setup and development notes.

## First Unity Setup Steps

1. Open the project in Unity.
2. Create a new empty GameObject in the scene named `MindFlowResearchRuntime`.
3. Add the `MindFlowResearchRuntime` script to that GameObject.
4. Create these configuration assets from the Unity asset menu:
   - `MindFlow/Puzzles/Puzzle Definition`
   - `MindFlow/Performance/Evaluation Settings`
   - `MindFlow/Adaptive Difficulty/Settings`
5. Assign the performance and adaptive difficulty settings assets to the `MindFlowResearchRuntime` component.
6. Create another empty GameObject named `PuzzleLauncher`.
7. Add the `PuzzleLaunchRequest` script to `PuzzleLauncher`.
8. Assign the `MindFlowResearchRuntime` scene object and a `Puzzle Definition` asset to `PuzzleLaunchRequest`.
9. Create a concrete puzzle script that inherits from `PuzzleBehaviour`.
10. Add that concrete puzzle script to a puzzle GameObject and assign it to `PuzzleLaunchRequest`.

## Minimal Puzzle Script Pattern

Concrete puzzle scripts should inherit from `PuzzleBehaviour` and report meaningful player actions:

```csharp
using MindFlow.Puzzles;

public sealed class ExamplePuzzle : PuzzleBehaviour
{
    public void SubmitAnswer(bool isCorrect)
    {
        ReportAttempt();

        if (!isCorrect)
        {
            ReportError();
            return;
        }

        ReportCompleted();
    }
}
```

## Suggested Next Steps

1. Create the first concrete puzzle prototype.
2. Add a small UI for starting, restarting, completing, and abandoning a puzzle session.
3. Add a debug panel that displays current difficulty, latest score, attempts, errors, and elapsed time.
4. Create sample `PuzzleDefinition` assets for difficulty levels 1 to 5.
5. Add editor or play mode tests for metrics collection and difficulty adaptation rules.
6. Validate exported CSV files with a small research dataset.
7. Document the research variables that will be analyzed later.

## Development Notes

Check `PROJECT_CONTEXT.md` before generating new systems or changing the project direction.

The project constraints are:

- No machine learning.
- No player character.
- No movement system.
- No exploration loop.
- No combat system.
- Puzzle metrics and adaptive difficulty are the main gameplay-research loop.
