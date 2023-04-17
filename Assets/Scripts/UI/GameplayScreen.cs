using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameplayScreen : BaseScreen
{
    public TextMeshProUGUI LevelText;

    private void OnEnable()
    {
        LevelInitializationManager.Instance.OnLevelLoaded += Close;
        LevelInitializationManager.Instance.OnLevelRestarted += Close;
        LevelInitializationManager.Instance.OnLevelStarted += Open;
        LevelInitializationManager.Instance.OnLevelStarted += SetText;
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelLoaded -= Close;
            LevelInitializationManager.Instance.OnLevelRestarted -= Close;
            LevelInitializationManager.Instance.OnLevelStarted -= Open;
            LevelInitializationManager.Instance.OnLevelStarted -= SetText;
        }
    }

    private void SetText(LevelData levelData)
    {
        LevelText.SetText("Level " + LevelInitializationManager.Instance.FakeLevel);
    }
}
