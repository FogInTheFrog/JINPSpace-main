using UnityEngine;
using UnityEngine.Events;
using System;

public struct DamageInfo
{
    public int DamageAmount;

    public DamageDealer Dealer;
}

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private int m_DamageMin = 20;

    [SerializeField]
    private int m_DamageMax = 30;


    public UnityEvent<bool, DamageInfo> OnObjectHit = new UnityEvent<bool, DamageInfo>();

    private void OnTriggerEnter(Collider other)
    {
        DamageHandler handler = other.GetComponent<DamageHandler>();
        bool applyDamage = handler != null;

        DamageInfo damageInfo = new DamageInfo()
        {
            DamageAmount = GetDamage(),
            Dealer = this
        };

        if (applyDamage)
        {
            handler.ApplyDamage(damageInfo);
            
        }

        OnObjectHit.Invoke(applyDamage, damageInfo);
    }

    private int GetDamage()
    {
        if (m_DamageMax < m_DamageMin)
        {
            Debug.LogError($"Max {m_DamageMax} is larger thatn Min {m_DamageMin}!");
            return 0;
        }

        int damage = UnityEngine.Random.Range(m_DamageMin, m_DamageMax);
        
        if (this.name == "Projectile(Clone)") {
            damage += DamageVar.AD * 5;
        }
        return damage;
    }

    // when we hit bonus
    public void IncreaseDamage()
    {
        Debug.Log("Attack Damage Increased");
        m_DamageMin += 10;
        m_DamageMax += 10;
    }
}
