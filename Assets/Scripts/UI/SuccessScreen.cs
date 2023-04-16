using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessScreen : BaseScreen
{
    public Button ScreenButton;

    private void Start()
    {
        ScreenButton.onClick.AddListener(() => {
            FinishLevel();
        });
    }

    private void OnDestroy()
    {
        if (LevelInitializationManager.Instance)
        {
            ScreenButton.onClick.RemoveListener(() => {
                FinishLevel();
            });
        }
    }

    private void FinishLevel()
    {
        LevelInitializationManager.Instance.FinishLevel();
    }
}
