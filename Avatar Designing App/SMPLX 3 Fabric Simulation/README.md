# Unity SMPL-X Runtime Fabric Controller

## 📖 Overview
This document elevates the previous pose/shape/expression setup into a fabric‑centric avatar controller. It blends Alembic garments with the existing pose pipeline so that wardrobe changes, pose switches, and UI visibility remain coordinated and predictable during play. The objective is a practical, reusable flow that you can drop into scenes without rewriting scripts or re‑authoring assets.

---

---

## ✨ Features
- Fabric‑first avatar control with fast garment swapping (Shirt1/Shirt2) powered by Alembic caches.
- Works alongside the pose system: pose toggles, panel visibility, and T‑pose resets remain intact.
- Image‑backed buttons and a dedicated RemoveCloth action for clear, visual wardrobe management.
- Scene‑friendly hierarchy that keeps garments following the rig in both idle and animated use cases.
- Minimal moving parts: no new frameworks, only a focused script and tidy UI wiring.

---

## 🛠 Workflow Summary
The process spans authoring in Blender and integration in Unity.

### Blender
- Author, simulate, and bake garments per item; export as Alembic with correct unit scaling.

### Unity
- Import the `.abc` files, wire buttons and panels, and connect the AlembicClothingChanger to show, hide, or clear garments while the PoseController maintains UI and pose state.

---

## 📑 Table of Contents

