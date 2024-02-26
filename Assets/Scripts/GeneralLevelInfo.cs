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

        public TickInfo(int NRow, float NTimeStamp, bool NSignle, float NLength)
        {
            Row = NRow;
            TimeStamp = NTimeStamp;
            Single = NSignle;
            Length = NLength;
        }
    }
    [Serializable]
    public struct LevelInfo
    {
        public string MusicName;
        public string LevelName;
        public int BPM;
        public TickInfo[] MusicTicks;
    }
}
