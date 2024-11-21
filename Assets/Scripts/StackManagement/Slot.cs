using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool IsOccupied => OccupyingPeople != null;
    public People OccupyingPeople { get; private set; }

    public void PlaceCube(People people)
    {
        if (IsOccupied) return; // Prevent placing a cube in an occupied slot

        OccupyingPeople = people;
        //cube.transform.position = transform.position; // Immediately place the cube in the slot
    }

    public void ClearSlot()
    {
        OccupyingPeople = null; // Mark the slot as unoccupied
    }

    // Check if the slot is occupied by a cube of the same color
    public bool IsOccupiedBySameColor(ColorsEnum colorEnum)
    {
        return OccupyingPeople != null && OccupyingPeople.ColorEnum == colorEnum;
    }

    // Check if the slot is occupied by a different color
    public bool IsOccupiedByDifferentColor(ColorsEnum colorEnum)
    {
        return OccupyingPeople != null && OccupyingPeople.ColorEnum != colorEnum;
    }
}