* [Overview](#overview)
* [How It Works](#how-it-works)
* [Getting Started: Project Setup](#getting-started-project-setup)
   1. [Prerequisites](#prerequisites)
   2. [Scene Preparation](#scene-preparation)
* [Step-by-Step Implementation Guide](#step-by-step-implementation-guide)
   * [Step 1: Blender – Bake and Export Alembic](#step-1-blender--bake-and-export-alembic)
   * [Step 2: Unity – Install Alembic Package](#step-2-unity--install-alembic-package)
   * [Step 3: Prepare Assets and Folders](#step-3-prepare-assets-and-folders)
   * [Step 4: Scene Hierarchy with Alembic Clothes](#step-4-scene-hierarchy-with-alembic-clothes)
   * [Step 5: UI Buttons and ClothPanel](#step-5-ui-buttons-and-clothpanel)
   * [Step 6: Add Scripts and Inspector Wiring](#step-6-add-scripts-and-inspector-wiring)
   * [Step 7: UI Button Wiring (On Click)](#step-7-ui-button-wiring-on-click)
   * [Step 8: Background and Materials](#step-8-background-and-materials)
* [Important Notes & Common Mistakes](#important-notes--common-mistakes)
* [Final Result](#final-result)

---

## How It Works

Blender produces the garment animation, Unity plays it back, and the UI orchestrates both appearance and pose. The PoseController keeps panel visibility and pose application unified (including auto T‑pose when toggling), while AlembicClothingChanger exposes concise actions for showing Shirt1/2 or clearing all clothing. The result is a coherent loop: choose a pose, apply or remove garments, and keep the character grounded and readable.

---

## Getting Started: Project Setup

### Prerequisites
- Unity 2020.x or newer with TextMeshPro installed for crisp UI labels and icons.
- Alembic package from the Unity Registry to support import and playback of `.abc` caches.
- Garments baked and exported from Blender with `Scale = 100` so world units match.
- A character rig available so garments can be parented and follow motion.

### Scene Preparation
- Open the target scene and verify camera and lighting present the avatar clearly.
- Ensure the character’s transforms are reset and uniform `(1,1,1)` to prevent mismatched offsets.
- Confirm the EventSystem exists; add one if missing to enable button interaction.

---

## Step-by-Step Implementation Guide

### Step 1: Blender – Bake and Export Alembic

- Finalize and **Bake** each garment (e.g., `Shirt1`, `Shirt2`) so motion is deterministic.
- Export per garment with **Selected Objects** enabled and **Scale = 100** to preserve unit parity.
- Keep Alembic files separate (`Shirt1.abc`, `Shirt2.abc`) for modular swapping inside Unity.

---

### Step 2: Unity – Install Alembic Package

- Window → Package Manager → Unity Registry → install **Alembic** to enable `.abc` import and playback.

---

### Step 3: Prepare Assets and Folders

- In `Assets`, keep `Model` and `Pics` at the top level for clarity.
- In `Assets/Scripts`, add `AlembicClothingChanger.cs` and replace any old `PoseController.cs` reference for cloth handling with this script.
- Move Pose images into `Assets/Pics/Pose` (rename the folder to `Pose`).
- Confirm `Shirt1.abc` and `Shirt2.abc` are imported and ready.

---

### Step 4: Scene Hierarchy with Alembic Clothes

- Drop `Shirt1.abc` and `Shirt2.abc` into the scene and parent them to the character rig so they follow animation.
- Target hierarchy (based on the provided screenshot):

```plaintext
SampleScene
└── smplx-male
      ├── SMPLX-male
      │   └── SMPLX-mesh-male
      └── root
Shirt1 (new)
Shirt2 (new)
PoseManager
ShapeManager
Canvas
      ├── PosePanel (pose buttons)
      ├── ClothPanel (new)
      ├── ShapePanel
      ├── ExpPanel
      ├── RotationSlider
      └── RemoveCloth (new)
      └── ToggleUI_Button
EventSystem
BackGround (new)
```

- Align garments to the rig and keep scales at (1,1,1) to avoid drift.

---

### Step 5: UI Buttons and ClothPanel

- Duplicate `ToggleUI_Button`, rename to `RemoveCloth`; delete its `Text (TMP)` child, anchor bottom-left for quick access.
- Select cross image from `Assets/Pics`; set Texture Type = Sprite (2D and UI), Sprite Mode = Single → Apply.
- On `RemoveCloth` button, drag the sprite into the Image component’s **Source Image**.
- Duplicate `PosePanel`, rename to `ClothPanel`, place above `ExpPanel`.
- Inside `ClothPanel`, keep only two buttons (`Shirt1`, `Shirt2`); drop extras and apply the correct color materials (green, blue).
- Add images to Shirt buttons:
   - Select an image from `Assets/Pics/Cloth`; set Texture Type = Sprite (2D and UI), Sprite Mode = Single → Apply.
   - On each button, drag the sprite into the Image component’s **Source Image**.
- Add materials to garments:
   - From `Assets/Pics/Cloth/Materials`, drag the proper material onto `Shirt1/Shirt1/Plane` (repeat for Shirt2 with its material).

---

### Step 6: Add Scripts and Inspector Wiring

- In PoseManager (Pose Management), assign:
   - Cloth Panel slot → `ClothPanel`
   - Remove Cloth Button slot → `RemoveCloth`
- Add `AlembicClothingChanger.cs` to the PoseManager via Add Component in the Inspector. In its Inspector:
   - `Shirt1` → `Shirt1`
   - `Shirt2` → `Shirt2`

---

### Step 7: UI Button Wiring (On Click)

- ClothPanel → Shirt1 Button, under Inspector tool:
   - Click `+`, then Drag `PoseManager` → `PoseController → ApplyPose(int)` → enter `5`
   - Click `+`, then Drag `PoseManager` → `AlembicClothingChanger → ShowShirt1()`
   - Click `+`, then Drag `RotationSlider` → `Slider → float value` → enter `0`

- ClothPanel → Shirt2 Button: same as above, but select `ShowShirt2()`.

- RemoveCloth Button:
   - Click `+`, then Drag `PoseManager` → `PoseController → ApplyPose(int)` → enter `5`
   - Click `+`, then Drag `PoseManager` → `AlembicClothingChanger → RemoveAllClothing()`
   - Click `+`, then Drag `RotationSlider` → `Slider → float value` → enter `0`

- ToggleUI_Button:
   - Click `+`, then Drag `PoseManager` → `PoseController → TogglePoseUIVisiblity()`
   - Click `+`, then Drag `PoseManager` → `AlembicClothingChanger → RemoveAllClothing()`
   - Click `+`, then Drag `RotationSlider` → `Slider → float value` → enter `0`
   - (T-pose is auto-handled inside `TogglePoseUIVisiblity()`, no extra ApplyPose needed.)

- PosePanel buttons (ArmsUp, Running, SideKick, Sitting, Surprise, TPose):
   - Each On Click(): `ApplyPose(int)` with values 0–5 respectively, plus `RemoveAllClothing()` to clear garments when changing poses.

- RotationSlider, On Value Changed (Single):
   - Click `+`, then Drag `PoseManager` → `PoseController → SetRootRotation` (Dynamic Float)

---

### Step 8: Background and Materials

- Background: create empty child `BackGround`, add component Sprite Renderer.
   - Import `BackGround` into `Assets/Pics` (or your own); set Texture Type = Sprite (2D and UI), Sprite Mode = Single → Apply.
   - Assign the sprite to Sprite Renderer and position via Transform to frame the avatar.
- SMPLX model material: choose from `Assets/SMPLX/Materials` and drag onto `smplx-male/SMPLX-male/SMPLX-mesh-male`.

---

## Important Notes & Common Mistakes

- Use this link for extra tips - https://github.com/shams1802/GAME_DEV/tree/main/Unity/ClothChanger.
- Keep Alembic export scale at `100` to avoid tiny garments; keep scene scales at (1,1,1).
- Ensure EventSystem exists so UI clicks register; confirm TextMeshPro is present for labels.
- Use the correct sprites/materials (green/blue) to differentiate Shirt1 vs. Shirt2 visually.
<!-- - If garments fail to follow the rig, re-parent and reset transforms before Play. -->

---

## Final Result

- A cloth-led avatar experience: PoseController handles pose/UI toggles, AlembicClothingChanger manages garments, and the UI surfaces ClothPanel + RemoveCloth for fast swaps.
- Shirt1/Shirt2 display with images and colored materials; RemoveCloth clears outfits; ToggleUI syncs panel visibility and pose state automatically.