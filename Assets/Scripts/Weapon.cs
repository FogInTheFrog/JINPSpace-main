using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ProjectileMovement m_ProjectilePrefab = null;

    [SerializeField]
    private Transform m_Emitter = null;

    [SerializeField]
    private static float m_RateOfFire = 2f;

    private PrefabPool<ProjectileMovement> m_Pool = null;

    private Coroutine m_LimiterCoroutine = null;

    private void Awake()
    {
        m_Pool = PoolManager.Instance.GetPool(m_ProjectilePrefab);
    }

    public bool Fire()
    {
        if (m_LimiterCoroutine != null)
        {
            return false;
        }

        m_LimiterCoroutine = StartCoroutine(RateOfFireLimiterCoroutine());
        return true;       
    }

    private IEnumerator RateOfFireLimiterCoroutine()
    {
        ProjectileMovement projectileInstance = m_Pool.Get();
        projectileInstance.Restart(m_Emitter.position, m_Emitter.rotation);

        yield return new WaitForSeconds(1f / m_RateOfFire);

        m_LimiterCoroutine = null;
    }

    public static void SetNewRateOfFire(float x)
    {
        m_RateOfFire = x;
    }

    public static void Reset()
    {
        m_RateOfFire = 2f;
    }
}
