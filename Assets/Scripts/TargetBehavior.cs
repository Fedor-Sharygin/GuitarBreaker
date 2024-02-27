using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour, ITargetBehavior
{
    private GameManager.ActiveRange m_Range;
    private GameManager m_GameManager;
    private void Awake()
    {
        m_GameManager = transform.parent?.parent?.GetComponent<GameManager>();
        m_Range = m_GameManager?.GetTickRange();
    }

    public bool IsInRange(float p_StartY)
    {
        return m_Range.IsInRange(p_StartY);
    }

    private bool m_TargetActive = false;
    private bool m_PrevTargetState = false;
    [SerializeField]
    private Timer m_TickActivationWindow;
    public void ActivateTarget(bool p_State)
    {
        //Debug.Log($"LOG: Target [{name}] activated state [{p_State}]");
        m_PrevTargetState = m_TargetActive;
        if (m_TargetActive == p_State)
        {
            return;
        }
        Debug.Log($"LOG: Target [{name}] new state [{p_State}]");
        m_TargetActive = p_State;
        if (p_State == false)
        {
            m_PointTimer?.Stop();
        }
    }
    private bool JustActivated()
    {
        return m_TargetActive == true && m_PrevTargetState == false;
    }
    private bool ActivatedTick()
    {
        bool IsInActivationWindow = true;
        if (m_TickActivationWindow != null)
        {
            IsInActivationWindow = m_TickActivationWindow.IsPlaying();
        }
        return JustActivated() && IsInActivationWindow;
    }

    private IMusicTick m_CurTickAvailable = null;
    private void OnTriggerEnter(Collider p_Other)
    {
        if (p_Other == null || p_Other.tag != "MusicTick")
        {
            return;
        }

        m_TickActivationWindow?.Play();
        Debug.Log($"LOG: Target [{gameObject.name}] received Tick [{p_Other.name}]");
        m_CurTickAvailable = p_Other.GetComponent<IMusicTick>();
    }
    private void OnTriggerExit(Collider p_Other)
    {
        if (p_Other == null || p_Other.tag != "MusicTick")
        {
            return;
        }

        Debug.Log($"LOG: Target [{gameObject.name}] lost Tick [{p_Other.name}]");
        m_CurTickAvailable = null;
        m_PointTimer?.Stop();
    }

    private void FixedUpdate()
    {
        ActivateTick();
    }

    [SerializeField]
    private Timer m_PointTimer;
    public void ActivateTick()
    {
        if (!JustActivated())
        {
            //Debug.Log($"LOG: Target [{name}] is not active");
            return;
        }

        //if (m_GameManager == null || m_Range == null)
        //{
        //    return;
        //}

        Debug.Log($"LOG: Target [{name}] now Grants Points");
        GrantPoints();
    }

    public void GrantPoints()
    {
        Debug.Log($"LOG: Target [{name}] is trying to Grant Points");
        if (m_CurTickAvailable != null)
        {
            //Debug.Log($"LOG: Target [{gameObject.name}] granted points from Tick [{((RowTick)m_CurTickAvailable).name}]");
            if (ActivatedTick())
            {
                //Debug.Log($"LOG: Target [{name}] grants points with Combo Increase");
                m_TickActivationWindow?.Stop();
                m_PointTimer?.Play();
                RowTick.GrantPoints(5, (RowTick)m_CurTickAvailable);
            }
            else if (m_CurTickAvailable.TickAvailableToPlay())
            {
                //Debug.Log($"LOG: Target [{name}] grants points NO Combo Increase");
                RowTick.GrantPoints(5);
            }
        }
        else if (JustActivated())
        {
            //Debug.Log($"LOG: Target [{gameObject.name}] breaks combo");
            m_TickActivationWindow?.Stop();
            m_PointTimer?.Stop();
            RowTick.GrantPoints(0);
        }
    }
}
