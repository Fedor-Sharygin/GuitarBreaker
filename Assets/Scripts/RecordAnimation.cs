#if UNITY_STANDALONE || UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordAnimation : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private RawImage m_RawImage;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_RawImage = GetComponent<RawImage>();
        LevelManager.LevelEditor.SetRecordingSymbol(this);
        FlipRecording();
    }

    private bool bRecording = true;
    public void FlipRecording()
    {
        bRecording = !bRecording;
        if (!bRecording)
        {
            if (m_SpriteRenderer == null && m_RawImage == null)
            {
                return;
            }

            Color SymbolColor;
            if (m_SpriteRenderer != null)
            {
                SymbolColor = m_SpriteRenderer.color;
                SymbolColor.a = 0f;
                m_SpriteRenderer.color = SymbolColor;
            }
            else
            {
                SymbolColor = m_RawImage.color;
                SymbolColor.a = 0f;
                m_RawImage.color = SymbolColor;
            }
        }
    }

    private float fAlphaTime = 0f;
    private void Update()
    {
        if (!bRecording || (m_SpriteRenderer == null && m_RawImage == null))
        {
            return;
        }

        fAlphaTime += Time.deltaTime;
        Color SymbolColor;
        if (m_SpriteRenderer != null)
        {
            SymbolColor = m_SpriteRenderer.color;
            SymbolColor.a = .75f + .2f * Mathf.Sin(fAlphaTime);
            m_SpriteRenderer.color = SymbolColor;
        }
        else
        {
            SymbolColor = m_RawImage.color;
            SymbolColor.a = .75f + .2f * Mathf.Sin(fAlphaTime);
            m_RawImage.color = SymbolColor;
        }
    }
}

#endif