using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterRadar : MonoBehaviour,IHunter
{
    [SerializeField] private float lookInterval = 1f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Vector3 detectionBoxSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private LayerMask detectionLayer;

    private float timer = 0f;
    [SerializeField] private bool isLookingLeft = true;


    public void UpdateBehavior(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= lookInterval)
        {
            Look();
            timer = 0f;
        }
    }

    private void Look()
    {
        //DetectCubesInDirection();
    }

    
    private void DetectCubesInDirection(bool setCanTap)
    {
        //Will implement later
        /*
        Collider[] detectedObjects = Physics.OverlapBox(detectionCenter, detectionBoxSize / 2, Quaternion.identity, detectionLayer);

        Debug.Log($"Detected {detectedObjects.Length} cubes in direction {direction}");
        foreach (Collider obj in detectedObjects)
        {
            if (obj.TryGetComponent<Cube>(out Cube cube))
            {
                cube.canTap = setCanTap; // Update canTap based on current direction
                Debug.Log($"Set {cube.name} canTap = {setCanTap}");
            }
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the detection range for both directions
        Gizmos.color = Color.red; // Left
        Vector3 leftCenter = transform.position + Vector3.left * detectionRange;
        Gizmos.DrawWireCube(leftCenter, detectionBoxSize);

        Gizmos.color = Color.green; // Right
        Vector3 rightCenter = transform.position + Vector3.right * detectionRange;
        Gizmos.DrawWireCube(rightCenter, detectionBoxSize);
    }
}
