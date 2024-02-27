#if UNITY_STANDALONE || UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LevelManager
{
    public static class LevelEditor
    {
        private static RecordAnimation m_RecordSymbol;
        public static void SetRecordingSymbol(RecordAnimation p_RecordSymbol)
        {
            m_RecordSymbol = p_RecordSymbol;
        }

        public static bool m_LevelEditingState { private set; get; } = false;
        public static void FlipEditState()
        {
            if (m_RecordSymbol)
            {
                m_RecordSymbol.FlipRecording();
            }
            m_LevelEditingState = !m_LevelEditingState;

            if (!m_LevelEditingState)
            {
                SaveToFile();
            }
            else
            {
                m_AudioSource.time = 0;
            }
        }

        private static string m_CurLevelFile;
        private static LevelInfo m_LevelInfo;
        private static AudioSource m_AudioSource;
        private static bool m_IntroRecording;

        private static List<List<Tuple<float, float>>> m_TickTimeStamps = new List<List<Tuple<float, float>>>(4);
        private static List<float> m_StartTimeStamps = new List<float>();
        private static List<float> m_EndTimeStamps = new List<float>();
        private static List<int> m_RowNums = new List<int>();

        public static float[] m_TimePerBeat = new float[4];
        private static float m_TimePerHalf;
        private static float m_TimePerQuarter;
        private static float m_TimePerEighth;
        private static float m_TimePerSixteenth;
        public static void SetCurFileLevel(string p_FileLevel, bool p_RecordState, LevelInfo p_LevelInfo, AudioSource p_AudioSource)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (m_TickTimeStamps.Count <= i)
                {
                    m_TickTimeStamps.Add(new List<Tuple<float, float>>());
                }
                else if(m_TickTimeStamps[i] == null)
                {
                    m_TickTimeStamps[i] = new List<Tuple<float, float>>();
                }
                else
                {
                    m_TickTimeStamps[i].Clear();
                }
            }

            m_CurLevelFile = p_FileLevel;
            m_IntroRecording = p_RecordState;
            m_LevelInfo = p_LevelInfo;
            m_AudioSource = p_AudioSource;

            //m_TickTimeStamps.Clear();
            m_StartTimeStamps.Clear();
            m_EndTimeStamps.Clear();
            m_RowNums.Clear();

            m_TimePerQuarter = 60f / m_LevelInfo.BPM;
            m_TimePerHalf = m_TimePerQuarter * 2f;
            m_TimePerEighth = m_TimePerQuarter / 2f;
            m_TimePerSixteenth = m_TimePerEighth / 2f;

            m_TimePerBeat[0] = m_TimePerHalf;
            m_TimePerBeat[1] = m_TimePerQuarter;
            m_TimePerBeat[2] = m_TimePerEighth;
            m_TimePerBeat[3] = m_TimePerSixteenth;
        }

        private static int m_BeatRoundingIdx = 2;
        public static float RoundedTime()
        {
            int BeatCount = Mathf.RoundToInt(m_AudioSource.time / m_TimePerBeat[m_BeatRoundingIdx]);
            return BeatCount * m_TimePerBeat[m_BeatRoundingIdx];
        }

        public static void AddBeatStart(int p_RowNum)
        {
            Tuple<float, float> TimeStampTuple = new Tuple<float, float>(RoundedTime(), int.MaxValue);
            m_TickTimeStamps[p_RowNum].Add(TimeStampTuple);
        }
        public static void AddBeatEnd(int p_RowNum)
        {
            Tuple<float, float> CurTuple = 
                new Tuple<float, float>(m_TickTimeStamps[p_RowNum][m_TickTimeStamps[p_RowNum].Count - 1].Item1, RoundedTime());
            m_TickTimeStamps[p_RowNum][m_TickTimeStamps[p_RowNum].Count - 1] = CurTuple;
        }

        private static void SaveToFile()
        {
            List<TickInfo> TickList = new List<TickInfo>();
            for (int i = 0; i < 4; ++i) {
                List<Tuple<float, float>> RowTickList = m_TickTimeStamps[i];
                foreach (Tuple<float, float> TickTimes in RowTickList)
                {
                    float Length = TickTimes.Item2 - TickTimes.Item1;
                    bool Single = Length < m_TimePerBeat[m_BeatRoundingIdx];
                    TickList.Add(new TickInfo(i, TickTimes.Item1, Single, Length));
                }
            }

            m_LevelInfo.MusicTicks = TickList.ToArray();
            string FilePath = Path.Combine(GlobalNamespace.GlobalMethods.PersistentFolder, m_CurLevelFile + ".json");
            string JsonDesc = JsonUtility.ToJson(m_LevelInfo, true);
            File.WriteAllText(FilePath, JsonDesc);

            //foreach (float TimeStamp in lfTimeStamps)
            //{
            //    NTickList.Add(new TickInfo(TimeStamp));
            //}

            //if (bIntroRecording)
            //{
            //    liLevel.IntroBeats = NTickList.ToArray();
            //}
            //else
            //{
            //    liLevel.GameBeats = NTickList.ToArray();
            //}
        }
    }
}

#endif