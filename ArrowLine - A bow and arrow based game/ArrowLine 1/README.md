# Unity Third-Person Bow & Arrow Combat System

A Unity project for a third-person bow and arrow combat system with realistic aiming, animation, and physics. Designed for action-adventure gameplay, supporting character movement, equipment, and combat mechanics.

##  Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Step-by-Step Implementation](#step-by-step-implementation)
    - [Phase 1: Project Foundation Setup](#phase-1-project-foundation-setup)
    - [Phase 2: Character Movement and Camera Control](#phase-2-character-movement-and-camera-control)
    - [Phase 3: Weapon System Creation](#phase-3-weapon-system-creation)
    - [Phase 4: Core Combat Scripting](#phase-4-core-combat-scripting)
    - [Phase 5: Advanced Aiming Mechanics](#phase-5-advanced-aiming-mechanics)
    - [Phase 6: Equipment Management](#phase-6-equipment-management)
    - [Phase 7: Audio and Ammunition](#phase-7-audio-and-ammunition)
- [Animation State Machine](#animation-state-machine)
- [Physics Configuration](#physics-configuration)
- [Troubleshooting](#troubleshooting)
- [Notes](#notes)

## Overview
Implements a third-person bow and arrow system with:
- Character movement and camera control
- Bow drawing, aiming, and arrow physics
- Animation state management
- Equipment and ammo management
- Audio feedback

## Features
- Third-person movement (WASD/controller)
- Camera collision and aim alignment
- Bow draw, aim, and release mechanics
- Arrow projectile physics and impact
- Animator-driven transitions (idle, walk, run, combat)
- Equip/unequip system
- Ammo UI and logic
- Modular scripts for easy extension

## Project Structure
`
Assets/
 Assets/Longbow/ (character, animations, materials, textures)
 Scripts/
    Camera/ (CameraController.cs)
    Character/ (InputSystem.cs, Movement.cs)
    Weapon/ (Bow.cs, Arrow.cs, etc.)
 Audio/ (SFX)
 Scenes/ (CombatTestScene)
`

## Prerequisites
- Unity 2020.3 LTS or newer
- Visual Studio/VS Code
- Mixamo Erica Archer character/animations
- Basic Unity and C# knowledge

## Quick Start
1. Download Erica Archer from Mixamo or use provided 'Longbow' assets
2. Set up project structure as above
3. Import character and animations
4. Add scripts and configure components
5. Set up animator controller with states and transitions
6. Test movement, aiming, and firing

## Step-by-Step Implementation

### Phase 1: Project Foundation Setup

#### Step 1: Download and Import Character Assets
- Download Erica Archer from https://www.mixamo.com/#/ or use provided 'Longbow'
- Structure: Assets/Assets/Longbow/ (Animator, Materials, Textures, FBX files)
- Drag erika_archer.fbx into Hierarchy
- Configure Rig: Animation Type Humanoid, Avatar Definition Create From This Model
- Extract Textures to Textures folder, Materials to Materials folder
- For each animation FBX: Rig Humanoid, Avatar Definition Copy From Other Avatar (erika_archerAvatar)

#### Step 2: Create Environment
- Create empty GameObject "ENV"
- Add Plane and 4 Cubes for ground and walls, adjust positions/scales

#### Step 3: Set Up Player Hierarchy
- Create empty "Player", reset transform
- Drag erika_archer under Player
- Hierarchy: ENV (Plane, Cubes), Player (erika_archer)

#### Step 4: Animator Setup
- In Assets/Assets/Longbow/Animator/, create Animator Controller "PlayerAnim"
- Add Animator component to Player, assign PlayerAnim controller and erika_archerAvatar

#### Step 5: Animation Configuration
- Open Animator window
- For standing idle 01: Rename to Idle, Loop Time on, Bake Pose Feet
- For walk animations: Rename, Loop Time on, Bake Rotation Original, Bake Position Feet
- For run animations: Same as walk

#### Step 6: Blend Trees
- Create Blend Tree states: Walk and Run
- Parameters: float forward, strafe; bool sprint
- Walk: 2D Simple Directional (strafe, forward), motions: Idle(0,0), Walk Forward(0,1), etc.
- Run: Similar setup

#### Step 7: Transitions
- Transition Walk to Run: sprint true
- Run to Walk: sprint false
- Test animations in Animator

### Phase 2: Character Movement and Camera Control

#### Step 1: Script Folders and Files
- Create Scripts/Camera/ and Scripts/Character/
- Add InputSystem.cs and Movement.cs to Character/
- Copy Movement.cs content from provided Assets/Scripts/Movement.cs

#### Step 2: Character Controller
- Add Character.cs script to Player (adds Character Controller component)
- Adjust Center, Radius, Height to fit model

#### Step 3: Input System
- Copy InputSystem.cs from provided Assets/Scripts/InputSystem/1 just player input/
- Add to Player
- Add Sprint axis in Input Manager: Name Sprint, Positive Button left shift

#### Step 4: Camera Setup
- Create CameraHolder (empty, position 0,0,0)
- Create Center under CameraHolder, position at hip
- Move Main Camera under Center, adjust position

#### Step 5: Camera Controller
- Add CameraController.cs to CameraHolder
- Copy content from provided Assets/Scripts/Camera/1 just camera and player/

#### Step 6: Tag and Test
- Tag Player as "Player"
- Test movement (collision issue expected)

#### Step 7: Collision Fix
- Replace CameraController.cs with content from Assets/Scripts/Camera/1 collision/
- Create CamPosition under Center, copy Main Camera transform values
- Adjust Main Camera clipping planes
- Test: collision removed

### Phase 3: Weapon System Creation
- Create Bow and Arrow GameObjects
- Position bow on hand bone
- Create prefabs with Rigidbody/CapsuleCollider on arrow

### Phase 4: Core Combat Scripting
- Bow.cs: Handles drawing, aiming, firing
- Arrow.cs: Flight rotation, collision sticking
- Attach scripts, configure fields

### Phase 5: Advanced Aiming Mechanics
- AimController.cs: Raycast from camera for aim point
- Rotate character and bow to aim

### Phase 6: Equipment Management
- EquipmentManager.cs: Toggle bow equip/unequip
- Update animator bool parameter

### Phase 7: Audio and Ammunition
- BowAudioManager.cs: Play draw/release sounds
- AmmoManager.cs: Track ammo, update UI

## Animation State Machine
**States:** Idle, Locomotion (Walk/Run blend trees), Combat Idle, Draw, Aim, Release, Equip/Unequip
**Parameters:** BowEquipped (bool), isDrawing (bool), Speed (float)
**Transitions:** Based on parameters and conditions

## Physics Configuration
**Arrow:** Rigidbody (mass 0.05, drag 0.1, gravity on), CapsuleCollider
**Layers:** Player, Weapon, Environment
**Collision Matrix:** Configure in Physics settings

## Troubleshooting
**Arrow not firing:** Check prefab assignment, input mapping, Rigidbody
**Bow not attached:** Ensure child of hand bone, adjust transform
**Aiming off:** Camera reference, LayerMask
**Animations not playing:** Animator assigned, parameters match
**No sound:** Audio clips assigned, AudioListener
**Ammo not decreasing:** AmmoManager attached, UI set

## Notes
- Test in Play Mode after changes
- Use version control (Git)
- Character must have Humanoid rig
- Arrow prefab needs Rigidbody
- Use simple shapes for prototyping
- Balance arrow speed/power for gameplay
- Modular scripts: easy to extend
### Phase 3: Weapon System Creation
- Create Bow and Arrow GameObjects
- Position bow on hand bone
- Create prefabs with Rigidbody/CapsuleCollider on arrow

### Phase 4: Core Combat Scripting
- Bow.cs: Handles drawing, aiming, firing
`csharp
public class Bow : MonoBehaviour {
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 50f;
    float drawTime, maxDrawTime = 2f, maxPower = 100f;
    GameObject drawnArrow;
    void Update() {
        if (Input.GetButtonDown("Fire1")) StartDraw();
        if (Input.GetButton("Fire1")) drawTime = Mathf.Min(drawTime + Time.deltaTime, maxDrawTime);
        if (Input.GetButtonUp("Fire1")) ReleaseArrow();
    }
    void StartDraw() {
        drawTime = 0f;
        drawnArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation, arrowSpawnPoint);
        drawnArrow.GetComponent<Rigidbody>().isKinematic = true;
    }
    void ReleaseArrow() {
        if (!drawnArrow) return;
        float power = (drawTime / maxDrawTime) * maxPower;
        drawnArrow.transform.SetParent(null);
        var rb = drawnArrow.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(arrowSpawnPoint.forward * power * arrowSpeed, ForceMode.Impulse);
        drawnArrow = null;
    }
}
`
- Arrow.cs: Flight rotation, collision sticking
`csharp
public class Arrow : MonoBehaviour {
    void FixedUpdate() {
        var rb = GetComponent<Rigidbody>();
        if (rb && rb.velocity.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    void OnCollisionEnter(Collision col) {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(col.transform);
    }
}
`
- Attach scripts, configure fields

### Phase 5: Advanced Aiming Mechanics
- AimController.cs: Raycast from camera for aim point
- Rotate character and bow to aim

### Phase 6: Equipment Management
- EquipmentManager.cs: Toggle bow equip/unequip
- Update animator bool parameter

### Phase 7: Audio and Ammunition
- BowAudioManager.cs: Play draw/release sounds
- AmmoManager.cs: Track ammo, update UI
