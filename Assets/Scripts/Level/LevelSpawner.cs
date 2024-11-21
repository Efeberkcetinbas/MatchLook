using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObject
    {
        public string tag;                     // Tag for ObjectPooler
        public List<Vector3> positions;       // Positions to spawn objects
    }

    [System.Serializable]
    public class HunterSpawnData
    {
        public Vector3 position;              // Position to spawn the hunter
        public HunterSettings hunterSettings; // ScriptableObject for hunter configuration
    }

    [Header("Spawn Settings")]
    public List<SpawnableObject> spawnableObjects;  // General spawnable objects
    public List<HunterSpawnData> hunterSpawnData;   // Specific spawn data for hunters

    [Header("References")]
    public ColorAssign colorAssigner;              // Reference to ColorAssigner

    private List<GameObject> spawnedObjects = new List<GameObject>(); // Track all spawned objects
    private List<People> spawnedPeople = new List<People>();          // Track only People objects
    private List<IHunter> spawnedHunters = new List<IHunter>();       // Track hunters for behavior updates

    void Start()
    {
        SpawnLevel();
    }

    /// <summary>
    /// Main method to handle the spawning of all objects.
    /// </summary>
    private void SpawnLevel()
    {
        CleanupLevel(); // Clean up previously spawned objects

        // Spawn general objects
        foreach (var spawnableObject in spawnableObjects)
        {
            SpawnObjects(spawnableObject);
        }

        // Spawn hunters with settings
        SpawnHunters();

        // Assign colors to spawned people
        AssignColorsToPeople();
    }

    /// <summary>
    /// Spawns objects based on the provided spawnableObject data.
    /// </summary>
    private void SpawnObjects(SpawnableObject spawnableObject)
    {
        foreach (var position in spawnableObject.positions)
        {
            GameObject spawnedObject = ObjectPooler.Instance.SpawnFromPool(spawnableObject.tag, position, Quaternion.identity);
            if (spawnedObject != null)
            {
                spawnedObjects.Add(spawnedObject);

                // Check if the object is a "People" and add it to the list
                if (spawnedObject.TryGetComponent<People>(out People person))
                {
                    spawnedPeople.Add(person);
                }
            }
            else
            {
                Debug.LogWarning($"Failed to spawn object with tag {spawnableObject.tag}.");
            }
        }
    }

    /// <summary>
    /// Spawns hunters using specific positions and settings.
    /// </summary>
    private void SpawnHunters()
    {
        foreach (var hunterData in hunterSpawnData)
        {
            GameObject spawnedHunter = ObjectPooler.Instance.SpawnFromPool("Hunter Spawner", hunterData.position, Quaternion.identity);
            if (spawnedHunter != null && hunterData.hunterSettings != null)
            {
                spawnedObjects.Add(spawnedHunter);

                // Assign settings to the HunterBehavior component
                if (spawnedHunter.TryGetComponent<HunterRadar>(out HunterRadar hunterBehavior))
                {
                    hunterBehavior.ApplySettings(hunterData.hunterSettings);
                    if (hunterBehavior is IHunter hunter)
                    {
                        spawnedHunters.Add(hunter); // Add to list for behavior updates
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Failed to spawn hunter at position {hunterData.position} with settings {hunterData.hunterSettings?.name}.");
            }
        }
    }

    /// <summary>
    /// Assigns colors to all spawned People objects.
    /// </summary>
    private void AssignColorsToPeople()
    {
        if (colorAssigner != null)
        {
            colorAssigner.AssignColors(spawnedPeople);
        }
        else
        {
            Debug.LogWarning("ColorAssigner is not assigned.");
        }
    }

    /// <summary>
    /// Cleans up all previously spawned objects by returning them to the pool.
    /// </summary>
    private void CleanupLevel()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
            {
                ObjectPooler.Instance.ReturnToPool(obj);
            }
        }

        spawnedObjects.Clear();
        spawnedPeople.Clear();
        spawnedHunters.Clear();
    }

    void Update()
    {
        // Update behavior for all spawned hunters
        foreach (var hunter in spawnedHunters)
        {
            hunter.UpdateBehavior(Time.deltaTime);
        }
    }
}
