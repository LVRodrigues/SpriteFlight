# Repository Guidelines

## Project Structure

This repository is a Unity 2D game project (Unity `6000.6.6f1`). Runtime C# scripts are in `Assets/Scripts/`; the main scene is `Assets/Scenes/Game.unity`. Prefabs, materials, UI documents, and render pipeline settings live in their corresponding `Assets/` folders. Unity project configuration and package versions are in `ProjectSettings/` and `Packages/`. Keep each Unity asset's `.meta` file alongside it so GUID references remain stable. There is no separate test directory currently.

## Build, Test, and Development

Open the repository folder in Unity Hub with the editor version specified in `ProjectSettings/ProjectVersion.txt`. Use the Editor's Play button to run the game and **File > Build Profiles** to create a build. No project-specific command-line build or test script is configured. If Unity is installed, a batch-mode compile can be run with `Unity -batchmode -quit -projectPath . -logFile /tmp/sprite-flight-unity.log`; review the log for errors. The Unity Test Framework package is installed, but no tests are currently present.

## Coding Style and Assets

Use C# conventions already present in `Assets/Scripts`: four spaces for indentation, PascalCase for types and methods, and descriptive camelCase for local variables and parameters. Keep gameplay behavior in focused `MonoBehaviour` scripts and expose Inspector-tuned values as serialized fields where appropriate. Name scripts after their main class (for example, `PlayerController.cs`). Edit scenes, prefabs, and UI assets through Unity when possible, and retain generated `.meta` files.

## Testing Guidelines

For gameplay changes, open `Assets/Scenes/Game.unity`, enter Play mode, and check the affected behavior and Console for errors. Add automated tests under a clearly named test folder (such as `Assets/Tests/EditMode` or `Assets/Tests/PlayMode`) when introducing test coverage, using Unity Test Framework conventions. Run them from **Window > General > Test Runner**.

## Commits and Pull Requests

Recent commits use short, direct subjects, sometimes in Portuguese and sometimes in English; no strict prefix convention is established. Keep commit subjects concise and describe the change. Pull requests should summarize gameplay or asset changes, list manual or automated checks performed, link related issues when available, and include screenshots or a short clip for visible scene or UI changes.

## Configuration

Use Unity's Package Manager to manage dependencies and commit intentional changes to `Packages/manifest.json` and `Packages/packages-lock.json` together. Avoid committing generated folders such as `Library/`, `Temp/`, and build output.
