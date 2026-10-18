# InclusiveSTONE: VR training for the natural stone industry

InclusiveSTONE is a virtual reality training simulator for vocational
education in the natural stone sector. In a set of hands-on scenarios,
learners practise operating machinery and handling workplace safety
tasks in a stone-processing plant. They get real-time feedback in a
safe virtual environment and can choose among several languages.

It was developed between October 2023 and January 2024 as part of an
**Erasmus+ Cooperation Partnership in Vocational Education and Training
(KA220-VET)**, project number `2021-1-DE02-KA220-VET-000033276`.

It was built at CTM (Centro Tecnológico del Mármol, Piedra y Materiales,
Spain) and is published with CTM's permission.

> **Note:** this public version does **not** include third-party
> models, textures and SDK assets whose licences do not allow
> redistribution. The project opens, but several objects in the scenes
> show as missing until you add those assets back
> (see [Getting started](#getting-started)).

## Training modules

| Scene | Module | What the learner does |
|---|---|---|
| `Main Menu` | Menu | Choose the language and the training module. |
| `Forklift - Storage` | Forklift operator | Tutorial (accelerate, reverse, brake, steer, operate the mast), then move pallets to the warehouse and load a truck. |
| `Crane - Slabs` | Overhead crane operator | Use the crane pendant control, sling marble slabs and place them on supports, avoiding obstacles. |
| `Clean` | Cleaning operator | Pick the right personal protective equipment (PPE), take the cleaning cart and decide how to handle each type of spill or waste. |
| `Waste Management` | Waste management | Sort chemical products on a shelf safely, answer quizzes about waste types and dispose of waste in the correct container. |
| `CNC` | Crane – block handling | *Work in progress, not in Build Settings.* Move a stone block from a truck to the block-cutting machine. |

User interface languages: English, Spanish, German, Greek and Italian
(`Assets/Resources/Lang/*.txt`). The German, Greek and Italian files are
AI-assisted translations that a native speaker still needs to review.

## Tech stack

- **Unity 2020.3.20f1** (LTS) with the Universal Render Pipeline 10.6
- **XR Interaction Toolkit 2.3.2**, XR Plug-in Management 4.0 and the
  **Oculus XR Plugin 1.10** (Meta Quest, standalone or via Link)
- Input System 1.4, uGUI, Shader Graph
- C# (about 60 scripts)

```
Assets/
├─ Scenes/                 Main Menu, Forklift, Crane, Clean, Waste Management, CNC
├─ Scripts/
│  ├─ GameControl/         mission flow, goal zones, sweeping
│  ├─ Machines/            crane (hook, slings, weights, obstacles), forklift, magnet
│  ├─ PlayerControl/       hands, two-hand grab, locomotion, UI ray
│  ├─ Canvas/              instructions, slides, multiple-choice quizzes
│  ├─ Translator/          key=value localisation loader
│  ├─ TriggerActivation/   quiz and collection triggers
│  └─ Select/, Challenge/, Animations/   object selection, sorting checks, progress UI
├─ Resources/Lang/         localisation files
└─ Assets/                 models, materials, prefabs, images, audio
```

## Getting started

1. Install **Unity 2020.3** through Unity Hub. The project was created
   with 2020.3.20f1, but use **2020.3.49f1 or later**: earlier
   versions are affected by
   [CVE-2025-59489](https://unity.com/security/sept-2025-01).
2. Install [Git LFS](https://git-lfs.com/) before cloning
   (`git lfs install`).
3. Open the project folder in Unity Hub.
4. In Package Manager, go to **XR Interaction Toolkit**, open
   **Samples** and import **Starter Assets**. Every scene uses its
   `XRI Default Input Actions`. Package samples keep their GUIDs, so
   the references resolve again.
5. Optionally, add back the removed third-party assets (below) to get
   the full visuals.
6. Open `Assets/Scenes/Main Menu.unity` and press Play with a Quest
   headset connected through Link. You can also build for Android
   (Quest).

### Removed third-party assets

| What | Where it was | Where to get it |
|---|---|---|
| Unity Standard Assets (DustStorm particles in `Clean`) | `Assets/Assets/Standard Assets` | Unity Asset Store, "Standard Assets (for Unity 2018.4)" (deprecated) |
| Oculus hand and controller models, controller button images | `Prefabs/Hands/Oculus Hands`, `Images/Oculus controller` | Meta XR / Oculus Integration packages |
| PPE, cleaning equipment, bins, pallets, chemical bottles, rocks and similar props | `Prefabs/Objects/*` | Commercial 3D marketplaces (for example CGTrader); any similar model works |
| Surface textures (Quixel Megascans) | `Prefabs/Assets Edu/Limpieza` | Fab / Quixel Megascans |

See [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md) for what stays in
the repository and under which licence.

### Known issues

- According to its changelog, XR Interaction Toolkit 2.3.x needs
  Unity 2021.3 or later. Opening the project in 2020.3 may need XRI
  2.2.x or an Editor upgrade. Not verified.
- `Prefabs/Truck/Truck/trailer_SEP.blend` needs Blender installed so
  that Unity can import it.
- The `fail`, `next`, `retry` and `sit2_check` localisation keys are
  used by two prefabs but are not defined in any language file.

## My role

I was the lead developer of InclusiveSTONE at CTM. I wrote all of the
application code (C#): the VR player rig and interactions, the machinery
simulations (forklift, crane, CNC), the cleaning and waste-management
modules, mission and scoring logic, the menus and the localisation
system, and I integrated the 3D environments into the training scenes.

## Credits

- Development: Charli (Carlos Martínez), [@charli-mangos](https://github.com/charli-mangos)
- 3D environment models: Eduardo Martínez García ([@edmarga](https://github.com/edmarga))
- Developed within the Erasmus+ KA220-VET project
  `2021-1-DE02-KA220-VET-000033276`. The partner organisations'
  logos are shown in the application's main menu.

## EU funding notice

Funded by the European Union. Views and opinions expressed are however
those of the author(s) only and do not necessarily reflect those of the
European Union or the European Education and Culture Executive Agency
(EACEA). Neither the European Union nor EACEA can be held responsible
for them.

## License

**Pending.** The licence for this repository has not been decided yet
and depends on CTM and the project consortium. Until a `LICENSE` file
is added, all rights are reserved.

The application's main menu shows a Creative Commons
Attribution-ShareAlike 4.0 International notice for the training
content. Third-party components keep their own licences (see
[THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md)).

## Security

See [SECURITY.md](SECURITY.md).
