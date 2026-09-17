# Form1 — Layout and Code Overview

This README documents the UI layout on `Form1` and explains the implementation contained in `Form1.cs`. Use this as a quick reference for the form controls, behavior, resources used, and suggested improvements.

## Overview
`Form1` implements a small rule-based chatbot UI that:
- Displays a logo on load (`pictureBox1`).
- Plays a greeting sound on load.
- Accepts user input and replies with canned responses based on keyword matching.

Target framework: .NET 8 (C# 12.0).

## Controls (names used in code)
- `pictureBox1` — displays the application logo (loaded by `Display_Logo()`).
- `textBox1` — multi-line chat log where the bot and messages are appended.
- `textBox2` — single-line input for user messages.
- `button1` — plays the greeting sound and appends a simple hello message to `textBox1`.
- `button2` — sends the content of `textBox2` to the bot, appends the user's message, and clears `textBox2`.

## Important methods and behavior (`Form1.cs`)
- `InitializeData()`
  - Builds `KeywordResponse`: a `Dictionary<string, List<string>>` mapping keywords (e.g., `"hello"`, `"password"`, `"phishing"`) to one-or-more canned responses.
  - Builds `worriedWords`: a `List<string>` of emotional trigger words (e.g., `"worried"`, `"anxious"`).

- `Response(string userInput)`
  - Performs case-insensitive keyword checks against `KeywordResponse`. If a match is found, chooses a random response from the corresponding list.
  - Separately checks `userInput` against `worriedWords` and returns a calming message if matched.
  - Appends the final response to `textBox1` with the prefix `Chatbot:`.
  - Notes:
    - The method currently uses `Console.Write` and `Thread.Sleep(15)`. Both are unnecessary for UI updates and can block the UI thread.
    - `random` is an instance-level `Random` used to pick random responses.

- `Form1_Load(object sender, EventArgs e)`
  - Calls `Display_Logo()` and `PlayGreeting()` when the form loads.

- `PlayGreeting()`
  - Uses `System.Media.SoundPlayer` to play an audio file at a hard-coded absolute path.

- `Display_Logo()`
  - Loads an image via `Image.FromFile` from a hard-coded absolute path and assigns it to `pictureBox1`.

- Event handlers:
  - `button1_Click` — plays greeting and appends `Bot: Hello` to `textBox1`.
  - `button2_Click` — appends the user's input as `Bot: <input>`, calls `Response(...)`, and clears `textBox2`.

## Resource paths (current implementation)
The project currently loads resources using absolute file paths:
- `C:\Users\Student\source\repos\GUIchartBot\Resourse\chartbot.wav`
- `C:\Users\Student\source\repos\GUIchartBot\Resourse\logo.png`

Recommendation: Do not use absolute, user-specific paths. Instead:
- Add the files to the project resources (`Properties.Resources`) or
- Copy them into the application output directory and load them with a relative path, or
- Embed them as resources and load via `Assembly.GetManifestResourceStream`.

## Run / build notes
- Open the solution in Visual Studio 2022 and build/run.
- Ensure audio and image files exist at the expected locations or update the code to use embedded or relative resources.

## Suggested improvements
- Replace absolute file paths with embedded resources or relative paths.
- Remove `Thread.Sleep(15)` from UI code; it blocks the UI thread.
- Replace `Console.Write` with proper logging or remove it for a GUI app.
- Use `using` or call `Dispose()` when using `SoundPlayer` or loaded `Image` instances, or better, use resources that the designer manages.
- Add null-checks and exception handling when loading resources to avoid runtime crashes.
- Consider asynchronous APIs for I/O and sound playback so the UI remains responsive.
- Add unit tests around the response generation logic (extract logic into a testable class).

## Quick navigation
- UI logic and event handlers: `Form1.cs`
- Designer/layout: `Form1.Designer.cs`
- Resources: move `chartbot.wav` and `logo.png` into the project resources folder or `Properties/Resources.resx`.
