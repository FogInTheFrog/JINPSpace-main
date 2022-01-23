using UnityEngine;

public abstract class ASingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance = null;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();
            }

            return m_Instance;
        }
    }

    public static bool IsSpawned => m_Instance != null;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Debug.LogError($"Singleton of type {GetType()} already exists! Destroying illegal copy {name}");
            Destroy(gameObject);

            return;
        }

        m_Instance = this as T;
    }
}
