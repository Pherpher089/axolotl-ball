# 🦎 Axolotl Ball

A fast-paced **2-player local multiplayer** game built in Unity where two axolotl characters battle for court domination by shooting a ball through hoops.

---

## 🎮 Gameplay

Two players compete on a shared 2D court. Each player controls an axolotl character and tries to shoot the ball through their opponent's hoop. Every time the ball passes through a hoop, **5 wall blocks** on the court are painted in the scoring player's color.

### Win Conditions

- **Domination** – Capture every block on the court to win immediately.
- **Timer** – When the 30-second round timer expires, the player who controls the most blocks wins. If both players control the same number of blocks, the match ends in a **Tie**.

---

## 🕹️ Controls

The game is designed primarily for **touch/mobile** input, with optional mouse support.

| Action | Input |
|--------|-------|
| Move character | Drag (touch or mouse) within your half of the court |
| Pause | `Escape` key |

Each player's drag input is constrained to their own side of the court, so two players can play on the same device simultaneously.

---

## 📋 Scenes

| Scene | Description |
|-------|-------------|
| `MainMenu` | Title screen with Play and Quit buttons |
| `GameScene` | Main gameplay arena |
| `HowToWin` | Instructions / tutorial screen |

---

## 🏗️ Project Structure

```
Axolotl Ball/
└── Assets/
    ├── _CharacterController/   # Player movement logic
    ├── _GameControl/           # Core game systems (scoring, timer, UI, audio)
    ├── _Environment/           # Court walls and invisible boundaries
    ├── _Audio/                 # Audio assets
    ├── _Scenes/                # Unity scene files
    ├── _Fonts/                 # Font assets
    └── _PhysicalMaterials/     # Physics materials
```

### Key Scripts

| Script | Purpose |
|--------|---------|
| `GameController` | Central game state: initializes players, handles scoring, win detection |
| `CharacterUserControl` | Player movement via touch/mouse drag input |
| `BallController` | Ball physics, speed clamping, and ownership tracking |
| `HoopControl` | Detects when the ball passes through a hoop and triggers scoring |
| `ScoreController` | Updates the on-screen block score bar |
| `RoundTimer` | Countdown timer that triggers end-of-round resolution |
| `StartCountDownController` | Pre-match 3-2-1-Go! countdown and post-match win screen |
| `PauseMenuController` | Pause/resume, restart, and quit-to-menu logic |
| `SoundManager` | Centralized audio playback (music, SFX, ambient crowd) |
| `CameraShake` | Triggers camera shake on score and win events |
| `WallController` | Tracks which player controls each court block |

---

## 🛠️ Built With

- **Unity 2022.3.9f1**
- **TextMesh Pro** – UI text rendering
- **Unity 2D Physics** – Ball and character rigidbodies

---

## 🚀 Getting Started

1. Clone the repository.
2. Open the `Axolotl Ball` folder as a Unity project in **Unity 2022.3.9f1** (or a compatible LTS version).
3. Open the `MainMenu` scene and press **Play** in the Unity Editor.
4. To build for a target platform, open **File → Build Settings**, add all three scenes, and build.

---

## 🤝 Contributing

Pull requests are welcome! Please open an issue first to discuss any significant changes.

