using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetBehavior
{
    public void ActivateTarget(bool p_State);
    public void ActivateTick();
    public bool IsInRange(float p_YPos);
}
