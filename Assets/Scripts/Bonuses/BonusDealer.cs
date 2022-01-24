using UnityEngine;
using UnityEngine.Events;

public class BonusDealer : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnPlayerHit = new UnityEvent();

    [SerializeField]
    private int m_BonusHP = 0;
    private void OnTriggerEnter(Collider other)
    {
        DestructibleObject destructibleObject = other.GetComponent<DestructibleObject>();

        if (destructibleObject != null && destructibleObject.IsPlayer)
        {
            int bonusHP = Random.Range((int)(0.66f * m_BonusHP), (int)(1.5f * m_BonusHP));
            destructibleObject.AddBonusHP(bonusHP);
            OnPlayerHit.Invoke();
            Debug.Log("Added " + bonusHP + " bonus hp to player");
        }

        
    }
}
