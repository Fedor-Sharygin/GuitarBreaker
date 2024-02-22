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
    private bool m_TickPlayed = false;
    //[SerializeField]
    //private Timer m_PlayRangeTimer;
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

        //if (m_PlayRangeTimer != null && !m_PlayRangeTimer.IsPlaying())
        //{
            //m_PlayRangeTimer.Play();
        //}
        m_TickPlayed = true;
        if (m_Single)
        {
            m_TickPlayable = false;
        }
    }
    public bool TickAvailableToPlay()
    {
        //bool TimerIsPlaying = true;
        //if (m_PlayRangeTimer != null)
        //{
        //TimerIsPlaying = m_PlayRangeTimer.IsPlaying();
        //m_PlayRangeTimer.Stop();
        //}
        //Debug.Log($"LOG: PlayableTick");
        //return m_TickPlayed == true && TimerIsPlaying;

        return !m_Single;
    }

    private void Update()
    {
        Move();
        //CheckPos();
    }

    private void CheckPos()
    {
        if (m_TickPlayed || transform.parent == null)
        {
            return;
        }

        ITargetBehavior target = transform.parent.GetComponent<ITargetBehavior>();
        if (target == null)
        {
            return;
        }

        m_TickPlayable = target.IsInRange(transform.localPosition.y);
    }

    [SerializeField]
    private float m_Speed;
    public void Move()
    {
        transform.localPosition += m_Speed * Time.deltaTime * Vector3.down;
    }
}
