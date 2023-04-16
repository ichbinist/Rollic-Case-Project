using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayGameObjectComponent : MonoBehaviour
{
    [FoldoutGroup("Component Data")]
    public EditorObjectType EditorObjectType;

    private GameplayObjectController gameplayObjectController;
    public GameplayObjectController GameplayObjectController { get { return (gameplayObjectController == null) ? gameplayObjectController = GetComponent<GameplayObjectController>() : gameplayObjectController; } }

    public void Initialize()
    {
        GameplayObjectController.SaveData();
    }
}