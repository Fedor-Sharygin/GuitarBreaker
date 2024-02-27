#if !(UNITY_ANDROID || UNITY_IOS)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalNamespace
{
    public static class GeneralSettings
    {
        public static List<char> m_PlayerControls = new List<char>{ 'Z', 'X', 'C', 'V' };

        #if UNITY_STANDALONE || UNITY_EDITOR
        public static KeyCode m_DebugRecordingButton = KeyCode.F9;
        #endif
    }
}

#endif