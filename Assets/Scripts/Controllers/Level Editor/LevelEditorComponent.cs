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

    public float[] AlignPositionVector(float width, int numberOfSlices, float spacing, float forwardPositionOffset = 0)
    {
        float sliceWidth = width / (numberOfSlices - 1);
        float[] positions = new float[numberOfSlices];

        for (int i = 0; i < numberOfSlices; i++)
        {
            positions[i] = (i * sliceWidth) - (width / 2.0f);
            if(i <= (numberOfSlices / 2 - 1))
            {
                positions[i] -= Mathf.Abs(i - ((numberOfSlices - 1)/2f)) * spacing;
            }
            else
            {
                positions[i] += Mathf.Abs(i - ((numberOfSlices - 1)/2f)) * spacing;
            }
            positions[i] += forwardPositionOffset;
        }

        return positions;
    }

    public float GetClosestAlignedXPosition(Vector3 position, float[] alignedPositions)
    {
        float minDifference = Mathf.Infinity;
        int closestIndex = 0;

        for (int i = 0; i < alignedPositions.Length; i++)
        {
            float difference = Mathf.Abs(position.x - alignedPositions[i]);
            if (difference < minDifference)
            {
                minDifference = difference;
                closestIndex = i;
            }
        }

        return alignedPositions[closestIndex];
    }

    public float GetClosestAlignedZPosition(Vector3 position, float[] alignedPositions)
    {
        float minDiff = Mathf.Infinity;
        int closestIndex = 0;

        for (int i = 0; i < alignedPositions.Length; i++)
        {
            float diff = Mathf.Abs(position.z - alignedPositions[i]);
            if (diff < minDiff)
            {
                minDiff = diff;
                closestIndex = i;
            }
        }

        return alignedPositions[closestIndex];
    }

    public virtual void Place(Vector3 position)
    {
        float[] alignPositions = AlignPositionVector(LevelEditorManager.Instance.LevelWidth, LevelEditorManager.Instance.VerticalSliceCount, LevelEditorManager.Instance.HorizontalPlacementOffset);

        float closestPosition = GetClosestAlignedXPosition(position, alignPositions);

        position.x = closestPosition;


        alignPositions = AlignPositionVector(LevelEditorManager.Instance.LevelLength, Mathf.RoundToInt(LevelEditorManager.Instance.LevelLength * LevelEditorManager.Instance.HorizontalSliceRatio), 0, LevelEditorManager.Instance.LevelLength / 2);

        closestPosition = GetClosestAlignedZPosition(position, alignPositions);

        position.z = closestPosition;


        LevelEditorGameObject createdGameObject = Instantiate(LevelEditorGameObject, position, Quaternion.identity);
    }

    public virtual void Remove()
    {

    }
}
