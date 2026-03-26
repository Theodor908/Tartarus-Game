# Tartarus-Game

### College Project

**Tartarus** is a Soulslike game developed in Unity, featuring the basic elements of the genre. Known issues include enemies occasionally hovering above the player during collisions (unresolved) and fleeing unexpectedly. Despite the game becoming somewhat messy and hard to manage as it grew, it was a valuable learning experience and remains a success in terms of functionality.

### Features
- **Title screen** with load functionality.
![Title Screen](https://github.com/user-attachments/assets/13acbdb0-7704-4894-a5c9-d5c288957f47)

- **Health, stamina, and focus bars**. The game includes a sword, shield, and two additional weapons dropped by enemies. There's one special spell, along with health and focus potions. A menu, activated by pressing **F**, allows players to assign items to quick slots. Additionally, there's an upgrade system for status bars.
![Status Bars](https://github.com/user-attachments/assets/d57926e3-ff27-44f2-97df-48b198dca537)

- **One of three enemy types** scattered across the map.
![Enemy](https://github.com/user-attachments/assets/99351957-aa7b-4f54-8eed-423478d4f9f6)

- **Unique boss battle**.
![Boss Battle](https://github.com/user-attachments/assets/f8c8fde7-fe6c-4d9b-8fb2-9014bc086945)
![Captură de ecran 2024-09-19 004109](https://github.com/user-attachments/assets/84b58ccf-2078-4dc2-8c93-ace2ae1def07)
---

### How to Play

#### Controls
| Action | Key |
|--------|-----|
| Move | WASD |
| Sprint | Hold Left Shift |
| Roll / Dodge | Space |
| Light Attack | Left Mouse Button |
| Heavy Attack | Hold Left Mouse Button |
| Block (with shield) | Right Mouse Button |
| Lock-On Target | Middle Mouse Button |
| Use Potion / Spell | 1 / 2 / 3 (Quick Slots) |
| Open Inventory Menu | F |
| Interact / Talk to NPC | G |

#### Gameplay Tips
- Rest at **Sites of Grace** to restore health and set your respawn point
- Manage your **stamina** carefully — every attack, roll, and block costs stamina
- Enemies drop weapons — pick them up and equip them via the inventory menu
- Spend **Essence** (currency dropped by enemies) to upgrade Vitality, Endurance, or Attunement
- Use **lock-on** during combat for more precise attacks and dodges

---

### Tech Stack

| Component | Technology |
|-----------|------------|
| Engine | Unity 6 (6000.2.7f2) |
| Render Pipeline | High Definition Render Pipeline (HDRP) |
| Language | C# |
| Input System | Unity New Input System (1.14.2) |
| Navigation | Unity AI Navigation (2.0.9) |
| UI | TextMesh Pro (2.0.0) |
| Physics | Unity Physics + Havok Physics |
| Level Design | ProBuilder + ProGrids |
| Version Control | Git + Git LFS |

---

### How to Run from Source

#### Prerequisites
- **Unity Hub** with **Unity 6 (6000.2.7f2)** installed
- **Git LFS** installed (`git lfs install`)
- The third-party assets listed below (not included in the repo due to licensing)

#### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/Theodor908/Tartarus-Game.git
   ```
2. Open **Unity Hub** and click **Open > Add project from disk**
3. Select the cloned `Tartarus-Game` folder
4. Unity will prompt you to install the correct editor version if needed
5. **Import the required third-party assets** from the Unity Asset Store (see below)
6. Open `Assets/Level/Scenes/Scene_Main_Menu_01.unity` and press **Play**

#### Required Third-Party Assets
These assets are excluded from the repository. You must import them manually via the Unity Asset Store or Package Manager:
- POLYGON Dungeon Realms
- POLYGON Fantasy Kingdom
- POLYGON Fantasy Rivals
- POLYGON Knights
- Malbers Animations - Forest Pack
- Fighter Pack Bundle
- Kevin Iglesias - Archer Animations
- Magical-Knight Set
- Fantasy RPG Icons Pack
- Cartoon FX Remaster (JMO Assets)
- Stylized Water 2

---

### Project Structure

```
Assets/
├── Code/Scripts/       # All game scripts (C#)
│   ├── Character/      # Player & AI character systems
│   ├── Items/          # Weapons, spells, potions
│   ├── Colliders/      # Damage collision logic
│   ├── Dialog/         # NPC dialog system
│   ├── Effects/        # Status effects
│   ├── Interactables/  # Sites of Grace, fog walls
│   ├── NPC/            # NPC interaction system
│   ├── SaveGame/       # Save/load system (JSON, 10 slots)
│   ├── UI/             # HUD, menus, title screen
│   └── WorldManagers/  # Game state singletons
├── Data/               # ScriptableObjects (items, AI, effects)
├── Level/              # Scenes, prefabs, level layout
└── Art/                # Animation controllers & masks
```

---

### Disclaimer

The assets used in this project were obtained from sources that distribute them for prototyping and learning purposes only. I have not paid for these assets, and I do not intend to profit from someone else's work. They were used strictly for practice in creating graphics, aligning animations, and other game development exercises.
### Game drive link
[https://drive.google.com/file/d/13O3YvTmD3Babi5WtOWgJyI2WwkodJIpC/view?usp=sharing](https://drive.google.com/file/d/1RIXrfbLCsbZ7uCZHj9MsYuRikzF0IcXx/view?usp=sharing)
