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
        // Find the first available slot to add the cube
        foreach (Slot slot in slots)
        {
            if (!slot.IsOccupied)  // If slot is available
            {
                PlaceCubeInSlot(slot, cube);
                CheckMatch();  // Check for a match after placing the cube
                return;
            }
        }

        // If no slots are available, trigger a warning
        Debug.LogWarning("No available slots!");
    }

    private void PlaceCubeInSlot(Slot slot, Cube cube)
    {
        slot.PlaceCube(cube);  // Place the cube in the slot

        // Animate the cube to move to the slot position using DOTween
        cube.transform.DOMove(slot.transform.position, moveDuration).SetEase(Ease.OutBack);
    }

    private void CheckMatch()
    {
        // Create a list to hold colors of all cubes in the stack
        List<Color> cubeColors = new List<Color>();

        // Loop through all slots and add the colors of the cubes
        foreach (var slot in slots)
        {
            Cube currentCube = slot.OccupyingCube;

            if (currentCube != null)
            {
                cubeColors.Add(currentCube.CubeColor);
            }
        }

        // Check if there are 3 or more cubes of the same color
        foreach (var cubeColor in cubeColors)
        {
            int colorCount = 0;

            // Count how many cubes have the same color
            foreach (var color in cubeColors)
            {
                if (color == cubeColor)
                {
                    colorCount++;
                    Debug.Log(colorCount);
                }
            }

            // If we find 3 or more cubes of the same color, trigger a match
            if (colorCount >= matchCount)
            {
                Debug.Log("MATCH");
                StartCoroutine(ClearMatchedCubes(cubeColor));  // Start the coroutine to clear matched cubes
                return;  // Exit after finding a match
            }
        }
    }

    private IEnumerator<UnityEngine.WaitForSeconds> ClearMatchedCubes(Color matchedColor)
    {
        // Wait for a short duration before clearing matched cubes (e.g., to show an animation)
        yield return new WaitForSeconds(0.5f);

        // Loop through all slots and clear cubes of the matched color
        foreach (var slot in slots)
        {
            Cube currentCube = slot.OccupyingCube;

            if (currentCube != null && currentCube.CubeColor == matchedColor)
            {
                // Destroy the matched cube
                Destroy(currentCube.gameObject);

                // Clear the slot (you should define ClearSlot() in your Slot class)
                slot.ClearSlot();
            }
        }
    }
}
