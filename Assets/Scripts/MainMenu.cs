using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManager;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown m_LevelSelect;
    [SerializeField]
    private MainMenuAnimation m_MenuAnim;
    public void Awake()
    {
        m_LevelSelect.ClearOptions();
        List<string> NewOptions = new List<string>();
        foreach (LevelInfo Level in LevelLoader.giGameInfo.Levels)
        {
            NewOptions.Add(Level.LevelName);
        }
        m_LevelSelect.AddOptions(NewOptions);

        int ClipIdx = Random.Range(0, LevelManager.LevelLoader.giGameInfo.Levels.Count);
        m_MenuAnim.StartAudio(ClipIdx);
        m_LevelSelect.value = ClipIdx;
    }

    public void SelectLevel()
    {
        m_MenuAnim.StartAudio(m_LevelSelect.value);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        //int RIdx = Random.Range(0, LevelManager.LevelLoader.giGameInfo.Levels.Count);
        LevelLoader.LoadLevel(m_LevelSelect.value);
    }
}
