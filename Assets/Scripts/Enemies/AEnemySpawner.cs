using UnityEngine;

public abstract class AEnemySpawner<T> : MonoBehaviour where T : APooledObject
{
    [SerializeField]
    protected ASpawnerConfig<T> m_Config = null;

    private float m_Timer = 0f;

    private void Update()
    {
        m_Timer += Time.deltaTime;
        float timeBetweenSpawns = m_Config.SpawnIntervalSeconds / GameplayManager.DifficultyLevel * 2f;
        if (m_Timer >= timeBetweenSpawns)
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
