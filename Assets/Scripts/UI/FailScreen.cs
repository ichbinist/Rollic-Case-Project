using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailScreen : BaseScreen
{
    public Button ScreenButton;
    private void OnEnable()
    {
        ScreenButton.onClick.AddListener(() => {
            LevelInitializationManager.Instance.OnLevelRestarted.Invoke(LevelInitializationManager.Instance.GetCurrentLevelData);
        });
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            ScreenButton.onClick.RemoveListener(() => {
                LevelInitializationManager.Instance.OnLevelRestarted.Invoke(LevelInitializationManager.Instance.GetCurrentLevelData);
            });
        }
    }
}