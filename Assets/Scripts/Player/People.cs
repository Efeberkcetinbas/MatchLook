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
public class People : MonoBehaviour
{
    public Color CubeColor { get; private set; }
    public bool canTap=true;
    public ColorsEnum colorEnum;

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    public void Initialize(Color color)
    {
        CubeColor = color;
        skinnedMeshRenderer.material.color = color;
    }
 

 /*
 
 GameObject cubeObject = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity,transform);
            Cube cube = cubeObject.GetComponent<Cube>();
            
            // Assign the enum to the cube
            cube.colorEnum = colorsEnums[i];
            cube.isLeft=isLeft;


            // Initialize the cube with the assigned color
            cube.Initialize(colors[i]);
 
 */
}
