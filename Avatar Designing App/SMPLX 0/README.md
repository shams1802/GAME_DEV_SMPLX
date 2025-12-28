# Unity SMPL-X Avatar Pose App

This Unity project takes raw SMPL-X models and turns them into fully usable Unity prefabs that support pose manipulation via the `SMPLX` component. Start from exported SMPL-X models and finish with scene-ready prefabs whose poses you can adjust in real time.

## Table of Contents
- [Overview](#overview)
- [Starting Point](#starting-point)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Step-by-Step Setup](#step-by-step-setup)
	- [Step 1: Import SMPL-X Models](#step-1-import-smpl-x-models)
	- [Step 2: Load Models into Hierarchy](#step-2-load-models-into-hierarchy)
	- [Step 3: Convert Models to Prefabs](#step-3-convert-models-to-prefabs)
	- [Step 4: Add SMPLX Component](#step-4-add-smplx-component)
	- [Step 5: Use Prefab for Pose Control](#step-5-use-prefab-for-pose-control)
- [Asset Download](#asset-download)
- [Important Notes](#important-notes)

## Overview
Each SMPL-X model is imported, converted into a prefab, configured with `SMPLX.cs`, and assigned the correct body type (Male / Female / Neutral). Any prefab can then be dropped into a scene and posed instantly.

## Starting Point
- Source: SMPL-X models
- Included in those models: SMPL-X mesh, skeleton, pose/shape/expression support
- No retargeting or rig conversion required

## Project Structure
```
Assets/
├─ SMPLX/
│  ├─ Materials/
│  ├─ Models/
│  ├─ Prefabs/
│  ├─ Resources/
│  ├─ Scripts/
│  └─ Textures/
└─ Scenes/
	 └─ SampleScene.unity
```

## Getting Started
- Open the project in Unity 6.x
- Open `SampleScene`
- Confirm all SMPL-X assets import correctly

## Step-by-Step Setup
### Step 1: Import SMPL-X Models
1) Download the SMPL-X models from [Asset Download](#asset-download).
2) Place them in `Assets/`.

### Step 2: Load Models into Hierarchy
1) In the Project window, select all SMPL-X models.
2) Drag them into the Hierarchy (each appears as its own GameObject).

### Step 3: Convert Models to Prefabs
1) In the Hierarchy, select a model.
2) Drag it into `Assets/SMPLX/Prefabs/`.
3) Repeat for all models to create reusable prefabs.

### Step 4: Add SMPLX Component
For each prefab:
1) Click the prefab to open it.
2) In the Inspector, click Add Component and add `SMPLX.cs`.
3) Set Model Type to the correct value: Male, Female, or Neutral.
4) Repeat for the remaining prefabs.

### Step 5: Use Prefab for Pose Control
1) Drag the desired prefab from `SMPLX/Prefabs` into the scene.
2) Select the avatar in the Hierarchy.
3) Adjust pose parameters with the `SMPLX` component sliders in the Inspector; pose updates apply instantly.

## Asset Download

- The SMPL-X model must be obtained separately from the official SMPL-X website
  provided.

- Download link:
  https://smpl-x.is.tue.mpg.de/download.php

- Under **SMPL-X Unity Package**, click **Download SMPL-X Unity Project**.

- Import the downloaded package locally into Unity.
  If required, delete any conflicting `.meta` files before importing.

⚠️ **Maintain the proper Unity project structure when importing SMPL-X assets.**


## Important Notes
- Convert every model into a prefab.
- Attach `SMPLX.cs` to every prefab.
- Ensure Model Type matches the avatar mesh (Male/Female/Neutral).
- Use prefabs (not raw models) in scenes.