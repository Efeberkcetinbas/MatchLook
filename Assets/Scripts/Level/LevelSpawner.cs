using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObject
    {
        public string tag;
        //public int count;
        public List<Vector3> positions;
    }

    public List<SpawnableObject> spawnableObjects;

    void Start()
    {
        
        foreach (var spawnableObject in spawnableObjects)
        {
            for (int i = 0; i < spawnableObject.positions.Count; i++)
            {
                if (i < spawnableObject.positions.Count)
                {
                    Vector3 position = spawnableObject.positions[i];
                    Quaternion rotation = Quaternion.identity;
                    ObjectPooler.Instance.SpawnFromPool(spawnableObject.tag, position, rotation);
                }
                else
                {
                    Debug.LogWarning($"Not enough positions provided for tag {spawnableObject.tag}.");
                }
            }
        }
    }
}
