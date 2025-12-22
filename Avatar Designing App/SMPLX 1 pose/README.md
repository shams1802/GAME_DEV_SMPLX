# Unity SMPL-X Character Pose & Shape Controller

This Unity project provides a minimal, repeatable pipeline to control pose, body shape, and expression of SMPL-X characters using a simple UI system.

The setup is derived from the SMPL-4 Pose App workflow, adapted to work only with the provided scripts and SMPL-X model set.

## 📑 Table of Contents
- [Overview](#overview)
- [Starting Point](#starting-point)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Step-by-Step Setup](#step-by-step-setup)
    - [Step 1: Import SMPL-X Models](#step-1-import-smpl-x-models)
    - [Step 2: Create Prefabs](#step-2-create-prefabs)
    - [Step 3: Configure PoseController](#step-3-configure-posecontroller)
    - [Step 4: Build the UI Panels](#step-4-build-the-ui-panels)
    - [Step 5: Connect the Panels](#step-5-connect-the-panels)
    - [Step 6: Add Shape Toggle Button](#step-6-add-shape-toggle-button)
- [Final Behavior](#final-behavior)
- [Important Notes](#important-notes)

## Overview
This project allows real-time control of:

- Full-body pose
- Body shape
- Facial expression

using three UI panels controlled by a single toggle button.

The logic is fully handled inside PoseController.cs, without introducing any new scripts.

## Starting Point
The starting point of this project is:

- ✔️ SMPL-X models already exported and placed inside the SMPLX/ folder
- ✔️ Scripts already provided (no new scripts added)
- ✔️ PoseController.cs taken from Before Button

This README explains how to go from that state to a working Pose + Shape + Expression UI app inside Unity.

## Project Structure
Your folder layout should remain as follows:

```
Assets/
├── SMPLX/
│   ├── smplx-male.fbx
│   ├── smplx-female.fbx
│   └── smplx-neutral.fbx
│
├── Scripts/
│   └── PoseController.cs   (from Before Button)
│
├── UI/
│   └── Canvas
│
└── Scenes/
        └── SampleScene.unity
```

⚠️ Do not add or rename scripts.
Use only the files already provided.

## Getting Started
- Open the project in Unity
- Open the target scene
- Ensure the SMPL-X models are correctly imported (Rig = Humanoid is not required)

## Step-by-Step Setup

### Step 1: Import SMPL-X Models
- Locate the SMPLX folder in Assets/
- Drag the required SMPL-X model(s) into the Hierarchy
- Reset transform (Position = 0, Rotation = 0, Scale = 1)

### Step 2: Create Prefabs
- Drag each SMPL-X model from Hierarchy → Project
- This creates reusable prefabs
- You may delete the scene instances afterward

### Step 3: Configure PoseController
- Use this link for better setup - https://github.com/shams1802/GAME_DEV/tree/main/Unity/SMPL%204%20pose%20app
- Create an empty GameObject
- Name it PoseManager
- Add PoseController.cs component
- Assign the following fields carefully:

#### SMPL-X Root Assignment (Important)
- In Pose Control Script:
    - SMPLX Root (Slot 1) → smplx-male/root
    - SMPLX Root (Slot 2) → smplx-male/root
- ⚠️ Both slots must reference smplx-male/root
    This is mandatory for correct pose & shape updates.

- Assign any pose data you want
- You may use poses provided in the folder
- Recommended: create your own pose values for better control

### Step 4: Build the UI Panels
1. Pose Panel (Already Exists)
     - PosePanel
     - Contains pose buttons
     - Visible by default

2. Shape Panel
     - Right-click Canvas → UI → Panel
     - Rename to ShapePanel
     - Match size & position exactly with PosePanel
     - Optional: set semi-transparent background color
     - Add sliders or buttons later if needed

3. Expression Panel
     - Right-click Canvas → UI → Panel
     - Rename to ExpPanel
     - Position it beside the ShapePanel
     - Keep it hidden initially

### Step 5: Connect the Panels
- Select your PoseManager object. In the Inspector, you will see three fields:
    - Drag your PosePanel from the Hierarchy into the Pose Panel slot.
    - Drag your ShapePanel from the Hierarchy into the Shape Panel slot.
    - Drag your ExpPanel from the Hierarchy into the Exp Panel slot.

### Step 6: Add Shape Toggle Button
- Right-click Canvas → UI → Button (TextMeshPro)
- Rename it ToggleShape_Button
- Set button text to: Shape
- Place it in a fixed, always-visible position

#### Connect Button Logic
- Select ToggleShape_Button
- In On Click ():
    - Click +
    - Drag PoseManager into object field
    - Select function:
        - PoseController → TogglePoseUIVisibility()

## Final Behavior
When you press Play:

- ✅ App starts with PosePanel visible
- ✅ ShapePanel and ExpPanel are hidden
- ✅ Clicking Shape button:
    - Hides PosePanel
    - Shows ShapePanel and ExpPanel
    - Character snaps to T-Pose
- ✅ Clicking Shape again:
    - Restores PosePanel
    - Hides Shape & Expression panels

All logic is handled inside PoseController.cs.

## Important Notes
- ❗ Do not add new scripts
- ❗ Do not replace SMPL-X models
- ❗ Both SMPL-X root slots must point to the same root
- ✅ You may customize poses, sliders, and UI layout freely
- ✅ Works with all SMPL-X body types (Male / Female / Neutral)