using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HunterSettings", menuName = "Data/HunterSettings")]
public class HunterSettings : ScriptableObject
{
    [Header("Detection Settings")]
    public float detectionRange = 5f;
    public float detectionRadius = 3f;
    public LayerMask detectionLayer;
    public Vector3 detectionBoxSize = new Vector3(2f, 2f, 2f);

    [Header("Rotation Settings")]
    public float rotationSpeed = 30f;
    public float lookInterval = 0.5f;
}