using System.Collections.Generic;
using UnityEngine;

public abstract class ASpawnerConfig<T> : ScriptableObject where T : APooledObject
{
    public List<T> Enemies = new List<T>();

    public float SpawnIntervalSeconds = 1f;

    public T GetRandomEnemy()
    {
        if (Enemies == null || Enemies.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, Enemies.Count);
        return Enemies[randomIndex];
    }
}

