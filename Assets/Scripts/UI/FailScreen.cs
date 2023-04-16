using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailScreen : BaseScreen
{
    public Button ScreenButton;
    private void Start()
    {
        ScreenButton.onClick.AddListener(() => {
            RestartLevel();
        });
    }

    private void OnDestroy()
    {
        if (LevelInitializationManager.Instance)
        {
            ScreenButton.onClick.RemoveListener(() => {
                RestartLevel();
            });
        }
    }

    private void RestartLevel()
    {
        LevelInitializationManager.Instance.RestartLevel();
    }
}