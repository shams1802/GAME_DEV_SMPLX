# Unity Third-Person Bow & Arrow Combat System

A Unity project for a third-person bow and arrow combat system with realistic aiming, animation, and physics. Designed for action-adventure gameplay, supporting character movement, equipment, and combat mechanics.

---

## 📑 Table of Contents
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
  - [Phase 6: Equipment Management System](#phase-6-equipment-management-system)
  - [Phase 7: Audio and Ammunition System](#phase-7-audio-and-ammunition-system)
- [Animation State Machine](#animation-state-machine)
- [Physics Configuration](#physics-configuration)
- [Best Practices](#best-practices)
- [Troubleshooting](#troubleshooting)
- [Important Notes](#important-notes)

---

## Overview
Implements a third-person bow and arrow system with:
- Character movement and camera control
- Bow drawing, aiming, and arrow physics
- Animation state management
- Equipment and ammo handling

---

## Features
- Third-person movement (keyboard/controller)
- Camera collision handling
- Animator blend trees for walk/run
- Modular scripts for easy extension

---

## Project Structure
```plaintext
Assets
└── Assets
    └── Longbow
        ├── Animator
        ├── Materials
        ├── Textures
        └── (erika_archer.fbx + animations)
Scripts
├── Camera
└── Character
UI (to be added)
Scenes
````

---

## Prerequisites

* Unity 2020.3 LTS or newer
* Visual Studio / VS Code
* Mixamo Erica Archer model & animations
* Basic Unity & C# knowledge

---

## Quick Start

1. Import Erica Archer from Mixamo
2. Configure humanoid rig and animations
3. Set up Animator Controller
4. Add movement & camera scripts
5. Test locomotion and camera behavior

---

## Step-by-Step Implementation

---

## Phase 1: Project Foundation Setup

### Step 1: Import Character Assets

* Download **Erica Archer** from [https://www.mixamo.com/#/](https://www.mixamo.com/#/) or use provided `Longbow`
* Create the following structure:

```plaintext
Assets
└── Assets
    └── Longbow
        ├── Animator
        ├── Materials
        ├── Textures
        └── (erika_archer.fbx + 39 animation fbx files)
Scripts (to be added)
UI (to be added)
```

* Drag `erika_archer.fbx` into the **Hierarchy**

* Select `erika_archer.fbx` in Project window:

  * **Rig Tab**

    * Animation Type: `Humanoid`
    * Avatar Definition: `Create From This Model`
    * Click **Apply**
  * **Materials Tab**

    * Click **Extract Textures** → `Assets/Assets/Longbow/Textures`
    * Fix Normal Map popup
    * Click **Extract Materials** → `Assets/Assets/Longbow/Materials`

* For **every animation FBX**:

  * Rig → Humanoid
  * Avatar Definition → `Copy From Other Avatar`
  * Select `erika_archerAvatar`
  * Click **Apply**

---

### Step 2: Environment Setup

* Create empty GameObject `ENV`
* Add:

  * Plane (ground)
  * 4 Cubes (walls)
* Adjust scale and position

---

### Step 3: Player Hierarchy

* Create empty GameObject `Player`
* Reset Transform
* Drag `erika_archer` under Player

```plaintext
ENV
├── Plane
├── Cube1
├── Cube2
├── Cube3
└── Cube4
Player
└── erika_archer
```

---

### Step 4: Animator Controller

* In `Assets/Assets/Longbow/Animator`

  * Create Animator Controller `PlayerAnim`
* Select `Player`

  * Add **Animator** component
  * Assign:

    * Controller → `PlayerAnim`
    * Avatar → `erika_archerAvatar`

---

### Step 5: Animation Configuration

**Idle**

* Rename `standing idle 01` → `Idle`
* Loop Time ✔
* Loop Pose ✔
* Root Transform Position (Y): Bake Into Pose → Feet

**Walk (front/back/left/right)**

* Loop Time ✔
* Loop Pose ✔
* Root Rotation: Bake Into Pose → Original
* Root Position (Y): Bake Into Pose → Feet

**Run (front/back/left/right)**

* Same settings as Walk

---

### Step 6: Blend Trees

* Create two Blend Trees:

  * `Walk`
  * `Run`

**Animator Parameters**

* `float forward`
* `float strafe`
* `bool sprint` (default false)

**Walk Blend Tree**

* Type: 2D Simple Directional
* Parameters: `strafe`, `forward`
* Motions:

  * Idle (0,0)
  * Walk Forward (0,1)
  * Walk Back (0,-1)
  * Walk Left (-1,0)
  * Walk Right (1,0)

**Repeat same setup for Run**

---

### Step 7: Transitions

* Walk → Run:

  * Has Exit Time ❌
  * Condition: `sprint == true`
* Run → Walk:

  * Condition: `sprint == false`

⚠️ **Do not rename parameters** (`forward`, `strafe`, `sprint`)

---

## Phase 2: Character Movement and Camera Control

---

### Step 1: Script Setup

Create folders:

```plaintext
Scripts
├── Camera
└── Character
    ├── InputSystem.cs
    └── Movement.cs
```

* Copy `Movement.cs` from provided assets

---

### Step 2: Character Controller

* Select `Player`
* Add `Character.cs`
* Adjust Character Controller:

  * Center
  * Radius
  * Height
    to fit model capsule

---

### Step 3: Input System

* Copy `InputSystem.cs` from:
  `Assets/Scripts/InputSystem/1 just player input/`
* Attach to `Player`

**Input Manager**

* Edit → Project Settings → Input Manager
* Increase Axes size by 1
* Add:

  * Name: `Sprint`
  * Positive Button: `left shift`
  * Alt Negative Button: `right shift`

---

### Step 4: Camera Hierarchy

* Create empty `CameraHolder` at (0,0,0)
* Under it create empty `Center`
* Position Center at character hip
* Move `Main Camera` under `Center`

```plaintext
CameraHolder
└── Center
    ├── Main Camera
    └── CamPosition (later)
```

---

### Step 5: Camera Controller

* Create `CameraController.cs` in `Scripts/Camera`
* Copy from:
  `Assets/Scripts/Camera/1 just camera and player/`
* Attach to `CameraHolder`

---

### Step 6: Tagging

* Select `Player`
* Set Tag → `Player`
* Play and observe camera collision issue

---

### Step 7: Camera Collision Fix

* Replace `CameraController.cs` with:
  `Assets/Scripts/Camera/1 collision/CameraController.cs`

**CamPosition Setup**

* Create empty `CamPosition` under `Center`
* Copy **Transform component** from Main Camera
* Paste into CamPosition

**Camera**

* Adjust Clipping Planes as needed

✅ Camera collision is now resolved

---

### Phase 3: Weapon System Creation

This phase covers the creation and configuration of bow and arrow GameObjects, including proper hierarchy setup, positioning, and prefab creation.

#### Step 3.1: Create Bow and Arrow GameObjects

**Create Bow Object**
1. In the Hierarchy, right-click and select Create Empty
2. Rename the empty GameObject to "Bow"
3. Reset its transform to zero (Position: 0,0,0 / Rotation: 0,0,0 / Scale: 1,1,1)

**Add Bow 3D Model**
1. If using a custom bow model:
   - Import the bow FBX file into Assets/Models/Weapons/
   - Drag the bow model as a child of the Bow GameObject
2. If creating a placeholder:
   - Right-click Bow → 3D Object → Cube
   - Scale the cube to resemble a bow shape (e.g., X:0.1, Y:1.5, Z:0.1)
   - Add a cylinder for the string if desired

**Create Arrow Object**
1. Right-click Bow in Hierarchy → Create Empty
2. Name it "Arrow"
3. Position it at the bow's nocking point (approximately center of bowstring)

**Add Arrow 3D Model**
1. If using a custom arrow model:
   - Import arrow FBX into Assets/Models/Weapons/
   - Drag as child of Arrow GameObject
2. If creating placeholder:
   - Right-click Arrow → 3D Object → Cylinder
   - Rotate 90 degrees on Z-axis to orient horizontally
   - Scale to arrow proportions (X:0.05, Y:0.5, Z:0.05)
   - Add a cone at the tip for the arrowhead

**Add ArrowSpawn Point**
1. Right-click Bow → Create Empty
2. Name it "ArrowSpawnPoint"
3. Position it at the exact point where arrows should spawn when released
4. This is typically slightly forward of the bow to prevent clipping

#### Step 3.2: Position Bow on Character

**Locate Character Hand Bone**
1. Drag your character model into the Scene
2. In Hierarchy, expand the character GameObject
3. Navigate through the bone hierarchy to find the hand bone
4. Path typically: Character → Armature → Spine → Shoulder → UpperArm → LowerArm → Hand
5. Right-click the hand bone you found

**Attach Bow to Hand**
1. Drag the Bow GameObject onto the character's right hand bone in the Hierarchy
2. The Bow is now a child of the hand bone
3. Select the Bow GameObject
4. In the Scene view, use the transform tools to position the bow:
   - Position: Adjust so the bow grip aligns with the palm
   - Rotation: Rotate so the bow faces the correct direction
   - Scale: Adjust if the bow is too large or small for the character

**Fine-Tune Transform Values**
Manual adjustment example (values will vary based on your character):
```
Bow Transform (relative to hand bone):
Position: X: 0.05, Y: -0.1, Z: 0
Rotation: X: 90, Y: 0, Z: 0
Scale: X: 1, Y: 1, Z: 1
```

**Test in Play Mode**
1. Enter Play Mode
2. Move the character around
3. Verify the bow stays attached to the hand during animations
4. Exit Play Mode and readjust if necessary

#### Step 3.3: Create Weapon Prefabs

**Create Bow Prefab**
1. In Hierarchy, right-click the Bow GameObject and select "Copy"
2. Right-click in Project window at Assets/Prefabs/Weapons/ → "Paste"
3. Alternatively, drag Bow from Hierarchy into Assets/Prefabs/Weapons/
4. The Bow object in Hierarchy now shows blue text (prefab instance)
5. Name the prefab "BowPrefab"

**Create Arrow Prefab**
1. Detach Arrow from Bow temporarily (drag it out in Hierarchy)
2. Drag Arrow from Hierarchy into Assets/Prefabs/Weapons/
3. Name it "ArrowPrefab"
4. Delete the loose Arrow instance from Hierarchy (the original will remain in BowPrefab)

**Configure Arrow Prefab for Physics**
1. Double-click ArrowPrefab in Project window to open Prefab mode
2. Select the root Arrow GameObject
3. In Inspector, click "Add Component"
4. Add "Rigidbody" component
5. Configure Rigidbody settings:
   - Mass: 0.05 (lightweight)
   - Drag: 0.1
   - Angular Drag: 0.05
   - Use Gravity: Enabled
   - Is Kinematic: Disabled
6. Add "Capsule Collider" component
7. Configure Collider:
   - Radius: Match arrow shaft thickness
   - Height: Match arrow length
   - Direction: Z-Axis (arrow points forward)
8. Click "Auto Save" or save manually

**Verify Prefab Setup**
1. Exit Prefab mode
2. Drag ArrowPrefab into the Scene temporarily
3. Enter Play Mode
4. Verify the arrow falls due to gravity
5. Exit Play Mode and delete the test arrow

This completes the weapon system GameObject structure, ready for script integration in the next phase.

### Phase 4: Core Combat Scripting

This phase implements the fundamental C# scripts that control bow drawing, arrow spawning, and basic firing mechanics.

#### Step 4.1: Create BowController Script

**Generate Script File**
1. In Project window, navigate to Assets/Scripts/Weapon/
2. Right-click → Create → C# Script
3. Name it "Bow" (matching best practices)
4. Double-click to open in your code editor

**Implement Basic Bow Logic**
```csharp
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float arrowSpeed = 50f;
    
    [Header("Bow Settings")]
    public float maxDrawTime = 2f;
    public float maxDrawPower = 100f;
    
    private float currentDrawTime = 0f;
    private bool isDrawing = false;
    private GameObject drawnArrow;
    
    void Update()
    {
        HandleBowInput();
    }
    
    void HandleBowInput()
    {
        // Start drawing bow
        if (Input.GetButtonDown("Fire1"))
        {
            StartDrawing();
        }
        
        // Continue drawing
        if (Input.GetButton("Fire1") && isDrawing)
        {
            ContinueDrawing();
        }
        
        // Release arrow
        if (Input.GetButtonUp("Fire1") && isDrawing)
        {
            ReleaseArrow();
        }
    }
    
    void StartDrawing()
    {
        isDrawing = true;
        currentDrawTime = 0f;
        
        // Instantiate arrow at spawn point but don't fire yet
        drawnArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        drawnArrow.transform.SetParent(arrowSpawnPoint);
        
        // Disable arrow physics while drawing
        Rigidbody arrowRb = drawnArrow.GetComponent<Rigidbody>();
        if (arrowRb != null)
        {
            arrowRb.isKinematic = true;
        }
    }
    
    void ContinueDrawing()
    {
        // Increase draw time up to maximum
        currentDrawTime += Time.deltaTime;
        currentDrawTime = Mathf.Clamp(currentDrawTime, 0f, maxDrawTime);
        
        // Visual feedback: pull arrow back along bowstring
        float drawProgress = currentDrawTime / maxDrawTime;
        // Implement bowstring visual deformation here if needed
    }
    
    void ReleaseArrow()
    {
        if (drawnArrow == null) return;
        
        isDrawing = false;
        
        // Calculate shot power based on draw time
        float shotPower = (currentDrawTime / maxDrawTime) * maxDrawPower;
        
        // Detach arrow from bow
        drawnArrow.transform.SetParent(null);
        
        // Enable physics
        Rigidbody arrowRb = drawnArrow.GetComponent<Rigidbody>();
        if (arrowRb != null)
        {
            arrowRb.isKinematic = false;
            
            // Apply force in forward direction
            Vector3 shootDirection = arrowSpawnPoint.forward;
            arrowRb.AddForce(shootDirection * shotPower * arrowSpeed, ForceMode.Impulse);
        }
        
        // Reset draw state
        drawnArrow = null;
        currentDrawTime = 0f;
    }
}
```

#### Step 4.2: Create Arrow Script

**Generate Arrow Script**
1. In Assets/Scripts/Weapon/, create new C# Script
2. Name it "Arrow"
3. Open in code editor

**Implement Arrow Behavior**
```csharp
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow Settings")]
    public float lifetime = 10f;
    public int damage = 25;
    public bool stickToSurfaces = true;
    
    private bool hasHit = false;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Auto-destroy arrow after lifetime expires
        Destroy(gameObject, lifetime);
    }
    
    void FixedUpdate()
    {
        if (!hasHit && rb != null && rb.velocity.magnitude > 0.1f)
        {
            // Rotate arrow to face direction of travel
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        
        hasHit = true;
        
        // Stop arrow physics
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
        if (stickToSurfaces)
        {
            // Parent arrow to the object it hit
            transform.SetParent(collision.transform);
        }
        
        // Apply damage if object has health component
        // Example: collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);
        
        // Play impact sound
        // AudioSource.PlayClipAtPoint(impactSound, transform.position);
    }
}
```

#### Step 4.3: Attach Scripts to GameObjects

**Configure Bow Script**
1. In Hierarchy, select the Bow GameObject on your character
2. In Inspector, click "Add Component"
3. Search for "Bow" and add the script
4. Configure public fields:
   - Arrow Prefab: Drag ArrowPrefab from Project window
   - Arrow Spawn Point: Drag the ArrowSpawnPoint child object from Hierarchy
   - Arrow Speed: 50
   - Max Draw Time: 2
   - Max Draw Power: 100

**Configure Arrow Prefab Script**
1. Double-click ArrowPrefab in Project window
2. Select the root Arrow GameObject in Prefab mode
3. Add the "Arrow" component
4. Configure settings:
   - Lifetime: 10
   - Damage: 25
   - Stick To Surfaces: Checked

**Test Basic Firing**
1. Enter Play Mode
2. Press and hold left mouse button (Fire1)
3. Release to fire arrow
4. Observe arrow trajectory and physics
5. Verify arrow sticks to surfaces on impact
6. Exit Play Mode

This completes the core combat scripting foundation, enabling basic bow and arrow functionality.

### Phase 5: Advanced Aiming Mechanics

This phase implements sophisticated aiming systems including character rotation, spine adjustment, and dynamic targeting.

#### Step 5.1: Implement Camera-Based Aiming

**Create AimController Script**
1. Create new script: Assets/Scripts/Weapon/AimController.cs
2. This script will handle aim direction calculation

**Aim Direction Logic**
```csharp
using UnityEngine;

public class AimController : MonoBehaviour
{
    [Header("Aim Settings")]
    public Transform cameraTransform;
    public LayerMask aimLayerMask;
    public float maxAimDistance = 100f;
    
    [Header("References")]
    public Transform characterBody;
    public Transform bowTransform;
    
    private Vector3 currentAimPoint;
    
    void Update()
    {
        UpdateAimDirection();
        RotateCharacterToAim();
        RotateBowToAim();
    }
    
    void UpdateAimDirection()
    {
        // Raycast from camera to determine aim point
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, maxAimDistance, aimLayerMask))
        {
            currentAimPoint = hit.point;
        }
        else
        {
            // If no hit, aim at max distance point
            currentAimPoint = ray.GetPoint(maxAimDistance);
        }
    }
    
    void RotateCharacterToAim()
    {
        // Calculate direction from character to aim point (horizontal only)
        Vector3 aimDirection = currentAimPoint - characterBody.position;
        aimDirection.y = 0; // Keep character upright
        
        if (aimDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
            characterBody.rotation = Quaternion.Slerp(
                characterBody.rotation, 
                targetRotation, 
                Time.deltaTime * 10f
            );
        }
    }
    
    void RotateBowToAim()
    {
        // Rotate bow to point at aim location including vertical angle
        Vector3 bowAimDirection = currentAimPoint - bowTransform.position;
        
        if (bowAimDirection.magnitude > 0.1f)
        {
            Quaternion bowRotation = Quaternion.LookRotation(bowAimDirection);
            bowTransform.rotation = Quaternion.Slerp(
                bowTransform.rotation,
                bowRotation,
                Time.deltaTime * 15f
            );
        }
    }
    
    // Public method for other scripts to access aim point
    public Vector3 GetAimPoint()
    {
        return currentAimPoint;
    }
}
```

#### Step 5.2: Configure Aiming System

**Setup AimController on Character**
1. Select your character in Hierarchy
2. Add the AimController component
3. Assign references:
   - Camera Transform: Drag the main camera or camera follow target
   - Character Body: Drag the character's root transform
   - Bow Transform: Drag the Bow GameObject
   - Max Aim Distance: 100
4. Configure Layer Mask to include relevant collision layers

**Integrate with Bow Script**
Update the Bow.cs script to use aim direction:
```csharp
// Add at top of Bow class
private AimController aimController;

void Start()
{
    aimController = GetComponentInParent<AimController>();
}

// Modify ReleaseArrow method
void ReleaseArrow()
{
    if (drawnArrow == null) return;
    
    isDrawing = false;
    float shotPower = (currentDrawTime / maxDrawTime) * maxDrawPower;
    
    drawnArrow.transform.SetParent(null);
    Rigidbody arrowRb = drawnArrow.GetComponent<Rigidbody>();
    
    if (arrowRb != null)
    {
        arrowRb.isKinematic = false;
        
        // Use aim controller direction instead of transform.forward
        Vector3 shootDirection = (aimController.GetAimPoint() - arrowSpawnPoint.position).normalized;
        arrowRb.AddForce(shootDirection * shotPower * arrowSpeed, ForceMode.Impulse);
    }
    
    drawnArrow = null;
    currentDrawTime = 0f;
}
```

This provides precise aiming mechanics that follow the camera's view direction.

### Phase 6: Equipment Management System

This phase implements the ability to equip and unequip the bow using input controls and animation integration.

#### Step 6.1: Create Equipment Manager Script

**Generate EquipmentManager Script**
```csharp
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Equipment Settings")]
    public GameObject bowGameObject;
    public KeyCode equipToggleKey = KeyCode.E;
    
    [Header("Animation")]
    public Animator characterAnimator;
    public string equipBoolParameter = "BowEquipped";
    
    private bool isBowEquipped = false;
    
    void Start()
    {
        // Start with bow unequipped
        SetBowEquipped(false);
    }
    
    void Update()
    {
        HandleEquipmentInput();
    }
    
    void HandleEquipmentInput()
    {
        if (Input.GetKeyDown(equipToggleKey))
        {
            ToggleBowEquipped();
        }
    }
    
    void ToggleBowEquipped()
    {
        isBowEquipped = !isBowEquipped;
        SetBowEquipped(isBowEquipped);
    }
    
    void SetBowEquipped(bool equipped)
    {
        isBowEquipped = equipped;
        
        // Show or hide bow GameObject
        if (bowGameObject != null)
        {
            bowGameObject.SetActive(equipped);
        }
        
        // Update animator parameter
        if (characterAnimator != null)
        {
            characterAnimator.SetBool(equipBoolParameter, equipped);
        }
        
        // Enable/disable bow script
        Bow bowScript = bowGameObject?.GetComponent<Bow>();
        if (bowScript != null)
        {
            bowScript.enabled = equipped;
        }
    }
    
    public bool IsBowEquipped()
    {
        return isBowEquipped;
    }
}
```

#### Step 6.2: Integrate Equipment System

**Add to Character**
1. Select character in Hierarchy
2. Add EquipmentManager component
3. Configure:
   - Bow Game Object: Drag the Bow GameObject
   - Equip Toggle Key: E
   - Character Animator: Drag the Animator component
   - Equip Bool Parameter: "BowEquipped"

**Setup Animator Controller**
1. Open the CharacterAnimator controller
2. Create a new Bool parameter called "BowEquipped"
3. Create animation states or blend trees for:
   - Combat stance (bow equipped)
   - Normal stance (bow unequipped)
4. Create transitions based on BowEquipped parameter
5. Configure transition conditions and blend times

This allows seamless weapon switching with visual feedback.

### Phase 7: Audio and Ammunition System

This phase adds audio feedback for combat actions and implements an ammunition tracking system.

#### Step 7.1: Implement Audio System

**Create AudioManager for Bow**
```csharp
using UnityEngine;

public class BowAudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip drawSound;
    public AudioClip releaseSound;
    public AudioClip impactSound;
    
    [Header("Audio Settings")]
    public float volume = 1f;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }
    
    public void PlayDrawSound()
    {
        if (drawSound != null)
        {
            audioSource.PlayOneShot(drawSound);
        }
    }
    
    public void PlayReleaseSound()
    {
        if (releaseSound != null)
        {
            audioSource.PlayOneShot(releaseSound);
        }
    }
    
    public void PlayImpactSound(Vector3 position)
    {
        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound, position, volume);
        }
    }
}
```

**Integrate Audio into Bow Script**
Add to Bow.cs:
```csharp
private BowAudioManager audioManager;

