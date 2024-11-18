using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Color CubeColor { get; private set; }

    public void Initialize(Color color)
    {
        CubeColor = color;
        GetComponent<Renderer>().material.color = color;
    }
}
