using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMusicTick
{
    public static event Action<int> GrantPointsEvent = delegate { };
    public void Move();
}
