using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObjectWrapper
{
    void Return();
}

public class PooledObjectWrapper<T> : IPooledObjectWrapper where T : APooledObject
{
    private T m_PooledObject = null;

    private PrefabPool<T> m_Pool = null;


    public PooledObjectWrapper(T pooledObject, PrefabPool<T> pool)
    {
        m_PooledObject = pooledObject;
        m_Pool = pool;
    }

    public void Return()
    {
        m_Pool.Return(m_PooledObject);
    }
}

public abstract class APooledObject : MonoBehaviour
{
    private IPooledObjectWrapper m_Wrapper = null;

    public void Initialize(IPooledObjectWrapper wrapper)
    {
        m_Wrapper = wrapper;
    }

    public void ReturnToPool()
    {
        m_Wrapper.Return();
    }
}
