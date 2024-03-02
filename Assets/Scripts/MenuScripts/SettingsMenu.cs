using UnityEngine;
using UnityEngine.UI;
using GlobalNamespace;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Scrollbar m_VolumeScrollbar;
    [SerializeField]
    public InputActionAsset m_Actions;

    private bool m_GameStarted = true;

    private void Awake()
    {
        GeneralSettings.SetupGeneralSettings();

        m_VolumeScrollbar.value = GeneralSettings.m_VolumePercentage;

        var Rebinds = PlayerPrefs.GetString("Rebinds");
        if (!string.IsNullOrEmpty(Rebinds))
        {
            m_Actions.LoadBindingOverridesFromJson(Rebinds);
        }
        m_GameStarted = false;
    }

    [SerializeField]
    private AudioSource m_MainMenuAudio;
    public void OnVolumeScrollerChanged()
    {
        if (!m_GameStarted)
        {
            SetSettingsChanged(true);
        }

        GeneralSettings.SetVolumePercentage(m_VolumeScrollbar.value);

        if (m_MainMenuAudio != null)
        {
            m_MainMenuAudio.volume = m_VolumeScrollbar.value;
        }
    }

    [SerializeField]
    private Button m_SaveButton;
    public void SetSettingsChanged(bool p_Changed)
    {
        m_SettingsChanged = p_Changed;
        if (m_SaveButton != null)
        {
            m_SaveButton.interactable = p_Changed;
        }
    }

    private bool m_SettingsChanged = false;
    private bool m_SettingsReset = false;
    public void SaveSettings()
    {
        if (!m_SettingsChanged)
        {
            return;
        }

        SetSettingsChanged(false);
        GeneralSettings.SaveGeneralSettings();

        if (!m_SettingsReset)
        {
            var Rebinds = m_Actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("Rebinds", Rebinds);
        }
        else
        {
            PlayerPrefs.DeleteKey("Rebinds");
        }
    }

    public void ResetSettingsToDefault()
    {
        GeneralSettings.ResetDefaultSettings();

        m_SettingsReset = true;
    }

    public void ResetSettingsToPrevious()
    {
        GeneralSettings.ResetPreviousSettings();

        var Rebinds = PlayerPrefs.GetString("Rebinds");
        if (!string.IsNullOrEmpty(Rebinds))
        {
            m_Actions.LoadBindingOverridesFromJson(Rebinds);
        }
        else
        {
            m_Actions.RemoveAllBindingOverrides();
        }

        m_SettingsChanged = false;
    }

    [SerializeField]
    private UnityEvent OnBackButtonPressed_Saved;
    [SerializeField]
    private UnityEvent OnBackButtonPressed_UnSaved;
    public void BackButtonPressed()
    {
        if (!m_SettingsChanged)
        {
            OnBackButtonPressed_Saved?.Invoke();
        }
        else
        {
            OnBackButtonPressed_UnSaved?.Invoke();
        }
    }
}
