using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour
{
    public UnityEvent<DamageInfo> OnDamageApplied = new UnityEvent<DamageInfo>();

    public void ApplyDamage(DamageInfo damageInfo)
    {
        OnDamageApplied.Invoke(damageInfo);
    }
}
