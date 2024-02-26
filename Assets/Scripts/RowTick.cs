using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowTick : MonoBehaviour, IMusicTick
{
    public static void GrantPoints(int p_Points, RowTick p_CurTick = null)
    {
        if (p_CurTick != null && !p_CurTick.Playable())
        {
            p_CurTick = null;
        }

        IMusicTick.GrantPoints(p_Points, p_CurTick);
    }

    [SerializeField]
    private bool m_Single = true;

    private bool m_TickPlayable = true;
    //private bool m_TickPlayed = false;
    public bool Playable()
    {
        return m_TickPlayable;
    }
    public void Play()
    {
        if (!m_TickPlayable)
        {
            return;
        }

        //m_TickPlayed = true;
        if (m_Single)
        {
            m_TickPlayable = false;
        }
    }
    public bool TickAvailableToPlay()
    {
        return !m_Single;
    }

    private void Update()
    {
        Move();
    }

    private bool m_Moving = false;
    [SerializeField]
    private float m_Speed;
    public void Move()
    {
        if (!m_Moving)
        {
            return;
        }

        transform.localPosition += m_Speed * Time.deltaTime * Vector3.down;
    }

    public void StartMovement()
    {
        m_Moving = true;
    }

    public float GetSpeed()
    {
        return m_Speed;
    }

    public void SetTickType(int p_Type)
    {
        m_Single = p_Type == 0 ? true : false;
    }
}
