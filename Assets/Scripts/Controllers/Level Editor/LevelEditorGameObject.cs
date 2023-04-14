using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGameObject : MonoBehaviour
{
    public EditorObjectType EditorObjectType;

    private void Awake()
    {
        LevelEditorManager.Instance.LevelEditorGameObjects.Add(this);
    }

    private void OnDestroy()
    {
        if(LevelEditorManager.Instance)
            LevelEditorManager.Instance.LevelEditorGameObjects.Remove(this);
    }
}

public enum EditorObjectType
{
    Collectable,
    Pit,
    Powerup
}