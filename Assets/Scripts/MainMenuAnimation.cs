using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalNamespace;

public class MainMenuAnimation : MonoBehaviour
{
    private float m_AngleLerp = Mathf.Pow(2, -5);

    
    [SerializeField]
    private AudioSource auSource;
    private void Awake()
    {
        LevelManager.LevelLoader.SetupLevelManager();
        if ( LevelManager.LevelLoader.giGameInfo.Levels == null
          || LevelManager.LevelLoader.giGameInfo.Levels.Count == 0)
        {
            return;
        }

        // No "RecordSymbol" as of now
        //GameObject RecCirc = GameObject.FindGameObjectWithTag("RecordSymbol");
        //if (RecCirc != null)
        //{
        //    #if UNITY_STANDALONE || UNITY_EDITOR
        //    DontDestroyOnLoad(RecCirc);
        //    #else
        //    Destroy(RecCirc);
        //    #endif
        //}
    }

    [SerializeField]
    private float[] m_TargetAngles;
    private int m_CurTargetIdx = 0;
    public void Update()
    {
        RotateToTargetAngle();
    }

    //private float m_PrevAngleVal;
    private void RotateToTargetAngle()
    {
        if (GlobalMethods.AreAnglesEqual(transform.localRotation.eulerAngles.y, m_TargetAngles[m_CurTargetIdx]))
        {
            return;
        }

        transform.localRotation = transform.localRotation * Quaternion.AngleAxis(-m_IdxDiff * m_AngleLerp, Vector3.up);
    }

    private int m_IdxDiff = 1;
    public void SetTargetAngle()
    {
        m_CurTargetIdx += m_IdxDiff;
        if (m_CurTargetIdx < 0 || m_CurTargetIdx == m_TargetAngles.Length)
        {
            m_IdxDiff *= -1;
            m_CurTargetIdx += 2 * m_IdxDiff;
        }
    }
    

    private LevelManager.LevelInfo CurLevelInfo;
    private int iClipIdx;
    public void StartAudio(int iAudioIdx)
    {
        iClipIdx = iAudioIdx;
        StartCoroutine("ChangeAudio");
    }

    public IEnumerator ChangeAudio()
    {
        CurLevelInfo = LevelManager.LevelLoader.GetMusicInfo(iClipIdx);

        #if UNITY_STANDALONE  || UNITY_EDITOR
        LevelManager.LevelEditor.SetCurFileLevel(LevelManager.LevelLoader.giGameInfo.GetLevelName(iClipIdx),
            true, CurLevelInfo, auSource);
        #endif

        yield return GlobalMethods.GetAudioClip(CurLevelInfo.MusicName,
            acAudioClip =>
            {
                auSource.clip = acAudioClip;
                auSource.loop = true;
                //iIntroBeatIdx = 0;
                //bReceivedAudio = true;
                auSource.Play();
            }
        );
    }
}
