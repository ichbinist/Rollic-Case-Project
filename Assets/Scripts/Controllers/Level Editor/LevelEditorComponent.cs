using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class LevelEditorComponent : MonoBehaviour, IEditorPlaceable, IEditorRemovable
{
    public KeyCode AssignedKeyCode;

    public bool IsSelected;

    public LevelEditorGameObject LevelEditorGameObject;

    public virtual void SelectionControl(KeyCode key)
    {
        if(key == AssignedKeyCode)
        {
            IsSelected = true;
            LevelEditorManager.Instance.CurrentSelectedComponent = this;
        }
        else
        {
            IsSelected = false;
        }
    }

    public virtual void Place(Vector3 position)
    {
        LevelEditorGameObject createdGameObject = Instantiate(LevelEditorGameObject, position, Quaternion.identity);
    }

    public virtual void Remove()
    {

    }
}
