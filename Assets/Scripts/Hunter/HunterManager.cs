using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterManager : MonoBehaviour
{
    public static HunterManager Instance { get; private set; }

    private readonly List<IHunter> hunters = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        foreach (var worker in hunters)
        {
            worker.UpdateBehavior(Time.deltaTime);
        }
    }

    public void RegisterWorker(IHunter hunter)
    {
        if (!hunters.Contains(hunter))
        {
            hunters.Add(hunter);
        }
    }

    public void UnregisterWorker(IHunter hunter)
    {
        if (hunters.Contains(hunter))
        {
            hunters.Remove(hunter);
        }
    }
}
