using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class LevelInitializationManager : Singleton<LevelInitializationManager>
{
    [FoldoutGroup("Level Order Data Reference")]
    public LevelOrderData LevelOrderData;

    #region Events
    public Action<LevelData> OnLevelLoaded;
    public Action<LevelData> OnLevelUnloaded;
    public Action OnLevelRestarted;
    #endregion

    private void OnEnable()
    {
        SceneInitializationManager.Instance.OnGameplaySceneLoaded += InitializeLevel;
    }

    private void OnDisable()
    {
        if(SceneInitializationManager.Instance)
            SceneInitializationManager.Instance.OnGameplaySceneLoaded -= InitializeLevel;
    }

    public int FakeLevel 
    {
        get
        {
            if (PlayerPrefs.HasKey("LevelOrder"))
            {
                return PlayerPrefs.GetInt("LevelOrder") + 1;
            }
            else
            {
                PlayerPrefs.SetInt("LevelOrder", 0);
                return 1;
            }
        }
    }

    public int DataOrderLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("LevelOrder"))
            {
                return (PlayerPrefs.GetInt("LevelOrder") % LevelOrderData.LevelOrder.Count);
            }
            else
            {
                PlayerPrefs.SetInt("LevelOrder", 0);
                return 0;
            }
        }
    }

    public int NextDataOrderLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("LevelOrder"))
            {
                return ((PlayerPrefs.GetInt("LevelOrder") + 1) % LevelOrderData.LevelOrder.Count);
            }
            else
            {
                PlayerPrefs.SetInt("LevelOrder", 0);
                return 0;
            }
        }
    }

    public LevelData GetCurrentLevelData
    {
        get
        {
            return (LevelOrderData.LevelOrder[DataOrderLevel]);
        }
    }

    public LevelData GetNextLevelData
    {
        get
        {
            return (LevelOrderData.LevelOrder[NextDataOrderLevel]);
        }
    }

    public void LoadLevel(int index)
    {
        OnLevelLoaded.Invoke(LevelOrderData.LevelOrder[index]);
    }

    public void UnloadLevel(int index)
    {
        OnLevelUnloaded.Invoke(LevelOrderData.LevelOrder[index]);
    }

    [Button]
    public void InitializeLevel()
    {
        LoadLevel(DataOrderLevel);
        LoadLevel(NextDataOrderLevel);
    }

    [Button]
    public void FinishLevel()
    {
        UnloadLevel(DataOrderLevel);
        PlayerPrefs.SetInt("LevelOrder", PlayerPrefs.GetInt("LevelOrder") + 1);
        LoadLevel(NextDataOrderLevel);
    }

    [Button]
    public void RestartLevel()
    {
        OnLevelRestarted.Invoke();
    }
}