void Start()
{
    audioManager = GetComponent<BowAudioManager>();
}

// In StartDrawing():
audioManager?.PlayDrawSound();

// In ReleaseArrow():
audioManager?.PlayReleaseSound();
```

#### Step 7.2: Implement Ammunition System

**Create AmmoManager Script**
```csharp
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    [Header("Ammo Settings")]
    public int maxAmmo = 30;
    public int currentAmmo = 30;
    
    [Header("UI")]
    public Text ammoText;
    
    void Start()
    {
        UpdateAmmoUI();
    }
    
    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }
    
    public bool ConsumeAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoUI();
            return true;
        }
        return false;
    }
    
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        UpdateAmmoUI();
    }
    
    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Arrows: {currentAmmo}/{maxAmmo}";
        }
    }
}
```

**Integrate Ammo System into Bow**
Modify Bow.cs StartDrawing():
```csharp
void StartDrawing()
{
    AmmoManager ammoManager = GetComponent<AmmoManager>();
    
    if (ammoManager != null && !ammoManager.HasAmmo())
    {
        // No ammo available, play empty sound or show notification
        return;
    }
    
    isDrawing = true;
    currentDrawTime = 0f;
    
    // Consume ammo
    ammoManager?.ConsumeAmmo();
    
    // Rest of the drawing logic...
}
```

**Create Ammo UI**
1. In Hierarchy: Right-click → UI → Canvas
2. Right-click Canvas → UI → Text (TextMeshPro if available)
3. Position text in corner of screen
4. Configure text appearance
5. Drag text element to AmmoManager component's Ammo Text field

This completes the full implementation of audio and resource management.

## Animation State Machine

**Recommended Animator States**
1. **Idle State** - Default resting pose
2. **Locomotion Blend Tree** - Walk/Run based on speed
3. **Combat Idle** - Armed stance with bow
4. **Draw Bow** - Pulling arrow back on bowstring
5. **Aim Bow** - Holding at full draw
6. **Release Bow** - Firing animation
7. **Equip/Unequip** - Weapon switching animations

**State Transitions**
- Idle ↔ Locomotion: Based on movement speed
- Idle → Combat Idle: BowEquipped = true
- Combat Idle → Draw Bow: isDrawing = true
- Draw Bow → Aim Bow: drawTime > threshold
- Aim Bow → Release Bow: Fire1 released
- Release Bow → Combat Idle: Animation complete

## Physics Configuration

**Arrow Rigidbody Settings**
- Mass: 0.05 kg (realistic arrow weight)
- Drag: 0.1 (air resistance)
- Angular Drag: 0.05 (rotation dampening)
- Use Gravity: Enabled (realistic trajectory)
- Interpolate: Interpolate (smooth visual movement)
- Collision Detection: Continuous Dynamic (fast-moving object)

**Collider Configuration**
- Type: Capsule Collider (matches arrow shape)
- Radius: Match arrow shaft thickness
- Height: Full arrow length including tip
- Is Trigger: Disabled (physical collision)

**Layer Setup**
Create layers for proper collision:
- Player (character)
- Weapon (bow/arrows)
- Enemy (targets)
- Environment (surfaces)

Configure Layer Collision Matrix in Project Settings → Physics.

## Best Practices

**Script Organization**
- Keep scripts modular and single-purpose
- Use public fields for designer-adjustable values
- Add [Header] attributes for Inspector organization
- Comment complex logic sections

**Performance Optimization**
- Use object pooling for arrows instead of Instantiate/Destroy
- Limit arrow lifetime to prevent accumulation
- Cache component references in Start() instead of repeated GetComponent()
- Use FixedUpdate for physics calculations, Update for input

**Animation Tips**
- Use animation events to trigger sounds and effects
- Blend between states with appropriate transition times
- Create sub-state machines for complex behaviors
- Use layers for upper/lower body separation

**Debugging Strategies**
- Use Debug.DrawRay() to visualize aim direction
- Add Debug.Log() statements to trace script execution
- Use Gizmos for visualizing spawn points and ranges
- Test with Time.timeScale adjustment for slow-motion debugging

## Troubleshooting

**Arrow Doesn't Fire**
- Verify ArrowPrefab is assigned in Bow component
- Check ArrowSpawnPoint is correctly positioned
- Ensure Fire1 input is configured in Project Settings → Input Manager
- Confirm arrow has Rigidbody component

**Bow Not Attaching to Hand**
- Verify bow is child of correct hand bone
- Check character has Humanoid rig
- Adjust bow local transform position/rotation
- Test with simple cube before using complex model

**Aiming Direction Incorrect**
- Confirm camera reference is assigned in AimController
- Check LayerMask includes ground and target layers
- Verify character rotation is not locked
- Test raycast visualization with Debug.DrawRay

**Animations Not Playing**
- Ensure Animator component is on character root
- Verify animation controller is assigned
- Check animation parameters are spelled correctly
- Confirm transitions have proper conditions

**No Sound Playing**
- Import audio files to Assets/Audio/
- Assign audio clips to BowAudioManager fields
- Check AudioListener is present in scene (usually on camera)
- Verify audio volume is not muted

**Ammo Not Decreasing**
- Confirm AmmoManager component is attached
- Check ConsumeAmmo() is called in StartDrawing()
- Verify currentAmmo has starting value
- Ensure UI text reference is assigned

## Important Notes

- ❗ Always test in Play Mode after making script changes
- ❗ Save scene frequently to prevent data loss
- ❗ Use version control (Git) to track changes
- ❗ Create backup copies before major refactoring
- ✅ Character must have Humanoid rig for proper bone attachment
- ✅ Arrow Prefab must have Rigidbody for physics simulation
- ✅ Camera raycast requires proper Layer configuration
- ✅ Animation states require matching parameter names
- ✅ Audio files should be in WAV or MP3 format
- ✅ All scripts should be in appropriate folder structure
- ✅ Test bow mechanics without animations first, then integrate
- ✅ Prototype with simple shapes before adding complex models
- ✅ Balance arrow speed and draw power for satisfying gameplay


## Notes

* Test frequently in Play Mode
* Do not rename Animator parameters
* Use Humanoid rig only
* Modular design allows easy extension
