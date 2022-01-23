using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_ScrollingSpeed = new Vector2();

    [SerializeField]
    private Renderer m_Renderer = null;

    private Material m_Material = null;

    private void Start()
    {
        m_Material = m_Renderer.material;
    }

    void Update()
    {
        m_Material.mainTextureOffset += m_ScrollingSpeed * Time.deltaTime;
    }
}
