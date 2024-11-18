using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool IsOccupied => OccupyingCube != null;
    public Cube OccupyingCube { get; private set; }

    public void PlaceCube(Cube cube)
    {
        if (IsOccupied) return;

        OccupyingCube = cube;
        cube.transform.position = transform.position; // Snap the cube to this slot
    }

    public void ClearSlot()
    {
        OccupyingCube = null;
    }
}
