using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelInitializationManager : Singleton<LevelInitializationManager>
{
    [FoldoutGroup("Level Order Data Reference")]
    public LevelOrderData LevelOrderData;
    [FoldoutGroup("Level Order Data Debug")]
    [ReadOnly]
    public int FakeLevel = 0;

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
}
