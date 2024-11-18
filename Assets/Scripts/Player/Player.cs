using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;       // Main camera for detecting touch
    [SerializeField] private StackManager stackManager; // Reference to StackManager

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Cube tappedCube = hit.collider.GetComponent<Cube>();

                if (tappedCube != null)
                {
                    stackManager.AddCubeToStack(tappedCube);
                }
            }
        }
    }
}
