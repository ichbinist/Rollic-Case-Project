using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndingScreen : BaseScreen
{
    public BaseScreen FailScreen, SuccessScreen;
    private void OnEnable()
    {
        LevelInitializationManager.Instance.OnLevelFinalized += FinalizeLevel;
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelFinalized -= FinalizeLevel;
        }
    }

    private void FinalizeLevel(bool state)
    {
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
}