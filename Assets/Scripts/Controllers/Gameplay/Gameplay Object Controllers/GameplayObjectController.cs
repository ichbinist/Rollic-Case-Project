using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayObjectController : MonoBehaviour
{
    public GameObject Graphics;

    private void Start()
    {
        LevelInitializationManager.Instance.OnLevelRestarted += ResetData;
    }

    private void OnDestroy()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelRestarted -= ResetData;
        }
    }

    public abstract void SaveData();
    public abstract void ResetData(LevelData levelData);

}
