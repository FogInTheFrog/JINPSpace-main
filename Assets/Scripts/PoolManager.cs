using System.Collections.Generic;
using UnityEngine;

public class PoolManager : ASingleton<PoolManager>
{
    [SerializeField]
    private Dictionary<APooledObject, IPrefabPool> m_Pools = new Dictionary<APooledObject, IPrefabPool>();

    public PrefabPool<T> GetPool<T>(T prefab) where T : APooledObject
    {
        if (prefab == null)
        {
            return null;
        }

        IPrefabPool storedPool;
        PrefabPool<T> pool;

        if (m_Pools.TryGetValue(prefab, out storedPool))
        {
            pool = storedPool as PrefabPool<T>;

            if (pool == null)
            {
                throw new System.Exception($"Pool type mismatch for prefab {prefab.name}! Expected type {typeof(PrefabPool<T>)} got type {storedPool.GetType()}");
            }

            return pool;
        }

        pool = new PrefabPool<T>(prefab);
        m_Pools.Add(prefab, pool);

        return pool;
    }
}
