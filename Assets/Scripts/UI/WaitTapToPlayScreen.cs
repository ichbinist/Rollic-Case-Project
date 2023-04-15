using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
public class WaitTapToPlayScreen : BaseScreen
{
    private void OnEnable()
    {
        LevelInitializationManager.Instance.OnLevelLoaded += Open;
        LevelInitializationManager.Instance.OnLevelStarted += Close;
        LeanTouch.OnFingerDown += OnLeanTouch;
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelLoaded -= Open;
            LevelInitializationManager.Instance.OnLevelStarted -= Close;
        }
        LeanTouch.OnFingerDown -= OnLeanTouch;
    }

    private void OnLeanTouch(LeanFinger finger)
    {
        if (IsScreenActive)
        {
            LevelInitializationManager.Instance.OnLevelStarted.Invoke(LevelInitializationManager.Instance.GetCurrentLevelData);
        }
    }
}
