using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public LevelType LevelType;
    public float LevelLength;
    public float LevelWidth;

    public List<LevelComponentData> LevelComponentDatas = new List<LevelComponentData>();
}
public enum LevelType
{
    Default
}