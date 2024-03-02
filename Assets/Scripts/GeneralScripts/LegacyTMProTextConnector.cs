using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegacyTMProTextConnector : MonoBehaviour
{
    [SerializeField]
    private Text m_LegacyText;
    [SerializeField]
    private TMPro.TextMeshProUGUI m_ModernText;

    public void Start()
    {
        UpdateModernTextDisplay();
    }

    public void UpdateModernTextDisplay()
    {
        m_ModernText.text = m_LegacyText.text;
    }
}
