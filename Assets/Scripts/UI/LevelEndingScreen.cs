using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndingScreen : BaseScreen
{
    public BaseScreen FailScreen, SuccessScreen;
    private void OnEnable()
    {
        LevelInitializationManager.Instance.OnLevelFinalized += FinalizeLevel;
        LevelInitializationManager.Instance.OnLevelStarted += Close;
        LevelInitializationManager.Instance.OnLevelRestarted += Close;
        LevelInitializationManager.Instance.OnLevelLoaded += Close;
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelFinalized -= FinalizeLevel;
            LevelInitializationManager.Instance.OnLevelStarted -= Close;
            LevelInitializationManager.Instance.OnLevelRestarted -= Close;
            LevelInitializationManager.Instance.OnLevelLoaded -= Close;
        }
    }

    private void FinalizeLevel(bool state)
    {
        Open(LevelInitializationManager.Instance.GetCurrentLevelData);

        if (state)
        {
            SuccessScreen.Open(LevelInitializationManager.Instance.GetCurrentLevelData);
            FailScreen.Close(LevelInitializationManager.Instance.GetCurrentLevelData);
        }
        else
        {
            SuccessScreen.Close(LevelInitializationManager.Instance.GetCurrentLevelData);
            FailScreen.Open(LevelInitializationManager.Instance.GetCurrentLevelData);
        }
    }

    public override void Close(LevelData levelData)
    {
        SuccessScreen.Close(levelData);
        FailScreen.Close(levelData);
        base.Close(levelData);
    }
}