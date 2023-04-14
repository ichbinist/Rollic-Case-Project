using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class LevelComponentData
{
    [FoldoutGroup("Component Data")]
    public EditorObjectType EditorObjectType;
    [FoldoutGroup("Component Data")]
    public Vector3 Position;
    [FoldoutGroup("Component Data")]
    public int PitRequiredCount;

    public LevelComponentData(EditorObjectType editorObjectType, Vector3 position, int pitRequiredCount)
    {
        EditorObjectType = editorObjectType;
        Position = position;
        PitRequiredCount = pitRequiredCount;
    }
}
