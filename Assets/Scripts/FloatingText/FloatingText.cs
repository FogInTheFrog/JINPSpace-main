using UnityEngine;

public class FloatingText : APooledObject
{
    [SerializeField]
    private TextMesh m_TextMesh = null;

    public void SetText(string text)
    {
        m_TextMesh.text = text;
    }

    public void Reset(string text, Vector3 position)
    {
        gameObject.SetActive(true);
        SetText(text);
        transform.position = position;
    }

    public void Hide()
    {
        ReturnToPool();
    }
}
