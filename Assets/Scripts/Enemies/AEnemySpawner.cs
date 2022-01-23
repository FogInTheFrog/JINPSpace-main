using UnityEngine;

public abstract class AEnemySpawner<T> : MonoBehaviour where T : APooledObject
{
    [SerializeField]
    private ASpawnerConfig<T> m_Config = null;

    private float m_Timer = 0f;

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_Config.SpawnIntervalSeconds)
        {
            m_Timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        T randomEnemyPrefab = m_Config.GetRandomEnemy();

        if (randomEnemyPrefab == null)
        {
            return;
        }

        PrefabPool<T> pool = PoolManager.Instance.GetPool(randomEnemyPrefab);
        T enemy = pool.Get();

        SetupEnemy(enemy);
    }

    protected abstract void SetupEnemy(T enemy);
}
