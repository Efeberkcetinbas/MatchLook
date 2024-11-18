using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorsEnum
{
    blue,
    red,
    green,
    purple,
    pink,
    yellow

}

public class Cube : MonoBehaviour
{
    public Color CubeColor { get; private set; }
    public ColorsEnum colorEnum;

    public void Initialize(Color color)
    {
        CubeColor = color;
        GetComponent<Renderer>().material.color = color;
    }
}
