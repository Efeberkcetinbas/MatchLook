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
        // Find the first available slot for this color
        for (int i = 0; i < slots.Count; i++)
        {
            Slot currentSlot = slots[i];

            if (currentSlot.IsOccupiedBySameColor(cube.colorEnum)) // If same color cube is already there
            {
                // Shift cubes to the next available slot
                ShiftCubeToNextAvailableSlot(i, cube);
                return;
            }
            else if (!currentSlot.IsOccupied) // If the slot is empty
            {
                currentSlot.PlaceCube(cube); // Place the cube here
                cube.transform.DOMove(currentSlot.transform.position, moveDuration).SetEase(Ease.OutBack);
                CheckForMatches(cube.colorEnum);  // Check for matches after placement
                return;
            }
        }

        Debug.LogWarning("No available slots!");
    }

    private void ShiftCubeToNextAvailableSlot(int startIndex, Cube newCube)
    {
        bool placed = false;

        // Start from the next slot and continue until you find an empty slot
        for (int i = startIndex + 1; i < slots.Count; i++)
        {
            Slot currentSlot = slots[i];

            // If the slot is empty, place the cube there
            if (!currentSlot.IsOccupied)
            {
                currentSlot.PlaceCube(newCube);
                newCube.transform.DOMove(currentSlot.transform.position, moveDuration).SetEase(Ease.OutBack);
                placed = true;
                break;
            }
            // If the slot is occupied by a different color, shift the existing cube to the next available slot
            else if (currentSlot.IsOccupiedByDifferentColor(newCube.colorEnum))
            {
                Cube cubeToShift = currentSlot.OccupyingCube;
                currentSlot.ClearSlot();

                // Find the next available slot for the shifted cube
                for (int j = i + 1; j < slots.Count; j++)
                {
                    Slot nextSlot = slots[j];
                    if (!nextSlot.IsOccupied)
                    {
                        nextSlot.PlaceCube(cubeToShift);
                        cubeToShift.transform.DOMove(nextSlot.transform.position, moveDuration).SetEase(Ease.OutBack);
                        break;
                    }
                }

                // Now place the new cube in the current slot
                currentSlot.PlaceCube(newCube);
                newCube.transform.DOMove(currentSlot.transform.position, moveDuration).SetEase(Ease.OutBack);
                placed = true;
                break;
            }
        }

        // After shifting and placing cubes, check for matches
        if (placed)
        {
            // Wait for the animation to finish before checking for matches
            StartCoroutine(WaitForAnimationToCompleteAndCheckForMatches(newCube.colorEnum));
        }
    }

    private IEnumerator WaitForAnimationToCompleteAndCheckForMatches(ColorsEnum addedCubeType)
    {
        yield return new WaitForSeconds(moveDuration); // Wait for the move animation to complete
        CheckForMatches(addedCubeType);  // Now check for matches after the shift has finished
    }

    private void CheckForMatches(ColorsEnum addedCubeType)
    {
        
        List<Slot> matchedSlots = new List<Slot>();

        // Count all cubes in the slots that match the type of the added cube
        foreach (Slot slot in slots)
        {
            Cube currentCube = slot.OccupyingCube;

            if (currentCube != null && currentCube.colorEnum == addedCubeType)
            {
                matchedSlots.Add(slot);  // Add the slot to the matched list if the color matches
            }
        }

        Debug.Log($"Matching cubes for {addedCubeType}: {matchedSlots.Count}");

        // If the count reaches the match threshold, clear the matched cubes
        if (matchedSlots.Count >= matchCount)
        {
            Debug.Log("MATCH: " + addedCubeType);
            StartCoroutine(ClearMatchedCubes(matchedSlots));  // Clear the matched cubes
        }
    }

    private IEnumerator ClearMatchedCubes(List<Slot> matchedSlots)
    {
        yield return new WaitForSeconds(0.5f);  // Optional delay before clearing

        foreach (Slot slot in matchedSlots)
        {
            Cube currentCube = slot.OccupyingCube;

            if (currentCube != null)
            {
                Destroy(currentCube.gameObject);  // Destroy the matched cube
                slot.ClearSlot();  // Clear the slot
            }
        }

        // Optionally, you can trigger any additional effects, such as filling the cleared slots.
    }
}
