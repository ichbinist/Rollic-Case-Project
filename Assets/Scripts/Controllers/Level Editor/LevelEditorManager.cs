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

    [FoldoutGroup("Level Editor Component Placement Settings")]
    public int VerticalSliceCount = 5;
    [FoldoutGroup("Level Editor Component Placement Settings")]
    public float HorizontalSliceRatio = 1.25f;
    [FoldoutGroup("Level Editor Component Placement Settings")]
    public float HorizontalPlacementOffset = -0.5f;

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
                        LevelEditorGameObject[] foundObjects = LevelEditorGameObjects.FindAll(x => Vector3.Distance(x.transform.position, pressedLocation) < 0.35f).ToArray();
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

        foreach (LevelEditorGameObject levelEditorGameObject in LevelEditorGameObjects)
        {
            Destroy(levelEditorGameObject.gameObject);
        }

        LevelEditorGameObjects = new List<LevelEditorGameObject>();

        ScriptableObjectName = levelData.name;
        LevelLength = levelData.LevelLength;
        LevelWidth = levelData.LevelWidth;

        CreateNewLevelData();
        CreateLoadedDataGameObjects(levelData);
    }

    [FoldoutGroup("Level Data Settings")]
    [Button]
    public void CreateNewLevelData()
    {
        SpriteRenderer.size = new Vector2(LevelWidth, LevelLength);
        SpriteRenderer.transform.localPosition = new Vector3(0, 0, SpriteRenderer.size.y / 2);
    }

    private void CreateLoadedDataGameObjects(LevelData levelData)
    {
        foreach (LevelComponentData levelComponentData in levelData.LevelComponentDatas)
        {
            LevelEditorComponents.Find(x => x.LevelEditorGameObject.EditorObjectType == levelComponentData.EditorObjectType).Place(levelComponentData.Position);

            if(levelComponentData.EditorObjectType == EditorObjectType.Pit)
            {
                (LevelEditorGameObjects[LevelEditorGameObjects.Count - 1] as PitGameObject).PitRequiredCount = levelComponentData.PitRequiredCount;
            }
        }
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

        string folderPath = "Assets/Data/Level Data";
        string filePath = Path.Combine(folderPath, $"{ScriptableObjectName}.asset");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        AssetDatabase.CreateAsset(asset, filePath);

        asset.LevelLength = LevelLength;
        asset.LevelWidth = LevelWidth;

        foreach (LevelEditorGameObject levelEditorGameObject in LevelEditorGameObjects)
        {
            LevelComponentData levelComponentData = new LevelComponentData(levelEditorGameObject.EditorObjectType, levelEditorGameObject.transform.position, 0);

            if(levelComponentData.EditorObjectType == EditorObjectType.Pit)
            {
                levelComponentData.PitRequiredCount = (levelEditorGameObject as PitGameObject).PitRequiredCount;
            }

            asset.LevelComponentDatas.Add(levelComponentData);
        }

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Saved cell list to {filePath}!");
    }
#endif


}
