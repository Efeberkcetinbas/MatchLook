using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;     // Slots where cubes are placed when tapped
    [SerializeField] private int matchCount = 3;    // Number of cubes needed for a match
    [SerializeField] private float moveDuration = 0.5f; // Duration for moving cubes to slots

    public void AddCubeToStack(Cube cube)
    {
        // Place the cube in the first available slot
        foreach (Slot slot in slots)
        {
            if (!slot.IsOccupied)  // Check if the slot is empty
            {
                PlaceCubeInSlot(slot, cube);  // Move cube to the slot
                CheckForMatches(cube.colorEnum);  // Check for matches based on the added cube's enum
                return;
            }
        }

        Debug.LogWarning("No available slots!");
    }

    private void PlaceCubeInSlot(Slot slot, Cube cube)
    {
        slot.PlaceCube(cube);  // Mark the slot as occupied and assign the cube

        // Animate the cube moving to the slot position
        cube.transform.DOMove(slot.transform.position, moveDuration).SetEase(Ease.OutBack);
    }

    private void CheckForMatches(ColorsEnum addedCubeType)
    {
        int typeCount = 0;

        // Count all cubes in the slots that match the type of the added cube
        foreach (Slot slot in slots)
        {
            Cube currentCube = slot.OccupyingCube;

            if (currentCube != null && currentCube.colorEnum == addedCubeType)
            {
                typeCount++;
            }
        }

        Debug.Log($"Count for {addedCubeType}: {typeCount}");

        // If the count reaches the match threshold, clear the matched cubes
        if (typeCount >= matchCount)
        {
            Debug.Log("MATCH: " + addedCubeType);
            StartCoroutine(ClearMatchedCubes(addedCubeType));  // Clear the matched cubes
        }
    }

    private IEnumerator ClearMatchedCubes(ColorsEnum matchedType)
    {
        yield return new WaitForSeconds(0.5f);  // Optional delay before clearing

        foreach (Slot slot in slots)
        {
            Cube currentCube = slot.OccupyingCube;

            if (currentCube != null && currentCube.colorEnum == matchedType)
            {
                Destroy(currentCube.gameObject);  // Destroy the matched cube
                slot.ClearSlot();  // Clear the slot
            }
        }
    }
}
