# Tetris
A playful Tetris terminal game written in C# that *should* work in most terminals.

If your terminal supports `ANSI escape codes` and/or complex unicode, you can further prettify the game by enabling color and full unicode support under the Options menu.

```
┌───────────────┐  ┌──────────────────────────────┐
│ .     .     . │  │ .     .     .     .     .    │
│ .  J  .     . │  │ .     .     .     .     .    │
│ .  J  J  J  . │  │ .     .     .     .     .    │
│ .     .     . │  │ .     .     .     .     .    │
└───────────────┘  │ .     .     .     .     .    │
                   │ .     .     .     .     .    │
┌───────────────┐  │ .     .     .     .     .    │
│ SCORE         │  │ .     .     .     .     .    │
│ 300           │  │ .     .     O  O  .     .    │
└───────────────┘  │ .     .     O  O  .     .    │
                   │ .     .     .     .     .    │
┌───────────────┐  │ .     .     .     .     .    │
│ Lines: 2      │  │ .     .     .     .     .    │
│ Blocks: 12    │  │ .     .     .     .     .    │
│ Level: 0      │  │ .     .     .     .     .    │
└───────────────┘  │ L     .     .     .     S    │
                   │ L     .     .     .     S  S │
                   │ L  L  .  J  J     .     Z  S │
                   │ O  O  .  J  I  I  .  Z  Z  Z │
                   │ .  T  S  S  I  I  T  T  Z  L │
                   └──────────────────────────────┘
```

## Controlls

Use arrow keys or W/A/S/D and Enter key to navigate menus.

Head to the Options menu in order to view the controls of the actual game. You can also chose between `simple` or `complex` control schemes; depending on how much control you want over block rotation.

## Persistent highscore and settings

Highscore and settings are stored in a text file called `tetris_store.txt` created at the root of the project path upon launching the game.

Removing this file will reset settings and highscore to default.

## Howto

Run `dotnet run --project TetrisGame` from the root to start the game.
