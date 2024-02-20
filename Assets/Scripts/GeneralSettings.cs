using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalNamespace
{
    public static class GeneralSettings
    {
        #if !(UNITY_ANDROID || UNITY_IOS)
        public static List<char> m_PlayerControls = new List<char>{ 'Z', 'X', 'C', 'V' };
        #endif
    }
}

