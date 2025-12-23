# Unity SMPL-X Runtime Shape & Expression Controller

This Unity project provides a complete and reusable system for **real-time body shape and facial expression control** for SMPL-X characters using an automatically generated UI.

Instead of manually editing blendshape values in the Inspector, this system creates **interactive sliders at runtime**, allowing users to modify the avatar’s body proportions and facial expressions while the game or application is running.

The solution is designed for **character customization menus**, **avatar editors**, **research tools**, and **interactive demos** where live feedback is required.

Built entirely inside **Unity**, the system works directly with **SMPL-X** models and integrates cleanly with existing pose or animation pipelines.

---

## 📑 Table of Contents

* [How It Works](#how-it-works)
* [Getting Started: Project Setup](#getting-started-project-setup)

  1. [Prerequisites](#1-prerequisites)
  2. [Scene Preparation](#2-scene-preparation)
* [Step-by-Step Implementation Guide](#step-by-step-implementation-guide)

  * [Step 1: Create the UI Structure](#step-1-create-the-ui-structure)
  * [Step 2: Create the Slider Prefab](#step-2-create-the-slider-prefab)
  * [Step 3: Add the Controller Scripts](#step-3-add-the-controller-scripts)
  * [Step 4: Configure Inspector References](#step-4-configure-inspector-references)
  * [Step 5: Runtime Testing](#step-5-runtime-testing)
* [Important Notes & Common Mistakes](#important-notes--common-mistakes)

---

## How It Works

The system is composed of **three main parts** that work together at runtime.

### `SMPLX.cs` (The Model API)

This script already exists as part of the SMPL-X Unity setup and acts as the **interface between code and mesh**.

It exposes:

* A `float[10] betas` array for body shape control
* A `float[10] expressions` array for facial expressions
* Methods to apply those values directly to blendshapes

It is responsible for:

* Updating the mesh
* Applying shape and expression values
* Adjusting the character’s height to stay grounded

---

### `ShapeController.cs` (Body Shape Manager)

This script dynamically generates **10 sliders** at runtime—one for each SMPL-X body shape (Beta 0–9).

Responsibilities:

* Instantiate sliders automatically
* Assign labels and ranges
* Listen for value changes
* Update the SMPL-X `betas[]` array
* Apply mesh updates instantly
* Snap the character back to the ground

---

### `ExpressionController.cs` (Facial Expression Manager)

This script mirrors the ShapeController but operates on **facial expressions**.

Responsibilities:

* Generate expression sliders (Exp 0–9)
* Update the `expressions[]` array
* Apply facial blendshape changes in real time

Both controllers are **fully independent**, making the system modular and easy to extend.

---

## Getting Started: Project Setup

Follow these steps starting from a **clean Unity scene**.

---

### 1. Prerequisites

Before implementing the system, ensure the following requirements are met:

* **Unity Project**

  * Unity 2019.x or newer recommended
  * UI system enabled

* **SMPL-X Model Imported**

  * Correctly imported `.fbx`
  * Mesh contains blendshapes:

    * `Shape000` → `Shape009`
    * `Exp000` → `Exp009`

* **SMPLX.cs Script Available**
  Must contain:

  ```csharp
  public float[] betas = new float[10];
  public float[] expressions = new float[10];

  public void SetBetaShapes();
  public void SetExpressions();
  public void SnapToGroundPlane();
  ```

* **TextMeshPro Installed**

  * Usually preinstalled in modern Unity versions

If you have already completed **SMPL-X Pose setup**, no additional configuration is required.

---

### 2. Scene Preparation

1. Create a **new 3D Scene**
2. Drag your **SMPL-X character** into the Hierarchy
3. Adjust the **Camera** and **Light** so the model is visible
4. Ensure the character is facing forward and grounded

---

## Step-by-Step Implementation Guide

### Step 1: Create the UI Structure

1. Right-click in Hierarchy → **UI → Canvas**

   * EventSystem will be created automatically

2. Under the Canvas, create **two empty UI Panels**:

   * `ShapePanel`
   * `ExpPanel`

3. Position the panels as desired (left/right or stacked)

4. Add a **Vertical Layout Group** to both panels:

   * This ensures sliders stack correctly
   * Adjust Padding and Spacing as needed

---

### Step 2: Create the Slider Prefab

1. Right-click **ShapePanel** → **UI → Slider – TextMeshPro**

2. Adjust:

   * Width / height
   * Font size
   * Colors (optional)

3. Add a **Layout Element** component:

   * Set **Min Height = 30** (recommended)

4. Drag the configured slider into:

   ```
   Assets/Prefabs/
   ```

5. Delete the slider from the scene
   (Only the prefab is needed)

---

### Step 3: Add the Controller Scripts

1. Create an empty GameObject → rename to **ShapeManager**
2. Attach:

   * `ShapeController.cs`
   * `ExpressionController.cs`

These scripts will handle **all runtime UI generation**.

---

### Step 4: Configure Inspector References

Select **ShapeManager** and assign the following fields:

#### ShapeController

* **SMPLX Model** → drag the SMPL-X character
* **Shape Panel** → drag `ShapePanel`
* **Slider Prefab** → drag the slider prefab

#### ExpressionController

* **SMPLX Model** → same character
* **Exp Panel** → drag `ExpPanel`
* **Slider Prefab** → same prefab

⚠️ All fields **must be assigned** before pressing Play.

---

### Step 5: Runtime Testing

1. Press **Play**
2. Sliders are generated automatically
3. Adjusting a slider:

   * Updates the mesh instantly
   * Changes body proportions or facial expressions
   * Keeps the character grounded

✅ No manual UI wiring is required.

---

## Important Notes & Common Mistakes

### ❌ Model jumps when posing/shape/exp changes

*Cause:* `SnapToGroundPlane()` moves the root to Y=0. If your floor is not at Y=0 or scales differ, the model can pop upward (seen between laptops).

*Fix options:*
- **Disable auto-snap (quick):** In `ShapeController.cs`, comment out `smplxModel.SnapToGroundPlane()` so sliders don’t move the root.
- **Match grounding:** Ensure the scene floor and character root start at Y=0; keep character and parent scales at (1,1,1).
- **Prefab parity:** Check prefab overrides or Unity version differences that might change mesh bounds; verify “Auto Snap to Ground”/similar toggle is consistent across machines.
- **Alternate approach:** If needed, snap to a specific floor object instead of Y=0.

### ❌ Missing Inspector References

**Problem:**
NullReferenceException on Play

**Solution:**
Ensure all public fields are assigned correctly.

---

### ❌ No Layout Group on Panels

**Problem:**
All sliders overlap in the center

**Solution:**
Always add a **Vertical Layout Group** to panels.

---

### ❌ Incorrect Lambda Usage in Loops

**Problem:**
All sliders control the same parameter

**Solution:**
Use a local index copy inside loops:

```csharp
int index = i;
```

---

### ❌ Incorrect Slider Prefab

**Problem:**
Missing label or broken UI

**Solution:**
Use **Slider – TextMeshPro** and verify hierarchy contains:

* `Slider`
* `TextMeshProUGUI`

---

## Final Result

After completing this setup, you will have:

* Fully dynamic **shape & expression UI**
* Real-time SMPL-X mesh updates
* Clean, modular controller scripts
* A reusable system that integrates easily with:

  * Pose control
  * Cloth toggling
  * Character customization menus

This system is designed to scale and can be extended with **pose, clothing, or animation controls** without restructuring the core logic.
