# Third-party notices

This repository contains, or depends on, the third-party components
listed below. Each keeps its own licence. Entries marked
**to be verified** have not been confirmed yet.

## Included in the repository

| Component | Location | Licence |
|---|---|---|
| Kawase blur render feature for URP | `Assets/Assets/FX/KawaseBlur/` | **To be verified.** Probably adapted from [sebastianhein/urp_kawase_blur](https://github.com/sebastianhein/urp_kawase_blur) (MIT). If so, its copyright notice must be kept here. |
| Fabric036 texture (ambientCG) | `Assets/Assets/Prefabs/Warehouse/Prefabs/Habitacion Aires/Texturas Air Room/Fabric036_2K_Color.png` | [CC0 1.0](https://docs.ambientcg.com/license/) |
| Partner and programme logos (CTM, DNV, FAMDIF, KSK, IED, Erasmus+, Creative Commons) | `Assets/Assets/Images/Logos/` | Trademarks of their owners, used with permission. The EU emblem has its own [usage rules](https://commission.europa.eu/resources-partners/european-union-emblem_en). |
| Models, textures, materials and audio of unconfirmed origin (`Prefabs/Warehouse`, `Crane`, `Forklift`, `Truck`, `Assets Edu`, `Objects/Residuos Piedras`, `Objects/Container/Dumpster Hydraulic.fbx`, `Materials/Marble`, `Images/CleaningProd`, `AudioFiles`) | `Assets/Assets/` | **To be verified.** Some were made by the project team. Others may come from third-party sources and must be removed if they cannot be redistributed. |

## Unity packages (resolved from the Unity registry, not included)

Listed in `Packages/manifest.json`. These packages are covered by the
[Unity Companion License](https://unity.com/legal/licenses/unity-companion-license)
or by the Unity Terms of Service:

- com.unity.render-pipelines.universal 10.6.0
- com.unity.xr.interaction.toolkit 2.3.2 (its *Starter Assets* sample
  must be imported from Package Manager)
- com.unity.xr.management 4.0.7
- com.unity.xr.oculus 1.10.0 (Oculus XR Plugin; its native components
  are covered by the Oculus SDK License)
- com.unity.formats.fbx 5.0.0, which includes Autodesk FBX SDK
  bindings (com.autodesk.fbx)
- com.unity.timeline, com.unity.ugui, com.unity.2d.sprite,
  com.unity.test-framework, com.unity.ide.rider,
  com.unity.ide.visualstudio and Unity built-in modules

## Removed from this public version (not redistributable)

The original project also used the following assets. Their licences do
not allow redistribution, so they are not in this repository:

- Unity Standard Assets (Unity Asset Store EULA)
- Oculus / Meta hand and controller models and images (Oculus SDK
  License)
- 3D models and textures from commercial marketplaces such as CGTrader
  (PPE, cleaning equipment, bins, pallets, chemical bottles, rocks,
  splats, vacuum cleaner, metal cabinet, garbage bags, ramp)
- Quixel Megascans surfaces
- A DeviantArt colour-wheel image
