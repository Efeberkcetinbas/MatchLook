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
        //cube.transform.position = transform.position; // Immediately place the cube in the slot
    }

    public void ClearSlot()
    {
        OccupyingCube = null; // Mark the slot as unoccupied
    }

    // Check if the slot is occupied by a cube of the same color
    public bool IsOccupiedBySameColor(ColorsEnum colorEnum)
    {
        return OccupyingCube != null && OccupyingCube.colorEnum == colorEnum;
    }

    // Check if the slot is occupied by a different color
    public bool IsOccupiedByDifferentColor(ColorsEnum colorEnum)
    {
        return OccupyingCube != null && OccupyingCube.colorEnum != colorEnum;
    }
}
