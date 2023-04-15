using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessScreen : BaseScreen
{
    public Button ScreenButton;

    private void OnEnable()
    {
        ScreenButton.onClick.AddListener(() => {
            LevelInitializationManager.Instance.OnLevelFinished.Invoke(LevelInitializationManager.Instance.GetCurrentLevelData);
        });
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            ScreenButton.onClick.RemoveListener(() => {
                LevelInitializationManager.Instance.OnLevelFinished.Invoke(LevelInitializationManager.Instance.GetCurrentLevelData);
            });
        }
    }
}
