using LevelManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    //private GuitarSmasherInputControls m_InputControls;
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

        //m_InputControls = new GuitarSmasherInputControls();
        
        //#if !(UNITY_ANDROID || UNITY_IOS)
        //m_InputControls.Player.BlueInput.started    += TargetControlStart;
        //m_InputControls.Player.RedInput.started     += TargetControlStart;
        //m_InputControls.Player.GreenInput.started   += TargetControlStart;
        //m_InputControls.Player.YellowInput.started  += TargetControlStart;

        //m_InputControls.Player.BlueInput.canceled   += TargetControlEnd;
        //m_InputControls.Player.RedInput.canceled    += TargetControlEnd;
        //m_InputControls.Player.GreenInput.canceled  += TargetControlEnd;
        //m_InputControls.Player.YellowInput.canceled += TargetControlEnd;

        //#if UNITY_STANDALONE || UNITY_EDITOR
        //m_InputControls.Player.DebugInput.started   += DebugInputFlip;
        //#endif

        //#endif
    }

    private void OnDestroy()
    {
        IMusicTick.GrantPointsEvent -= ReceivePoints;
        
        //#if !(UNITY_ANDROID || UNITY_IOS)
        //m_InputControls.Player.BlueInput.started    -= TargetControlStart;
        //m_InputControls.Player.RedInput.started     -= TargetControlStart;
        //m_InputControls.Player.GreenInput.started   -= TargetControlStart;
        //m_InputControls.Player.YellowInput.started  -= TargetControlStart;

        //m_InputControls.Player.BlueInput.canceled   -= TargetControlEnd;
        //m_InputControls.Player.RedInput.canceled    -= TargetControlEnd;
        //m_InputControls.Player.GreenInput.canceled  -= TargetControlEnd;
        //m_InputControls.Player.YellowInput.canceled -= TargetControlEnd;
        
        //#if UNITY_STANDALONE || UNITY_EDITOR
        //m_InputControls.Player.DebugInput.started   -= DebugInputFlip;
        //#endif

        //#endif
    }
    
    #if !(UNITY_ANDROID || UNITY_IOS)
    private void TargetControlStart(InputAction.CallbackContext p_InputContext)
    {
        switch (p_InputContext.action.name)
        {
            case "Blue Input":   m_ButtonsActive[0] = true;  break;
            case "Red Input":    m_ButtonsActive[1] = true;  break;
            case "Green Input":  m_ButtonsActive[2] = true;  break;
            case "Yellow Input": m_ButtonsActive[3] = true;  break;
        }
    }

    private void TargetControlEnd(InputAction.CallbackContext p_InputContext)
    {
        switch (p_InputContext.action.name)
        {
            case "Blue Input":   m_ButtonsActive[0] = false; break;
            case "Red Input":    m_ButtonsActive[1] = false; break;
            case "Green Input":  m_ButtonsActive[2] = false; break;
            case "Yellow Input": m_ButtonsActive[3] = false; break;
        }
    }

    public void TargetControl(InputAction.CallbackContext p_InputContext)
    {
        if (p_InputContext.phase == InputActionPhase.Started)
        {
            TargetControlStart(p_InputContext);
        }
        else if (p_InputContext.phase == InputActionPhase.Canceled)
        {
            TargetControlEnd(p_InputContext);
        }
    }

    #endif
    
    #if UNITY_STANDALONE || UNITY_EDITOR
    private void DebugInputFlip(InputAction.CallbackContext _p_InputContext)
    {
        LevelEditor.FlipEditState();
    }
    public void DebugInput(InputAction.CallbackContext p_InputContext)
    {
        if (p_InputContext.phase != InputActionPhase.Started)
        {
            return;
        }

        DebugInputFlip(p_InputContext);
    }
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
    }
    #endif

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
        #if UNITY_ANDROID || UNITY_IOS
        ResetButtonsState();
        foreach (Touch CurTouch in Input.touches)
        {
            Ray TouchRay = Camera.main.ScreenPointToRay(CurTouch.position);
            RaycastHit Hit;
            if (Physics.Raycast(TouchRay, out Hit))
            {
                CheckButtonHit(Hit);
            }
        }
        #endif

        //#else
        
        //int ButtonIdx = 0;
        //foreach (KeyCode ButtonControl in GlobalNamespace.GeneralSettings.m_PlayerControls)
        //{
        //    if (ButtonIdx >= m_ButtonHammerConnections.Length)
        //    {
        //        break;
        //    }

        //    //PlayerInput playerInput = GetComponent<PlayerInput>();
        //    //playerInput;

        //    if (Input.GetKey(ButtonControl))
        //    {
        //        m_ButtonsActive[ButtonIdx] = true;
        //    }
        //    ++ButtonIdx;
        //}

        //#endif

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
        if (m_LevelInfo.MusicTicks == null)
        {
            return;
        }

        float TickSpeed = m_TickPrefab.GetComponent<IMusicTick>().GetSpeed();
        Color[] RowColors = new Color[m_ButtonHammerConnections.Length];
        int Idx = 0;
        foreach (ButtonHammerConnection Connection in m_ButtonHammerConnections)
        {
            RowColors[Idx] = Connection.m_ButtonCollider.GetComponent<SpriteRenderer>().color;
            ++Idx;
        }

        foreach (TickInfo Info in m_LevelInfo.MusicTicks)
        {
            GameObject NewTick = Instantiate(m_TickPrefab, m_Targets[Info.Row].transform);
            NewTick.transform.localPosition = new Vector3(0, TickSpeed * Info.TimeStamp, .15f);
            NewTick.transform.localRotation = Quaternion.identity;
            Color RowColor = RowColors[Info.Row];

            SpriteRenderer[] TickSprites = NewTick.GetComponentsInChildren<SpriteRenderer>(true);
            if (TickSprites != null)
            {
                foreach (SpriteRenderer TickSprite in TickSprites)
                {
                    TickSprite.color = RowColor;
                }
            }
            NewTick.GetComponent<Renderer>().material.SetColor("_TintColor", RowColor);

            m_LevelTicks.Add(NewTick.GetComponent<IMusicTick>());
            if (!Info.Single)
            {
                BoxCollider TickBox = NewTick.GetComponent<BoxCollider>();
                Vector3 TickScale = NewTick.transform.localScale;
                float PrevYScale = TickScale.y;
                float YScaler = Info.Length / TickBox.size.y;
                TickScale.y = YScaler;
                NewTick.transform.localScale = TickScale;

                for (int i = 0; i < NewTick.transform.childCount; ++i)
                {
                    Transform ModelTransform = NewTick.transform.GetChild(i);
                    Vector3 ModelScale = ModelTransform.localScale;
                    ModelScale.y /= (YScaler / PrevYScale);
                    ModelTransform.localPosition = new Vector3(0, (i == 0 ? -1 : 1) * .45f, 0);
                    ModelTransform.localScale = ModelScale;
                }

                NewTick.transform.localPosition += new Vector3(0, YScaler / 2, 0);
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
