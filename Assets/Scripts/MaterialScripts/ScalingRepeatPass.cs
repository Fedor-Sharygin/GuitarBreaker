using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingRepeatPass : MonoBehaviour
{
    private Renderer m_RepeatMaterialRenderer;
    private Vector3 PrevScale = Vector3.one;

    private void Awake()
    {
        SetScaleInShader();
    }

    private void OnDrawGizmos()
    {
        SetScaleInShader();
    }

    private void Update()
    {
        SetScaleInShader();
    }

    private void SetScaleInShader()
    {
        if (transform.localScale == PrevScale)
        {
            return;
        }
        if (m_RepeatMaterialRenderer == null)
        {
            m_RepeatMaterialRenderer = GetComponent<Renderer>();
        }

        Vector3 CurScale = transform.localScale;
        m_RepeatMaterialRenderer.sharedMaterial.SetVector("_Scale", new Vector4(CurScale.x, CurScale.y));
        PrevScale = CurScale;
    }
}
