using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABonusSpawner<T> : MonoBehaviour where T : APooledObject
{
    [SerializeField]
    private T m_BonusPrefab = null;

    private float m_Timer = 0f;

    [SerializeField]
    protected float timeBetweenSpawns = 100f;

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= timeBetweenSpawns)
        {
            m_Timer = 0f;
            SpawnBonus();
        }
    }

    private void SpawnBonus()
    {
        PrefabPool<T> pool = PoolManager.Instance.GetPool(m_BonusPrefab);
        T bonus = pool.Get();

        SetupBonus(bonus);
    }

    protected abstract void SetupBonus(T bonus);
}
