using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour
{
    public UnityEvent OnDestroyed = new UnityEvent();

    [SerializeField]
    private int m_TotalHealthpoints = 100;

    [SerializeField]
    private int m_CurrentHealthpoints = 0;

    [SerializeField]
    private int m_NumberOfResurrections = 0;

    private bool m_IsDestroyed = false;

    [Header("Floating text")]
    [SerializeField]
    private FloatingText m_FloatingTextPrefab = null;

    private PrefabPool<FloatingText> m_FloatingTextPool = null;

    [Header("Healthbar")]
    [SerializeField]
    private Vector3 m_HealthbarOffset;

    [SerializeField]
    private Healthbar m_HealthbarPrefab = null;

    private Healthbar m_Healthbar = null;

    // bonus generation purpouses
    private int bonusAD = 1;
    private int bonusAS = 2;
    private int bonusHP = 3;
    private int bonusMS = 4;
    
    private void Awake()
    {
        Reset();
        m_FloatingTextPool = PoolManager.Instance.GetPool(m_FloatingTextPrefab);
    }

    public void ApplyDamage(DamageInfo damageInfo)
    {
        if (m_IsDestroyed)
        {
            return;
        }

        InstantiateFloatingText(
            damageInfo.DamageAmount.ToString(),
            damageInfo.Dealer.transform.position);

        m_CurrentHealthpoints -= damageInfo.DamageAmount;

        if (m_Healthbar == null)
        {
            PrefabPool<Healthbar> healthbarPool = PoolManager.Instance.GetPool(m_HealthbarPrefab);

            m_Healthbar = healthbarPool.Get();
            m_Healthbar.Reset(transform, m_HealthbarOffset);
        }

        m_Healthbar.SetPercentage((float)m_CurrentHealthpoints / m_TotalHealthpoints);


        if (m_CurrentHealthpoints <= 0)
        {
            // Bonus generation purpouses
            int randomDrop = UnityEngine.Random.Range(1, 8);
            string textLvLUp = "";

            if (randomDrop == bonusAD) {
                DamageVar.LevelUpAD();
                textLvLUp = "Attack Damage Bonus";
            }
            else if (randomDrop == bonusAS)
            {
                DamageVar.LevelUpAS();
                textLvLUp = "Attack Speed Bonus";
            }
            else if (randomDrop == bonusHP)
            {
                DamageVar.LevelUpHP();
                textLvLUp = "Health Points Bonus";
            }
            else if (randomDrop == bonusMS)
            {
                DamageVar.LevelUpMS();
                textLvLUp = "Movement Speed Bonus";
            }
            else
            {
                textLvLUp = "No Drop";
            }

            InstantiateFloatingText(
            textLvLUp,
            GameObject.Find("Player").transform.position);
            m_IsDestroyed = true;

            m_Healthbar.ReturnToPool();
            m_Healthbar = null;

            
            OnDestroyed.Invoke();
        }
    }

    public void IncrementHP(int x)
    {
        m_CurrentHealthpoints += x;
        m_Healthbar.SetPercentage((float)m_CurrentHealthpoints / m_TotalHealthpoints);
    }

    private void InstantiateFloatingText(string text, Vector3 position)
    {
        FloatingText damageText = m_FloatingTextPool.Get();

        position += Random.insideUnitSphere;

        damageText.Reset(text, position);
    }

    public void Reset()
    {
        if (m_IsDestroyed)
        {
            m_CurrentHealthpoints = m_TotalHealthpoints + m_NumberOfResurrections * 20;
        }
        else
        {
            m_CurrentHealthpoints = m_TotalHealthpoints;
        }
        m_NumberOfResurrections++;
        m_IsDestroyed = false;
    }
}
