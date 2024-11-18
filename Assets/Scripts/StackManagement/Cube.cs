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
    public bool isLeft;
    public bool canTap=true;
    public ColorsEnum colorEnum;

    public void Initialize(Color color)
    {
        CubeColor = color;
        GetComponent<Renderer>().material.color = color;
    }

    public void RemoveFromFormation(Cube cube)
    {
        if(cube.isLeft)
        {
            transform.parent.GetComponent<CubeFormation>().leftCubes.Remove(cube.transform);
            transform.parent.GetComponent<Worker>().OnReOrderLeft();
        }
        else
        {
            transform.parent.GetComponent<CubeFormation>().rightCubes.Remove(cube.transform);
            transform.parent.GetComponent<Worker>().OnReOrderRight();
        }
    }
}
