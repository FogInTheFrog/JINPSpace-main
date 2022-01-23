using System.Collections.Generic;
using UnityEngine;

public interface IPrefabPool
{ 

}

public class PrefabPool<T> : IPrefabPool where T :APooledObject 
{
    private Queue<T> m_Pool = new Queue<T>();
    
    private T m_Prefab;

    public PrefabPool(T prefab)
    {
        m_Prefab = prefab;
    }

    public T Get()
    {
        T instance;

        if (m_Pool.Count > 0)
        {
            instance = m_Pool.Dequeue();
        }
        else
        {
            instance = GameObject.Instantiate(m_Prefab);

            PooledObjectWrapper<T> wrapper = new PooledObjectWrapper<T>(instance, this);
            instance.Initialize(wrapper);
        }

        return instance;
    }

    public void Return(T instance)
    {
        if (instance == null)
        {
            return;
        }

        instance.gameObject.SetActive(false);
        m_Pool.Enqueue(instance);
    }
}
