using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMusicGameManager
{
    public void ReceivePoints(int iPoints);
    public void SetCurrentLevel(int iLevelIdx);
    public void ParseInformation();
    public IEnumerator PassAudio();
    public AudioSource GetAudioSource();
}
