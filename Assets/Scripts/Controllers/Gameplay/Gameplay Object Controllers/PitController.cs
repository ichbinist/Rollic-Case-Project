using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PitController : GameplayObjectController
{
    [ReadOnly]
    public Vector3 StartingPosition;
    [ReadOnly]
    public Vector3 LocalPlatformStartingPosition;
    [ReadOnly]
    public bool IsPitCountReached;
    public GameObject LocalPlatform;

    public override void ResetData(LevelData levelData)
    {
        transform.localPosition = StartingPosition;
        IsPitCountReached = false;
        Graphics.SetActive(true);
        LocalPlatform.transform.localPosition = LocalPlatformStartingPosition;
        LocalPlatform.transform.localScale = Vector3.zero;
        LocalPlatform.SetActive(false);
    }

    public override void SaveData()
    {
        StartingPosition = transform.position;
        LocalPlatformStartingPosition = LocalPlatform.transform.localPosition;
    }
}
