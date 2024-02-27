using LevelManager;
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
    #if UNITY_STANDALONE || UNITY_EDITOR
    private bool[] m_PrevButtonState = new bool[4];
    #endif
    private bool[] m_ButtonsActive;
    [SerializeField]
    private TargetBehavior[] m_Targets;

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

        public bool IsInRange(float p_YPos)
        {
            return p_YPos <= m_Top && p_YPos >= m_Bottom;
        }
    }
    [SerializeField]
    private ActiveRange m_TickRange;
    public ActiveRange GetTickRange()
    {
        return m_TickRange;
    }

    private void Start()
    {
        m_ButtonsActive = new bool[m_ButtonHammerConnections.Length];
        ResetButtonsState();

        IMusicTick.GrantPointsEvent += ReceivePoints;

        ComboCounts = ComboRanges;
        for (int i = 1; i < ComboCounts.Length; ++i)
        {
            ComboCounts[i] += ComboCounts[i - 1];
        }

        ParseInformation();
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

    private void FixedUpdate()
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
        ActivateTargets();
        
        #if UNITY_STANDALONE || UNITY_EDITOR
        CheckEditorState();
        Array.Copy(m_ButtonsActive, m_PrevButtonState, 4);
        #endif
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

    private void ActivateTargets()
    {
        int ButtonIdx = 0;
        foreach (TargetBehavior target in m_Targets)
        {
            target.ActivateTarget(m_ButtonsActive[ButtonIdx]);
            ++ButtonIdx;
        }
    }

    #if UNITY_STANDALONE || UNITY_EDITOR
    private void CheckEditorState()
    {
        if (LevelEditor.m_LevelEditingState)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (m_ButtonsActive[i] == m_PrevButtonState[i])
                {
                    continue;
                }

                if (m_ButtonsActive[i])
                {
                    LevelEditor.AddBeatStart(i);
                }
                else
                {
                    LevelEditor.AddBeatEnd(i);
                }
            }
        }

        if (Input.GetKeyDown(GlobalNamespace.GeneralSettings.m_DebugRecordingButton))
        {
            LevelEditor.FlipEditState();
        }
    }
    #endif

    private uint m_ComboIdx = 0;
    private int m_TickCount = 0;
    [SerializeField]
    private int[] ComboRanges;
    private int[] ComboCounts;
    [SerializeField]
    private int[] ComboMultipliers;
    public void BreakCombo()
    {
        Debug.Log($"LOG: Combo broken");
        m_ComboIdx = 0;
        m_TickCount = 0;
    }
    public void IncreaseCombo()
    {
        Debug.Log($"LOG: Combo increased");
        m_TickCount++;
        m_ComboCountText.text = m_TickCount.ToString();
        if (m_ComboIdx < ComboCounts.Length && m_TickCount >= ComboCounts[m_ComboIdx])
        {
            Debug.Log($"LOG: Combo is at next idx {m_ComboIdx}");
            m_ComboIdx++;
        }
        m_ComboMultText.text = GetComboMultiplier().ToString();
    }
    public int GetComboMultiplier()
    {
        return ComboMultipliers[m_ComboIdx];
    }

    public AudioSource GetAudioSource()
    {
        return m_LevelMusic;
    }

    private int m_CurPoints = 0;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_PointsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_ComboCountText;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_ComboMultText;
    public void ReceivePoints(int p_Points, IMusicTick p_CurTick = null)
    {
        if (p_Points == 0)
        {
            BreakCombo();
            return;
        }

        if (p_CurTick != null)
        {
            IncreaseCombo();
        }
        Debug.Log($"LOG: Points granted: {p_Points}");
        m_CurPoints += GetComboMultiplier() * p_Points;
        m_PointsText.text = m_CurPoints.ToString();
    }

    [SerializeField]
    private GameObject m_TickPrefab;
    private List<IMusicTick> m_LevelTicks = new List<IMusicTick>();
    public void ParseInformation()
    {
        float TickSpeed = m_TickPrefab.GetComponent<IMusicTick>().GetSpeed();

        foreach (TickInfo Info in m_LevelInfo.MusicTicks)
        {
            GameObject NewTick = Instantiate(m_TickPrefab, m_Targets[Info.Row].transform);
            NewTick.transform.localPosition = new Vector3(0, TickSpeed * Info.TimeStamp, .15f);
            NewTick.transform.localRotation = Quaternion.identity;

            m_LevelTicks.Add(NewTick.GetComponent<IMusicTick>());
            if (Info.Length > 0)
            {
                NewTick.transform.localScale.Scale(new Vector3(1, Info.Length, 1));
            }
            m_LevelTicks[m_LevelTicks.Count - 1].SetTickType(Info.Single ? 0 : 1);
        }

        SetPointsText("0");
        SetComboCountText("x 0");
        SetComboMultText("x 1");
    }

    private void SetPointsText(string p_PointsText)
    {
        if (m_PointsText == null)
        {
            return;
        }
        m_PointsText.text = p_PointsText;
    }
    private void SetComboCountText(string p_ComboCountText)
    {
        if (m_ComboCountText == null)
        {
            return;
        }
        m_ComboCountText.text = p_ComboCountText;
    }
    private void SetComboMultText(string p_ComboMultText)
    {
        if (m_ComboMultText == null)
        {
            return;
        }
        m_ComboMultText.text = p_ComboMultText;
    }

    [SerializeField]
    private AudioSource m_LevelMusic;
    public IEnumerator PassAudio()
    {
        yield return GlobalNamespace.GlobalMethods.GetAudioClip(m_LevelInfo.MusicName,
            p_AudioClip =>
            {
                m_LevelMusic.clip = p_AudioClip;
                m_LevelMusic.loop = false;
                m_LevelMusic.Play();

                foreach (IMusicTick Tick in m_LevelTicks)
                {
                    Tick.StartMovement();
                }

                GlobalNamespace.GlobalMethods.StartLevel();
            }
        );
    }

    private int m_CurLevelIdx;
    private LevelInfo m_LevelInfo;
    public void SetCurrentLevel(int p_LevelIdx)
    {
        m_CurLevelIdx = p_LevelIdx;
        m_LevelInfo = LevelLoader.giGameInfo.Levels[p_LevelIdx];
        StartCoroutine("PassAudio");
    }
}
