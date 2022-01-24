using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBall : APooledObject
{
    [SerializeField]
    private DestructibleObject m_DestructibleObject = null;

    [SerializeField]
    private Vector3 m_MovementSpeed = new Vector3(0, 0, -20f);

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    public void Reset(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);

        m_DestructibleObject.Reset();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = m_Rigidbody.position + m_MovementSpeed * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(targetPosition);
    }

    public void HandleDestroyed()
    {
        ReturnToPool();
    }

}
