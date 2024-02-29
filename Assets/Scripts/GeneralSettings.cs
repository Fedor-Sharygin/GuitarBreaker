

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalNamespace
{
    public static class GeneralSettings
    {
        #region VOLUME_SETTINGS
        private static float m_DefaultVolumePercentage = .3f;
        public static float m_PrevVolumePercentage { get; private set; } = m_DefaultVolumePercentage;
        public static float m_VolumePercentage { get; private set; } = m_DefaultVolumePercentage;
        private static string m_MusicVolumeKey = "MusicVolume";
        private static void SetupVolumeSetting()
        {
            if (PlayerPrefs.HasKey(m_MusicVolumeKey))
            {
                m_VolumePercentage = PlayerPrefs.GetFloat(m_MusicVolumeKey);
            }
            else
            {
                PlayerPrefs.SetFloat(m_MusicVolumeKey, m_VolumePercentage);
                m_VolumePercentage = .3f;
            }
            m_PrevVolumePercentage = m_VolumePercentage;
        }
        private static void SaveVolumeSetting()
        {
            m_PrevVolumePercentage = m_VolumePercentage;
            PlayerPrefs.SetFloat(m_MusicVolumeKey, m_PrevVolumePercentage);
        }
        public static void SetVolumePercentage(float p_NewPercentage)
        {
            m_VolumePercentage = p_NewPercentage;
        }
        private static void ResetDefaultVolumePercentage()
        {
            SetVolumePercentage(m_DefaultVolumePercentage);
        }
        private static void ResetPreviousVolumePercentage()
        {
            SetVolumePercentage(m_PrevVolumePercentage);
        }
        #endregion

        #region PC_SETTINGS
#if !(UNITY_ANDROID || UNITY_IOS)

        #region PLAYER_CONTROL_SETTINGS
        private static List<KeyCode> m_DefaultPlayerControls
            = new List<KeyCode> {
                KeyCode.Z,
                KeyCode.X,
                KeyCode.C,
                KeyCode.V
            };
        public static List<KeyCode> m_PlayerControls { get; private set; } = new List<KeyCode>(m_DefaultPlayerControls);
        private static List<string> m_PlayerControlKeys
            = new List<string>
            {
                "Control0",
                "Control1",
                "Control2",
                "Control3"
            };
        private static void SetupPlayerControls()
        {
            for (int i = 0; i < m_PlayerControlKeys.Count; ++i)
            {
                string PlayerControlKey = m_PlayerControlKeys[i];
                if (PlayerPrefs.HasKey(PlayerControlKey))
                {
                    m_PlayerControls[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(PlayerControlKey));
                }
                else
                {
                    PlayerPrefs.SetString(PlayerControlKey, m_PlayerControls[i].ToString());
                }
            }
        }
        private static void SavePlayerControls()
        {
            for (int i = 0; i < m_PlayerControlKeys.Count; ++i)
            {
                PlayerPrefs.SetString(m_PlayerControlKeys[i], m_PlayerControls[i].ToString());
            }
        }
        public static void SetPlayerControl(int p_Idx, KeyCode p_KeyCode)
        {
            m_PlayerControls[p_Idx] = p_KeyCode;
        }
        private static void ResetDefaultPlayerControls()
        {
            for (int i = 0; i < m_DefaultPlayerControls.Count; ++i)
            {
                SetPlayerControl(i, m_DefaultPlayerControls[i]);
            }
        }
        #endregion

        #region DEBUG_SETTINGS
        #if UNITY_STANDALONE || UNITY_EDITOR
        private static KeyCode m_DefaultDebugRecordingButton = KeyCode.F9;
        public static KeyCode m_DebugRecordingButton { get; private set; } = m_DefaultDebugRecordingButton;
        private static string m_DebugControlKey = "DebugKey";
        private static void SetupDebugSetting()
        {
            if (PlayerPrefs.HasKey(m_DebugControlKey))
            {
                m_DebugRecordingButton = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(m_DebugControlKey));
            }
            else
            {
                PlayerPrefs.SetString(m_DebugControlKey, m_DebugRecordingButton.ToString());
            }
        }
        private static void SaveDebugSetting()
        {
            PlayerPrefs.SetString(m_DebugControlKey, m_DebugRecordingButton.ToString());
        }
        private static void ResetDefaultDebugControl()
        {
            m_DebugRecordingButton = m_DefaultDebugRecordingButton;
        }
#endif
        #endregion

#endif
        #endregion

        private static bool m_SetupComplete = false;
        public static void SetupGeneralSettings()
        {
            if (m_SetupComplete)
            {
                return;
            }

            SetupVolumeSetting();

            #if !(UNITY_ANDROID || UNITY_IOS)
            SetupPlayerControls();

            #if UNITY_STANDALONE || UNITY_EDITOR
            SetupDebugSetting();

            #endif
            #endif

            PlayerPrefs.Save();

            m_SetupComplete = true;
        }
        public static void SaveGeneralSettings()
        {
            SaveVolumeSetting();

            #if !(UNITY_ANDROID || UNITY_IOS)
            SavePlayerControls();

            #if UNITY_STANDALONE || UNITY_EDITOR
            SaveDebugSetting();

            #endif
            #endif

            PlayerPrefs.Save();
        }
        public static void ResetDefaultSettings()
        {
            ResetDefaultVolumePercentage();

            #if !(UNITY_ANDROID || UNITY_IOS)
            ResetDefaultPlayerControls();

            #if UNITY_STANDALONE || UNITY_EDITOR
            ResetDefaultDebugControl();

            #endif
            #endif
        }
        public static void ResetPreviousSettings()
        {
            ResetPreviousVolumePercentage();
        }
    }
}
