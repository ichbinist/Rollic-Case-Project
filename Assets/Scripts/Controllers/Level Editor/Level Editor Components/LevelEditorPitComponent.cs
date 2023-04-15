using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelEditorPitComponent : LevelEditorComponent
{
    public int PitRequiredCount;

    public override void Place(Vector3 position)
    {
        PitGameObject createdGameObject = Instantiate(LevelEditorGameObject, new Vector3(0f, position.y, position.z), Quaternion.identity) as PitGameObject;
        createdGameObject.PitRequiredCount = PitRequiredCount;
    }
}
