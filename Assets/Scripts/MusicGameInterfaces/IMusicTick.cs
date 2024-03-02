using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMusicTick
{
    public static event Action<int, IMusicTick> GrantPointsEvent = delegate { };
    protected static void GrantPoints(int p_Points, IMusicTick p_CurTick = null)
    {
        if (p_CurTick != null)
        {
            p_CurTick.Play();
        }
        GrantPointsEvent(p_Points, p_CurTick);
    }

    public void Move();
    public bool Playable();
    public void Play();
    public bool TickAvailableToPlay();

    public void StartMovement();
    public float GetSpeed();

    public void SetTickType(int p_Type);
}
