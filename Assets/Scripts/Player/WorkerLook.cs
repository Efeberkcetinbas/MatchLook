using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerLook : MonoBehaviour,IWorker
{
    [SerializeField] private float lookInterval = 1f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Vector3 detectionBoxSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private LayerMask detectionLayer;

    private float timer = 0f;
    [SerializeField] private bool isLookingLeft = true;

    //TEMP
    public GameObject left,right;

    private void Start()
    {
        WorkerManager.Instance.RegisterWorker(this);
        Look();
    }

    private void OnDestroy()
    {
        WorkerManager.Instance.UnregisterWorker(this);
    }

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
        if (isLookingLeft)
        {
            Debug.Log($"{name} is looking left");
            DetectCubesInDirection(Vector3.left, false);
            DetectCubesInDirection(Vector3.right, true);
            left.SetActive(true);
            right.SetActive(false);
        }
        else
        {
            Debug.Log($"{name} is looking right");
            DetectCubesInDirection(Vector3.right, false);
            DetectCubesInDirection(Vector3.left, true);
            left.SetActive(false);
            right.SetActive(true);
        }

        isLookingLeft = !isLookingLeft;
    }

    private void DetectCubesInDirection(Vector3 direction, bool setCanTap)
    {
        // Calculate the center of the detection box
        Vector3 detectionCenter = transform.position + direction * detectionRange;

        // Perform OverlapBox detection
        Collider[] detectedObjects = Physics.OverlapBox(detectionCenter, detectionBoxSize / 2, Quaternion.identity, detectionLayer);

        Debug.Log($"Detected {detectedObjects.Length} cubes in direction {direction}");
        foreach (Collider obj in detectedObjects)
        {
            if (obj.TryGetComponent<Cube>(out Cube cube))
            {
                cube.canTap = setCanTap; // Update canTap based on current direction
                Debug.Log($"Set {cube.name} canTap = {setCanTap}");
            }
        }
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
