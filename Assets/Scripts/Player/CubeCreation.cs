using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCreation : MonoBehaviour
{
    [SerializeField] private List<Color> colors = new List<Color>();
    [SerializeField] private int cubeCounter;
    [SerializeField] private Worker worker;

    private void Start()
    {
        worker.SpawnCubes(cubeCounter,colors);
    }
}
