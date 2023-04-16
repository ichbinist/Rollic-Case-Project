using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : BaseScreen
{
    private void OnEnable()
    {
        LevelInitializationManager.Instance.OnLevelLoaded += Close;
        LevelInitializationManager.Instance.OnLevelRestarted += Close;
        LevelInitializationManager.Instance.OnLevelStarted += Open;
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelLoaded -= Close;
            LevelInitializationManager.Instance.OnLevelRestarted -= Close;
            LevelInitializationManager.Instance.OnLevelStarted -= Open;
        }
    }
}
