using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAssign : MonoBehaviour
{
    [System.Serializable]
    public class ColorCount
    {
        public ColorsEnum ColorEnum;
        public Color Color;
        public int Count;
    }

    public List<ColorCount> colorAssignments;
    private List<People> peopleToAssign;

    public void AssignColors(List<People> people)
    {
        peopleToAssign = new List<People>(people); // Clone the list

        foreach (var colorAssignment in colorAssignments)
        {
            for (int i = 0; i < colorAssignment.Count; i++)
            {
                if (peopleToAssign.Count == 0)
                {
                    Debug.LogWarning("Not enough people to assign all colors.");
                    return;
                }

                // Randomly pick a person to assign this color
                int randomIndex = Random.Range(0, peopleToAssign.Count);
                People person = peopleToAssign[randomIndex];
                person.Initialize(colorAssignment.Color, colorAssignment.ColorEnum);

                // Remove the assigned person from the list
                peopleToAssign.RemoveAt(randomIndex);
            }
        }

        // If there are leftover people, assign random colors
        foreach (var person in peopleToAssign)
        {
            var randomColor = colorAssignments[Random.Range(0, colorAssignments.Count)];
            person.Initialize(randomColor.Color, randomColor.ColorEnum);
        }
    }
}
