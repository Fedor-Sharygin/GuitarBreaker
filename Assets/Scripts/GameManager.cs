using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IMusicGameManager
{

    [Serializable]
    protected class ButtonHammerConnection
    {
        public Collider m_ButtonCollider;
        public Animator m_HammerAnimator;
    }
    [SerializeField]
    private ButtonHammerConnection[] m_ButtonHammerConnections;
    private bool[] m_ButtonsActive;


    [Serializable]
    public class ActiveRange
    {
        [SerializeField]
        private float m_Top;
        public float Top
        {
            get { return m_Top; }
            set { m_Top = value; }
        }

        [SerializeField]
        private float m_Bottom;
        public float Bottom
        {
            get { return m_Bottom; }
            set { m_Bottom = value; }
        }

        [SerializeField]
        private float m_OutHeight;
        public float OutHeight
        {
            get { return m_OutHeight; }
            set { m_OutHeight = value; }
        }

    }
    [SerializeField]
    private ActiveRange m_TickRange;
    public ActiveRange GetTickRange()
    {
        return m_TickRange;
    }

    private void Awake()
    {
        m_ButtonsActive = new bool[m_ButtonHammerConnections.Length];
        ResetButtonsState();

        IMusicTick.GrantPointsEvent += ReceivePoints;
    }

    private void OnDestroy()
    {
        IMusicTick.GrantPointsEvent -= ReceivePoints;
    }

    private void ResetButtonsState()
    {
        for (int i = 0; i < m_ButtonsActive.Length; ++i)
        {
            m_ButtonsActive[i] = false;
        }
    }
    

    #if UNITY_ANDROID || UNITY_IOS

    private void CheckButtonHit(RaycastHit CurHit)
    {
        if (CurHit.collider == null)
        {
            return;
        }

        for (int i = 0; i < m_ButtonHammerConnections.Length; ++i)
        {
            if (CurHit.collider == m_ButtonHammerConnections[i].m_ButtonCollider)
            {
                m_ButtonsActive[i] = true;
            }
        }
    }

    #endif

    private void Update()
    {
        ResetButtonsState();

        #if UNITY_ANDROID || UNITY_IOS

        foreach (Touch CurTouch in Input.touches)
        {
            Ray TouchRay = Camera.main.ScreenPointToRay(CurTouch.position);
            RaycastHit Hit;
            if (Physics.Raycast(TouchRay, out Hit))
            {
                CheckButtonHit(Hit);
            }
        }

        #else
        
        int ButtonIdx = 0;
        foreach (char ButtonControl in GlobalNamespace.GeneralSettings.m_PlayerControls)
        {
            if (ButtonIdx >= m_ButtonHammerConnections.Length)
            {
                break;
            }
            KeyCode CurButtonControl = KeyCode.A + (ButtonControl - 'A');
            if (Input.GetKey(CurButtonControl))
            {
                m_ButtonsActive[ButtonIdx] = true;
            }
            ++ButtonIdx;
        }

        #endif

        AnimateHammers();
    }

    private void AnimateHammers()
    {
        int ButtonIdx = 0;
        foreach (ButtonHammerConnection BHC in m_ButtonHammerConnections)
        {
            if (BHC.m_HammerAnimator.GetBool("ButtonHit") != m_ButtonsActive[ButtonIdx])
            {
                BHC.m_HammerAnimator.SetBool("ButtonHit", m_ButtonsActive[ButtonIdx]);
                BHC.m_HammerAnimator.SetTrigger("ButtonHitTrigger");
            }

            ++ButtonIdx;
        }
    }

    public AudioSource GetAudioSource()
    {
        throw new System.NotImplementedException();
    }

    public void ReceivePoints(int iPoints)
    {
        throw new System.NotImplementedException();
    }

    public void ParseInformation()
    {
        //TO-DO:
        //Parse json info about each row.
        //Place them local as Row GO child localPos.y = timInfo * tickSpeed
        throw new System.NotImplementedException();
    }

    public IEnumerator PassAudio()
    {
        throw new System.NotImplementedException();
    }

    public void SetCurrentLevel(int iLevelIdx)
    {
        throw new System.NotImplementedException();
    }
}
