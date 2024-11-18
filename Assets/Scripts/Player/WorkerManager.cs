using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public static WorkerManager Instance { get; private set; }

    private readonly List<IWorker> workers = new();

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
        foreach (var worker in workers)
        {
            worker.UpdateBehavior(Time.deltaTime);
        }
    }

    public void RegisterWorker(IWorker worker)
    {
        if (!workers.Contains(worker))
        {
            workers.Add(worker);
        }
    }

    public void UnregisterWorker(IWorker worker)
    {
        if (workers.Contains(worker))
        {
            workers.Remove(worker);
        }
    }
}
