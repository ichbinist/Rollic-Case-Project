using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using System.IO;
    using UnityEditor;
#endif

public class LevelEditorManager : Singleton<LevelEditorManager>
{
    [FoldoutGroup("Level Data Settings")]
    public string ScriptableObjectName;
    [FoldoutGroup("Level Data Settings")]
    public float LevelLength;
    [FoldoutGroup("Level Data Settings")]
    public float LevelWidth;

    [FoldoutGroup("Level Editor Settings")]
    public SpriteRenderer SpriteRenderer;

    [FoldoutGroup("Level Editor Component Settings")]
    public List<LevelEditorComponent> LevelEditorComponents = new List<LevelEditorComponent>();
    
    [FoldoutGroup("Level Editor Component Settings")]
    [ReadOnly]
    public List<LevelEditorGameObject> LevelEditorGameObjects = new List<LevelEditorGameObject>();

    [FoldoutGroup("Level Editor Component Settings")]
    [ReadOnly]
    public LevelEditorComponent CurrentSelectedComponent;

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey && !e.isMouse)
            {
                foreach (LevelEditorComponent levelEditorComponent in LevelEditorComponents)
                {
                    levelEditorComponent.SelectionControl(e.keyCode);
                }
            }

            if (e.isMouse)
            {
                Vector3 pressedLocation = Input.mousePosition;

                pressedLocation = Camera.main.ScreenToWorldPoint(pressedLocation + Vector3.forward * 10f);

                if (e.button == 0 && CurrentSelectedComponent != null)
                {
                    CurrentSelectedComponent.Place(pressedLocation);
                }

                if(e.button == 1)
                {
                    if(LevelEditorGameObjects.Count > 0)
                    {
                        LevelEditorGameObject[] foundObjects = LevelEditorGameObjects.FindAll(x => Vector3.Distance(x.transform.position, pressedLocation) < 0.1f).ToArray();
                        foreach (LevelEditorGameObject levelEditorGameObject in foundObjects)
                        {
                            Destroy(levelEditorGameObject.gameObject);
                        }
                    }
                }
            }
        }
    }

    [FoldoutGroup("Level Data Settings")]
    [Button]
    public void LoadLevelData(LevelData levelData)
    {
        if (levelData == null)
        {
            Debug.LogError("Please Assign a Level Data!");
            return;
        }
        LevelEditorGameObjects = new List<LevelEditorGameObject>();
        ScriptableObjectName = levelData.name;
        LevelLength = levelData.LevelLength;
        LevelWidth = levelData.LevelWidth;
    }

    [FoldoutGroup("Level Data Settings")]
    [Button]
    public void CreateNewLevelData()
    {
        SpriteRenderer.size = new Vector2(LevelWidth, LevelLength);
        SpriteRenderer.transform.localPosition = new Vector3(0, 0, SpriteRenderer.size.y / 2);
    }

#if UNITY_EDITOR
    [FoldoutGroup("Level Data Settings")]
    [Button]
    public void SaveLevelData()
    {
        if (string.IsNullOrEmpty(ScriptableObjectName))
        {
            Debug.LogError("Scriptable object name is not set!");
            return;
        }

        var asset = ScriptableObject.CreateInstance<LevelData>();

        string folderPath = "Assets/Resources/Data/Scriptable Objects/Maze Data";
        string filePath = Path.Combine(folderPath, $"{ScriptableObjectName}.asset");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        AssetDatabase.CreateAsset(asset, filePath);

        //Move Level data inside asset variable

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Saved cell list to {filePath}!");
    }
#endif


}
