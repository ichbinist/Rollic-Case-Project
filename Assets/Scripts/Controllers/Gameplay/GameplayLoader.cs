using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class GameplayLoader : MonoBehaviour
{
    [FoldoutGroup("References")]
    public GameObject PlatformPrefab;
    [FoldoutGroup("References")]
    public GameObject SidewayPrefab;
    [FoldoutGroup("References")]
    public GameObject EndlinePrefab;
    [FoldoutGroup("References/Gameplay Object References")]
    public List<GameplayGameObjectComponent> GameplayObjects = new List<GameplayGameObjectComponent>();
    [FoldoutGroup("Settings")]
    [FoldoutGroup("Settings/Platform Settings")]
    public float SidewayHalfWidth = 0.075f;
    [FoldoutGroup("Settings")]
    [FoldoutGroup("Settings/Platform Settings")]
    public float PitHalfLength = 1.5f;
    [FoldoutGroup("Debug")]
    [ShowInInspector]
    [ReadOnly]
    internal List<localLevelData> LocalLevelDatas = new List<localLevelData>();

    private float currentTargetLength;

    private void OnEnable()
    {
        LevelInitializationManager.Instance.OnLevelLoaded += LoadLevel;
        LevelInitializationManager.Instance.OnLevelUnloaded += UnloadLevel;
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelLoaded -= LoadLevel;
            LevelInitializationManager.Instance.OnLevelUnloaded -= UnloadLevel;
        }
    }


    private void LoadLevel(LevelData levelData)
    {
        GameObject levelDataObject = new GameObject(levelData.name);
        levelDataObject.transform.parent = transform;

        LevelComponentData[] pitData = levelData.LevelComponentDatas.FindAll(x => x.EditorObjectType == EditorObjectType.Pit).ToArray();

        InitializePlatforms(pitData, levelData, levelDataObject);
        InitializeSideways(levelDataObject, levelData);
        InitializeGameplayObjects(levelDataObject, levelData);
        localLevelData newLevelData = new localLevelData();
        newLevelData.LevelDataGameObject = levelDataObject;
        newLevelData.levelData = levelData;
        LocalLevelDatas.Add(newLevelData);

        levelDataObject.transform.position = new Vector3(0f, 0f, currentTargetLength);
        currentTargetLength += levelData.LevelLength + 1f;
    }

    private void InitializePlatforms(LevelComponentData[] pitData, LevelData levelData, GameObject levelDataObject)
    {
        float currentLength = 0f;

        if(pitData.Length > 0)
        {
            GameObject platform = AddPlatform(levelDataObject, levelData.LevelWidth, pitData[0].Position.z - PitHalfLength);
            currentLength = pitData[0].Position.z + PitHalfLength;
            for (int i = 0; i < pitData.Length; i++)
            {
                if (pitData.Length == 1) break;
                if (i == 0) continue;
                platform = AddPlatform(levelDataObject, levelData.LevelWidth, pitData[i].Position.z - PitHalfLength);
                platform.transform.localPosition = new Vector3(0, 0, currentLength);
                currentLength = pitData[i].Position.z + PitHalfLength;
            }
            platform = AddPlatform(levelDataObject, levelData.LevelWidth, levelData.LevelLength - currentLength);
            platform.transform.localPosition = new Vector3(0, 0, currentLength);

        }
        else
        {
            AddPlatform(levelDataObject, levelData.LevelWidth, levelData.LevelLength);
        }

        GameObject endlinePlatform = AddPlatform(levelDataObject, 1f, 1f, EndlinePrefab);
        endlinePlatform.transform.localPosition = new Vector3(0, 0, levelData.LevelLength + 0.5f);
    }

    private void InitializeGameplayObjects(GameObject levelDataObject, LevelData levelData)
    {
        GameObject objectComponents = new GameObject(levelData.name + " Gameplay Objects");
        objectComponents.transform.parent = levelDataObject.transform;
        foreach (LevelComponentData levelComponentData in levelData.LevelComponentDatas)
        {
            GameplayGameObjectComponent gameplayGameObject = Instantiate(GameplayObjects.Find(x => x.EditorObjectType == levelComponentData.EditorObjectType), objectComponents.transform);
            if(gameplayGameObject.EditorObjectType == EditorObjectType.Pit)
            {
                (gameplayGameObject as PitGameObjectComponent).PitRequiredCount = levelComponentData.PitRequiredCount;
            }
            gameplayGameObject.transform.localPosition = new Vector3(levelComponentData.Position.x, 0.1f, levelComponentData.Position.z);
        }
    }

    private void InitializeSideways(GameObject levelDataObject, LevelData levelData)
    {
        AddSideway(levelDataObject, levelData, levelData.LevelWidth / 2f + SidewayHalfWidth);
        AddSideway(levelDataObject, levelData, -(levelData.LevelWidth / 2f + SidewayHalfWidth));
    }

    private void AddSideway(GameObject levelDataObject, LevelData levelData, float xPosition)
    {
        GameObject sideway = Instantiate(SidewayPrefab, levelDataObject.transform);
        sideway.transform.localScale = new Vector3(1f, 1f, levelData.LevelLength);
        sideway.transform.localPosition = new Vector3(xPosition, 0f, 0f);
    }

    private GameObject AddPlatform(GameObject levelDataObject,float width, float length, GameObject platformPrefab = null)
    {
        GameObject platform;

        if (platformPrefab)
        {
            platform = Instantiate(platformPrefab, levelDataObject.transform);
        }
        else
        {
            platform = Instantiate(PlatformPrefab, levelDataObject.transform);
        }

        platform.transform.localScale = new Vector3(width, 1f, length);
        return platform;
    }

    private void UnloadLevel(LevelData levelData)
    {
        localLevelData localLevelData = LocalLevelDatas.Find(x => x.levelData == levelData);
        LocalLevelDatas.Remove(localLevelData);
        Destroy(localLevelData.LevelDataGameObject);
    }
}


internal class localLevelData
{
    internal LevelData levelData;
    internal GameObject LevelDataGameObject;
}