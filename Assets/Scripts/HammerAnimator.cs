using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAnimator : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve m_RotationCurve;
    private bool m_Animate = false;
    private bool m_Direction = false;
    public void FlipAnimationDirection()
    {
        m_Animate = true;
        m_Direction = !m_Direction;
    }

    [SerializeField]
    private float m_AnimationSpeed;
    private float m_CurPercent = 0f;
    private float m_PrevXRotation = 0f;
    private void Update()
    {
        if (!m_Animate)
        {
            return;
        }

        m_CurPercent += (m_Direction ? m_AnimationSpeed : -m_AnimationSpeed) * Time.deltaTime;
        if (m_CurPercent > 1f)
        {
            m_CurPercent = 1f;
            m_Animate = false;
        }
        if (m_CurPercent < 0f)
        {
            m_CurPercent = 0f;
            m_Animate = false;
        }

        float NewXRotation = m_RotationCurve.Evaluate(m_CurPercent);
        Quaternion NRotation = Quaternion.AngleAxis(NewXRotation - m_PrevXRotation, Vector3.right);
        transform.rotation = transform.rotation * NRotation;
        m_PrevXRotation = NewXRotation;
    }

}
