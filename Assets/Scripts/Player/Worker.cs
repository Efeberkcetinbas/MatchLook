using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Worker : MonoBehaviour
{
    [SerializeField] private Transform leftSpawnPoint;  // Position for spawning cubes on the left
    [SerializeField] private Transform rightSpawnPoint; // Position for spawning cubes on the right
    [SerializeField] private GameObject cubePrefab;     // Cube prefab to instantiate
    [SerializeField] private float spawnAnimationDuration = 0.5f; // Animation duration for the spawn
    [SerializeField] private float spawnSpacing = 0.5f; // Vertical spacing between spawned cubes

    // This method spawns cubes on both sides
    public void SpawnCubes(int totalCubes, List<Color> colors, List<ColorsEnum> colorsEnums)
    {
        // Ensure the total number of cubes matches the color list length
        if (totalCubes != colors.Count)
        {
            Debug.LogError("The number of cubes must equal the number of colors in the list!");
            return;
        }

        int cubesPerSide = totalCubes / 2;

        // Check if the total cubes is even
        if (totalCubes % 2 != 0)
        {
            Debug.LogError("Total cubes must be divisible by 2!");
            return;
        }

        // Assign colors to both sides
        List<Color> leftColors = new List<Color>(colors.GetRange(0, cubesPerSide));  // First half for left stack
        List<Color> rightColors = new List<Color>(colors.GetRange(cubesPerSide, cubesPerSide)); // Second half for right stack

        // Assign enums to both sides
        List<ColorsEnum> leftEnums = new List<ColorsEnum>(colorsEnums.GetRange(0, cubesPerSide));  // First half for left enums
        List<ColorsEnum> rightEnums = new List<ColorsEnum>(colorsEnums.GetRange(cubesPerSide, cubesPerSide)); // Second half for right enums

        // Spawn cubes on both sides
        SpawnSideCubes(leftSpawnPoint, leftColors, leftEnums);
        SpawnSideCubes(rightSpawnPoint, rightColors, rightEnums);
    }

    // Spawn cubes for each side
    private void SpawnSideCubes(Transform spawnPoint, List<Color> colors, List<ColorsEnum> colorsEnums)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            GameObject cubeObject = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity,transform);
            Cube cube = cubeObject.GetComponent<Cube>();
            
            // Assign the enum to the cube
            cube.colorEnum = colorsEnums[i];

            // Initialize the cube with the assigned color
            cube.Initialize(colors[i]);

            // Set the position for the spawn and animate the cube's appearance
            Vector3 targetPosition = spawnPoint.position + new Vector3(0, i * spawnSpacing, 0);
            cubeObject.transform.DOMove(targetPosition, spawnAnimationDuration).SetEase(Ease.OutBack);
        }
    }
}
