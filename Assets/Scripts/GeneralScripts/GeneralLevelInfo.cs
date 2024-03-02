using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManager
{
    [Serializable]
    public struct TickInfo
    {
        public int Row;
        public float TimeStamp;
        public bool Single;
        public float Length;

        public TickInfo(int p_Row, float p_TimeStamp, bool p_Signle, float p_Length)
        {
            Row = p_Row;
            TimeStamp = p_TimeStamp;
            Single = p_Signle;
            Length = p_Length;
        }
    }
    [Serializable]
    public struct LevelInfo
    {
        public string MusicName;
        public string LevelName;
        public float BPM;
        public TickInfo[] MusicTicks;
    }
}
