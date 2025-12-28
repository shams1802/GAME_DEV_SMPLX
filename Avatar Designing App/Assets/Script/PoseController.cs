using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// A serializable class to hold rotation data for a single bone using its string name.
[System.Serializable]
public class BoneTransformData
{
    public string boneName;
    public Vector3 rotation;
}

// A ScriptableObject to define a complete pose.
[CreateAssetMenu(fileName = "New Custom Pose", menuName = "Animation/Custom Pose Asset")]
public class PoseAsset : ScriptableObject
{
    public List<BoneTransformData> boneTransforms = new List<BoneTransformData>();
}

public class PoseController : MonoBehaviour
{
    [Header("Character Setup")]
    public GameObject characterRoot;
    public Transform rootBoneForSlider;

    [Header("Pose Assets")]
    public List<PoseAsset> poses;
    [Tooltip("The specific T-Pose asset to apply when hiding the UI.")]
    public PoseAsset tPoseAsset;

    [Header("UI Elements")]
    [Tooltip("Main panel holding all the pose buttons.")]
    public GameObject posePanel;
    public GameObject clothPanel;
    public GameObject removeClothButton; // ✅ New RemoveCloth button
    [Tooltip("Panel for shape adjustments.")]
    public GameObject shapePanel;
    [Tooltip("Panel for facial expressions.")]
    public GameObject expPanel;

    private Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();

    void Awake()
    {
        if (characterRoot == null)
        {
            Debug.LogError("Character Root is not assigned.", this);
            return;
        }

        // Build bone map
        foreach (Transform child in characterRoot.GetComponentsInChildren<Transform>())
            boneMap[child.name] = child;

        if (rootBoneForSlider == null)
        {
            if (boneMap.ContainsKey("root")) rootBoneForSlider = boneMap["root"];
            else Debug.LogError("Could not find 'root' bone.", this);
        }

        // ✅ Initial state
        if (posePanel != null) posePanel.SetActive(true);
        if (clothPanel != null) clothPanel.SetActive(true);
        if (removeClothButton != null) removeClothButton.SetActive(true);
        if (shapePanel != null) shapePanel.SetActive(false);
        if (expPanel != null) expPanel.SetActive(false);
    }

    public void TogglePoseUIVisibility()
    {
        // Safety check
        if (posePanel == null || clothPanel == null || removeClothButton == null || shapePanel == null || expPanel == null)
        {
            Debug.LogWarning("One or more UI panels/buttons are not assigned in PoseController.", this);
            return;
        }

        // Determine current state
        bool isPoseCurrentlyActive = posePanel.activeSelf && clothPanel.activeSelf && removeClothButton.activeSelf;

        // ✅ Toggle both groups together
        posePanel.SetActive(!isPoseCurrentlyActive);
        clothPanel.SetActive(!isPoseCurrentlyActive);
        removeClothButton.SetActive(!isPoseCurrentlyActive);
        shapePanel.SetActive(isPoseCurrentlyActive);
        expPanel.SetActive(isPoseCurrentlyActive);

        // Optional: Apply T-Pose when switching to shape/exp mode
        if (isPoseCurrentlyActive && tPoseAsset != null)
        {
            ApplyPoseLogic(tPoseAsset);
        }
    }

    public void ApplyPose(int poseIndex)
    {
        if (poses == null || poseIndex < 0 || poseIndex >= poses.Count)
        {
            Debug.LogWarning("Invalid pose index.", this);
            return;
        }

        ApplyPoseLogic(poses[poseIndex]);
    }

    private void ApplyPoseLogic(PoseAsset poseToApply)
    {
        if (poseToApply == null)
        {
            Debug.LogWarning("Pose Asset is null.", this);
            return;
        }

        foreach (var boneData in poseToApply.boneTransforms)
        {
            if (boneMap.TryGetValue(boneData.boneName, out Transform boneTransform))
            {
                boneTransform.localEulerAngles = boneData.rotation;
            }
            else
            {
                Debug.LogWarning($"Bone '{boneData.boneName}' not found.", this);
            }
        }
    }

    public void SetRootRotation(float yRotation)
    {
        if (rootBoneForSlider != null)
        {
            Vector3 currentRotation = rootBoneForSlider.localEulerAngles;
            rootBoneForSlider.localEulerAngles = new Vector3(currentRotation.x, yRotation, currentRotation.z);
        }
    }
}
