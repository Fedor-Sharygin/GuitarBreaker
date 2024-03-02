using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMusicGameManager
{
    public void ReceivePoints(int p_Points, IMusicTick p_CurTick = null);
    public void SetCurrentLevel(int p_LevelIdx);
    public void ParseInformation();
    public IEnumerator PassAudio();
    public AudioSource GetAudioSource();

    public void BreakCombo();
    public void IncreaseCombo();
    public int GetComboMultiplier();
}
