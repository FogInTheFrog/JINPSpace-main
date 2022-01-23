using UnityEngine;

public class ProjectileMovement : APooledObject
{
    [SerializeField]
    private float m_MovementSpeed = 10f;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    [SerializeField]
    private TrailRenderer m_TrailRenderer = null;

    private void FixedUpdate()
    {
        Vector3 targetPosition = m_Rigidbody.position + transform.forward * m_MovementSpeed * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(targetPosition);
    }

    public void HandleObjectHit(bool applyDamage, DamageInfo damgeInfo)
    {
        ReturnToPool();
    }

    public void Restart(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        gameObject.SetActive(true);
        m_TrailRenderer.Clear();
    }
}
