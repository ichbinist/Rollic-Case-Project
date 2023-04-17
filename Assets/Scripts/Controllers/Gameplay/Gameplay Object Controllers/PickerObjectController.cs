using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PickerObjectController : GameplayObjectController
{
    [ReadOnly]
    public Vector3 StartingPosition;

    private PickerMovementController pickerMovementController;
    public PickerMovementController PickerMovementController { get { return (pickerMovementController == null) ? pickerMovementController = GetComponent<PickerMovementController>() : pickerMovementController; } }

    private void Start()
    {
        SaveData();
    }

    private void OnEnable()
    {
        LevelInitializationManager.Instance.OnLevelRestarted += ResetData;
        LevelInitializationManager.Instance.OnLevelFinished += ResetData;
    }

    private void OnDisable()
    {
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelRestarted -= ResetData;
            LevelInitializationManager.Instance.OnLevelFinished -= ResetData;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectableController collectable = other.GetComponent<CollectableController>();
        if (collectable != null)
        {
            collectable.isTagged = true;
        }
    }

    public override void ResetData(LevelData levelData)
    {
        transform.position = StartingPosition;
        transform.localScale = Vector3.one;
        PickerMovementController.HorizontalMovementIndicator = 1f;
        PickerMovementController.IsMovementStarted = false;
    }

    public override void SaveData()
    {
        StartingPosition = transform.position;
    }

    public void SizeUp(Vector3 sizeAmount)
    {
        transform.DOScale(transform.localScale + sizeAmount, 0.35f).SetEase(Ease.OutQuart);
    }
}
