using UnityEngine;
using UnityEngine.UI;

public class Healthbar : APooledObject
{
    [SerializeField]
    private Image m_Image = null;

    [SerializeField]
    private Canvas m_Canvas = null;

    private void Awake()
    {
        m_Canvas.worldCamera = Camera.main;
        m_Canvas.renderMode = RenderMode.WorldSpace;
    }

    public void SetPercentage(float fill)
    {
        m_Image.fillAmount = fill;
    }

    public void Reset(Transform parent, Vector3 offset)
    {
        gameObject.SetActive(true);

        transform.SetParent(parent);
        transform.localPosition = offset;
    }
}
