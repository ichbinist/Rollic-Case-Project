using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayGameObjectComponent : MonoBehaviour
{
    [FoldoutGroup("Component Data")]
    public EditorObjectType EditorObjectType;
}