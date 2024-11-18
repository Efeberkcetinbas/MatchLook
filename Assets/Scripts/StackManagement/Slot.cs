using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool IsOccupied => OccupyingCube != null;
    public Cube OccupyingCube { get; private set; }

    public void PlaceCube(Cube cube)
    {
        if (IsOccupied) return; // Prevent placing a cube in an occupied slot

        OccupyingCube = cube;
        //cube.transform.position = transform.position; // Snap the cube to this slot
    }

    public void ClearSlot()
    {
        OccupyingCube = null; // Mark the slot as unoccupied
    }
}
