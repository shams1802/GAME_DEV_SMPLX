# Unity SMPL-X Avatar Designing Application

A comprehensive Unity-based avatar design and customization system built on SMPL-X models. This project enables real-time control of character pose, body shape, facial expressions, and dynamic clothing simulation, creating a complete avatar customization pipeline.

---

## 📑 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Asset Download](#asset-download)
- [Implementation Modules](#implementation-modules)
  - [Module 0: SMPL-X Model Setup](#module-0-smpl-x-model-setup)
  - [Module 1: Rigging and Pose Control](#module-1-rigging-and-pose-control)
  - [Module 2: Shape and Facial Expression Manipulation](#module-2-shape-and-facial-expression-manipulation)
  - [Module 3: Fabric Simulation](#module-3-fabric-simulation)
- [Complete System Integration](#complete-system-integration)
- [Important Notes](#important-notes)
- [Troubleshooting](#troubleshooting)
- [References](#references)

---

## Overview

This Unity project transforms raw SMPL-X GameDev 2 output models into fully functional, customizable avatars with comprehensive control systems. The application provides:

- **Real-time pose manipulation** through an intuitive UI
- **Dynamic body shape control** with 10 beta parameters
- **Facial expression control** with 10 expression parameters
- **Runtime clothing simulation** using Alembic-based fabric
- **Seamless integration** between all control systems

The system is designed for character customization menus, avatar editors, research tools, interactive demos, and game development applications requiring advanced character control.

---

## Features

✨ **Core Capabilities:**

- **Full-body pose control** with preset pose library
- **Real-time body shape manipulation** (10 beta parameters)
- **Facial expression control** (10 expression parameters)
- **Dynamic clothing system** with Alembic-based fabric simulation
- **Automatic UI generation** for shape and expression sliders
- **Modular architecture** allowing independent or integrated use
- **Multi-model support** (Male, Female, Neutral body types)
- **Grounding system** to keep characters properly positioned
- **Image-backed UI** with visual clothing previews
- **T-pose auto-reset** when switching control modes

---

## Project Structure

```
Avatar Designing App/
├── Assets/
│   ├── Editor/
│   │   └── PoseAssetCreator.cs
│   ├── Model/
│   │   ├── Shirt1.abc
│   │   └── Shirt2.abc
│   ├── Pics/
│   │   ├── Cloth/
│   │   │   └── Materials/
│   │   │       ├── Shirt1.mat
│   │   │       └── Shirt2.mat
│   │   └── Pose/
│   ├── Poses/
│   │   ├── Pose_ArmsUp.asset
│   │   ├── Pose_Running.asset
│   │   ├── Pose_SideKick.asset
│   │   ├── Pose_Sitting.asset
│   │   ├── Pose_Surprised.asset
│   │   └── Pose_T_Pose.asset
│   ├── Prefabs/
│   │   └── Slider.prefab
│   └── Script/
│       ├── AlembicClothingChanger.cs
│       ├── ExpressionController.cs
│       ├── PoseController.cs
│       └── ShapeController.cs
├── SMPLX 0/                    (Module 0: Setup Guide)
├── SMPLX 1 Rigging and Pose Control/
├── SMPLX 2 Shape And Facial Expression Manipulation/
└── SMPLX 3 Fabric Simulation/
```

---

## Getting Started

### Prerequisites

**Software Requirements:**
- Unity 2019.x or newer (Unity 2020.x+ recommended)
- TextMeshPro (usually preinstalled in modern Unity versions)
- Alembic package from Unity Registry (for fabric simulation)
- Blender (optional, for custom garment creation)

**SMPL-X Assets:**
- SMPL-X GameDev 2 output models
- Models must include SMPL-X mesh, skeleton, and blendshapes
- No retargeting or rig conversion required

**Required Blendshapes:**
- Body Shape: `Shape000` → `Shape009`
- Facial Expression: `Exp000` → `Exp009`

### Asset Download

📦 **SMPL-X GameDev 2 Models:**  
[Download from Google Drive](https://drive.google.com/drive/folders/1VGCQE2hc5W3VZN0CM3zILKSPlRSSl6Tv?usp=sharing)

---

## Implementation Modules

The project is divided into four progressive modules, each building upon the previous one. You can implement them sequentially or jump to specific modules based on your needs.

> 📖 **For detailed step-by-step instructions with screenshots, refer to the individual README files in each module folder.**

---

### Module 0: SMPL-X Model Setup

**Objective:** Import and prepare SMPL-X models for Unity integration.

This foundational module converts raw SMPL-X GameDev 2 output models into Unity prefabs with the SMPLX component for pose control.

**What You'll Learn:**
- Importing SMPL-X models into Unity
- Converting models to reusable prefabs
- Adding and configuring the SMPLX component
- Setting model types (Male, Female, Neutral)

**Key Deliverable:** Scene-ready SMPL-X prefabs with pose control capability

📂 **[View Detailed Guide →](SMPLX%200/)**

---

### Module 1: Rigging and Pose Control

**Objective:** Implement a UI-based pose control system with preset poses.

This module provides real-time full-body pose control through an interactive UI system with toggle-able panels for pose selection.

**Key Components:**
- `PoseController.cs` - Manages pose application and UI panel visibility
- Preset pose Assets (ArmsUp, Running, SideKick, Sitting, Surprised, T-Pose)
- UI system with PosePanel, ShapePanel, and ExpPanel

**What You'll Learn:**
- Creating and configuring PoseManager
- Building UI panels and buttons
- Connecting pose Assets to the controller
- Managing panel visibility with toggle functionality

**Key Deliverable:** Interactive pose control system with preset poses

📂 **[View Detailed Guide →](SMPLX%201%20Rigging%20and%20Pose%20Control/)**

---

### Module 2: Shape and Facial Expression Manipulation

**Objective:** Enable runtime body shape and facial expression control through dynamically generated sliders.

This module creates interactive sliders at runtime for real-time modification of body proportions and facial expressions.

**Core System Architecture:**

**SMPLX.cs** (The Model API)
- Exposes `float[10] betas` array for body shape control
- Exposes `float[10] expressions` array for facial expressions
- Provides methods to apply values to blendshapes

**ShapeController.cs** (Body Shape Manager)
- Dynamically generates 10 sliders at runtime (Beta 0–9)
- Updates SMPL-X `betas[]` array and applies mesh updates

**ExpressionController.cs** (Facial Expression Manager)
- Generates expression sliders (Exp 0–9)
- Applies facial blendshape changes in real time

**What You'll Learn:**
- Creating UI structure for dynamic sliders
- Building reusable slider prefabs
- Configuring ShapeManager with multiple controllers
- Runtime UI generation and interaction

**Key Deliverable:** Dynamic shape and expression control system with auto-generated UI

📂 **[View Detailed Guide →](SMPLX%202%20Shape%20And%20Facial%20Expression%20Manipulation/)**

---

### Module 3: Fabric Simulation

**Objective:** Add dynamic clothing system with Alembic-based garment switching.

This module integrates Alembic garments with the pose pipeline, enabling coordinated wardrobe changes during runtime.

**Features:**
- Fast garment swapping (Shirt1/Shirt2) powered by Alembic caches
- Image-backed buttons for visual wardrobe management
- Dedicated RemoveCloth action for clearing garments
- Scene-friendly hierarchy keeping garments following the rig

**Suggested Scene Hierarchy:**

```plaintext
SampleScene
└── smplx-male
  ├── SMPLX-male
  │   └── SMPLX-mesh-male
  └── root
Shirt1
Shirt2
PoseManager
ShapeManager
Canvas
  ├── PosePanel
  ├── ClothPanel
  ├── ShapePanel
  ├── ExpPanel
  ├── RotationSlider
  ├── RemoveCloth
  └── ToggleUI_Button
EventSystem
BackGround
```

**Workflow Overview:**

**Blender Pipeline:**
- Author, simulate, and bake garments per item
- Export as Alembic with correct unit scaling (Scale = 100)

**Unity Integration:**
- Import `.abc` files and install Alembic package
- Wire buttons and panels
- Connect `AlembicClothingChanger` to manage garments

**What You'll Learn:**
- Blender to Unity Alembic workflow
- Creating clothing UI with image-backed buttons
- Wiring cloth swapping logic
- Coordinating clothing with pose system

**Key Deliverable:** Complete clothing system integrated with pose control

📂 **[View Detailed Guide →](SMPLX%203%20Fabric%20Simulation/)**

---

## Complete System Integration

When all modules are implemented, you'll have a comprehensive avatar design system with:

✅ **Unified Control System:**
- Toggle between Pose and Shape/Expression modes
- Automatic T-pose reset when switching modes
- Coordinated panel visibility across all systems

✅ **Clothing Integration:**
- Garments automatically cleared when changing poses
- Visual clothing previews with image-backed buttons
- Quick remove functionality for all clothing items

✅ **Real-time Feedback:**
- Instant mesh updates for all modifications
- Automatic character grounding
- Smooth transitions between control modes

✅ **Modular Architecture:**
- Each system works independently
- Easy to extend with new features
- Clean separation of concerns

---

## Important Notes

### General Guidelines
- ⚠️ Begin strictly from SMPL-X Model
- ⚠️ Convert every model into a prefab before use
- ⚠️ Attach `SMPLX.cs` to every prefab
- ⚠️ Ensure Model Type matches avatar mesh (Male/Female/Neutral)
- ⚠️ Use prefabs (not raw models) in scenes
- ⚠️ Both SMPL-X root slots must point to the same root
- ✅ Works with all SMPL-X body types
- ✅ Customize poses, sliders, and UI layout freely

### Fabric Simulation Specific
- Keep Alembic export scale at `100` to avoid sizing issues
- Keep all scene scales at (1,1,1) to prevent drift
- Ensure EventSystem exists for UI interaction
- Confirm TextMeshPro is installed for labels
- Re-parent and reset transforms if garments don't follow rig
- Use correct sprites/materials (color-coded) to differentiate garments

### UI and Layout
- Always add **Vertical Layout Group** to panels for proper slider stacking
- Ensure all public Inspector fields are assigned before Play
- Use **Slider – TextMeshPro** for consistent UI hierarchy
- Verify camera and lighting present the avatar clearly

---

## Troubleshooting

### Model Jumps When Adjusting Values

**Cause:** `SnapToGroundPlane()` moves root to Y=0. Floor mismatch or scale differences cause vertical displacement.

**Solutions:**
- **Quick fix:** Comment out `smplxModel.SnapToGroundPlane()` in `ShapeController.cs`
- **Proper fix:** Ensure scene floor and character root start at Y=0
- Keep character and parent scales at (1,1,1)
- Check prefab overrides or Unity version differences affecting mesh bounds
- Snap to specific floor object instead of Y=0 if needed

### Missing Inspector References

**Problem:** NullReferenceException on Play

**Solution:** Verify all public fields in Inspector are properly assigned

### Overlapping Sliders

**Problem:** All sliders appear in center overlapping each other

**Solution:** Add **Vertical Layout Group** component to panels

### All Sliders Control Same Parameter

**Problem:** Incorrect lambda usage in loops

**Solution:** Use local index copy inside loops:
```csharp
int index = i;
```

### Garments Don't Follow Character

**Problem:** Clothing doesn't move with rig

**Solution:**
- Verify garments are parented to character rig
- Reset transforms before Play
- Ensure scales are (1,1,1) throughout hierarchy

### Missing UI Elements or Broken Layout

**Problem:** Missing label or broken UI structure

**Solution:** Use **Slider – TextMeshPro** and verify hierarchy contains both Slider and TextMeshProUGUI components

---

## References

### External Resources
- [SMPL-X Official Documentation](https://smpl-x.is.tue.mpg.de/)
- [Unity Alembic Package](https://docs.unity3d.com/Packages/com.unity.formats.alembic@latest)
- [TextMeshPro Documentation](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html)

### Related Projects
- SMPL-4 Pose App: [GitHub Repository](https:////github.com/shams1802/GAME_DEV/tree/main/Unity/SMPL%204%20pose%20app)
- ClothChanger Reference: [GitHub Repository](https://github.com/shams1802/GAME_DEV/tree/main/Unity/ClothChanger)

---

**Project Maintained By:** Shams Quamar, Tahseen Muneer Purray, Prajwal Pathode  
**Last Updated:** December 2025  
**Unity Version:** 2019.x - 2023.x  
<!-- **License:** [Specify your license] -->


### SMPL-X Notice

This project uses the SMPL-X body model for academic research purposes only.

The SMPL-X model, data, and software are NOT included in this repository.
They are subject to a separate non-commercial license by the
Max Planck Institute for Intelligent Systems / Max-Planck-Gesellschaft.

Users must obtain SMPL-X separately from the official source and agree
to its license terms.

SMPL-X Citation:
Pavlakos et al., "Expressive Body Capture: 3D Hands, Face, and Body from a Single Image",
CVPR 2019.


---

*For detailed step-by-step instructions with screenshots for each module, refer to the individual README files in the respective SMPLX folders.*