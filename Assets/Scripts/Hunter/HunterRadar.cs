using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HunterRadar : MonoBehaviour,IHunter
{
     private float lookInterval;
    private float detectionRange;
    private float detectionRadius;
    private float rotationSpeed;
    private LayerMask detectionLayer;

    private float timer = 0f;

    /// <summary>
    /// Applies settings from the HunterSettings ScriptableObject.
    /// </summary>
    public void ApplySettings(HunterSettings settings)
    {
        lookInterval = settings.lookInterval;
        detectionRange = settings.detectionRange;
        detectionRadius = settings.detectionRadius;
        rotationSpeed = settings.rotationSpeed;
        detectionLayer = settings.detectionLayer;
    }

    /// <summary>
    /// Updates hunter behavior.
    /// </summary>
    public void UpdateBehavior(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= lookInterval)
        {
            DetectPeople();
            timer = 0f;
        }

        RotateHunter(deltaTime);
    }

    /// <summary>
    /// Rotates the hunter over time.
    /// </summary>
    private void RotateHunter(float deltaTime)
    {
        transform.Rotate(Vector3.up, rotationSpeed * deltaTime);
    }

    /// <summary>
    /// Detects all "People" objects within the detection radius.
    /// </summary>
    private void DetectPeople()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        Debug.Log($"Hunter detected {detectedObjects.Length} objects.");

        foreach (Collider obj in detectedObjects)
        {
            if (obj.TryGetComponent<People>(out People person))
            {
                Debug.Log($"Detected a person: {person.name}");
                HandlePersonDetection(person);
            }
        }
    }

    /// <summary>
    /// Handles logic when a person is detected.
    /// </summary>
    /// <param name="person">The detected person.</param>
    private void HandlePersonDetection(People person)
    {
        // Example logic: Prevent the person from being tapped
        person.CanTap = false;
        Debug.Log($"Set {person.name}'s canTap to false.");
    }

    /// <summary>
    /// Draws the detection range for debugging.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
