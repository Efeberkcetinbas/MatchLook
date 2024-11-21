using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorsEnum
{
    Red,
    Blue,
    Yellow,
    Green,
    Purple //

}
public class People : MonoBehaviour
{
    public Color CubeColor { get; private set; }
    public ColorsEnum ColorEnum { get; private set; }
    public bool CanTap = true;

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    public Animator animator;

    public void Initialize(Color color, ColorsEnum colorEnum)
    {
        CubeColor = color;
        ColorEnum = colorEnum;
        skinnedMeshRenderer.material.color = color;
        
    }


}
