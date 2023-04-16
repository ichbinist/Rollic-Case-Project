using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Lean.Touch;

public class PickerMovementController : MonoBehaviour
{
    [FoldoutGroup("Picker Settings")]
    public float HorizontalSpeed = 8f;
    [FoldoutGroup("Picker Settings")]
    public float VerticalSpeed = 1f;
    [FoldoutGroup("Picker Settings")]
    public float OverallSpeed = 5f;

    [ReadOnly]
    public bool IsMovementStarted = false;

    private float targetVerticalPosition;
    private float localStartPosition = 0f;
    public float HorizontalMovementIndicator = 1f;

    private Rigidbody _rigidbody;
    public Rigidbody _Rigidbody { get { return (_rigidbody == null) ? _rigidbody = GetComponent<Rigidbody>() : _rigidbody; } }

    private PickerObjectController pickerObjectController;
    public PickerObjectController PickerObjectController { get { return (pickerObjectController == null) ? pickerObjectController = GetComponent<PickerObjectController>() : pickerObjectController; } }

    private void OnEnable()
    {
        LeanTouch.OnFingerUpdate += OnFingerSwerve;
        LeanTouch.OnFingerDown += OnFingerDown;
        LevelInitializationManager.Instance.OnLevelStarted += OnLevelStarted;
        GameEventManager.Instance.OnPickerMovementFinalized += OnLevelFinalized;
        GameEventManager.Instance.OnPickerMovementStart += () =>  HorizontalMovementIndicator = 1f;
        GameEventManager.Instance.OnPickerMovementStop += () =>  HorizontalMovementIndicator = 0f;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUpdate -= OnFingerSwerve;
        LeanTouch.OnFingerDown -= OnFingerDown;
        if (LevelInitializationManager.Instance)
        {
            LevelInitializationManager.Instance.OnLevelStarted -= OnLevelStarted;
        }
        if (GameEventManager.Instance)
        {
            GameEventManager.Instance.OnPickerMovementFinalized -= OnLevelFinalized;
            GameEventManager.Instance.OnPickerMovementStart -= () => HorizontalMovementIndicator = 1f;
            GameEventManager.Instance.OnPickerMovementStop -= () => HorizontalMovementIndicator = 0f;
        }
    }

    private void OnFingerDown(LeanFinger finger)
    {
        localStartPosition = transform.position.x;
    }

    private void OnFingerSwerve(LeanFinger finger)
    {
        if (finger.Old && !finger.Up)
        {
            Vector2 swerveDirection = finger.LastScreenPosition - finger.StartScreenPosition;
            float targetValue = swerveDirection.x / Screen.width;
            float targetValueByPlatform = targetValue * LevelInitializationManager.Instance.GetCurrentLevelData.LevelWidth * VerticalSpeed;
            targetVerticalPosition = Mathf.Clamp(localStartPosition + targetValueByPlatform, -(LevelInitializationManager.Instance.GetCurrentLevelData.LevelWidth/2 - 1f), LevelInitializationManager.Instance.GetCurrentLevelData.LevelWidth/2 - 1f);
        }
    }

    private void OnLevelStarted(LevelData levelData)
    {
        IsMovementStarted = true;
    }

    private void OnLevelFinalized(float startingZPosition)
    {
        IsMovementStarted = false;
        PickerObjectController.StartingPosition.z = startingZPosition;
    }

    private void FixedUpdate()
    {
        if (IsMovementStarted)
        {
            Vector3 targetPosition = new Vector3(targetVerticalPosition, 0f, _Rigidbody.position.z + HorizontalSpeed * OverallSpeed * HorizontalMovementIndicator);
            _Rigidbody.MovePosition(targetPosition); // = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * OverallSpeed); //transform.position + 
        }

    }
}
