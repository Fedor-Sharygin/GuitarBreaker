using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float m_TimerSeconds = 20f;
    public float m_CurTimeLeft { private set; get; }

    [SerializeField]
    private bool m_ResetOnEnd = true;
    private bool m_TimerEnded = false;
    [SerializeField]
    private bool m_StartOnAwake = true;

    public UnityEvent m_EndActions;

    private bool m_Paused = false;

    private void Awake()
    {
        if (m_StartOnAwake)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }

    private void FixedUpdate()
    {
        if (m_TimerEnded || m_Paused)
        {
            return;
        }

        m_CurTimeLeft -= Time.fixedDeltaTime;
        if (m_CurTimeLeft <= 0f)
        {
            StartCoroutine("ExecuteOnEnd");
        }
    }

    public void ResetTimer()
    {
        m_TimerEnded = false;
        m_CurTimeLeft = m_TimerSeconds;
    }

    public void ExecuteOnEnd()
    {
        Debug.Log($"LOG: Timer [{name}] ended");
        m_TimerEnded = true;
        m_EndActions.Invoke();
        if (m_ResetOnEnd)
        {
            ResetTimer();
        }
    }

    public void Pause()
    {
        m_Paused = true;
    }

    public void UnPause()
    {
        m_Paused = false;
    }

    public void Stop()
    {
        ResetTimer();
        Pause();
    }

    public void Play()
    {
        ResetTimer();
        UnPause();
    }

    public bool IsPlaying()
    {
        return !m_Paused && !m_TimerEnded;
    }
}
