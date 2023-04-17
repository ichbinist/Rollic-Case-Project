using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : GameplayObjectController
{
    [ReadOnly]
    public Vector3 StartingPosition;

    private Rigidbody _rigidbody;
    public Rigidbody _Rigidbody { get { return (_rigidbody == null) ? _rigidbody = GetComponent<Rigidbody>() : _rigidbody; } }
    [ReadOnly]
    public bool isCollected;

    [ReadOnly]
    public bool isTagged;

    private bool isDataSaved;
    private void OnEnable()
    {
        GameEventManager.Instance.OnTaggedObjectsClear += Dissappear;
        GameEventManager.Instance.OnPickerMovementStop += AddForce;
    }

    private void OnDisable()
    {
        if (GameEventManager.Instance)
        {
            GameEventManager.Instance.OnTaggedObjectsClear -= Dissappear;
            GameEventManager.Instance.OnPickerMovementStop -= AddForce;
        }
    }

    private void Dissappear(float zValue)
    {
        if (transform.TransformPoint(StartingPosition).z < zValue || isTagged)
        {
            transform.localScale = Vector3.zero;
        }
    }

    private void AddForce()
    {
        if (isTagged)
        {
            _Rigidbody.AddForce(Vector3.forward * 750);
        }
    }

    public override void ResetData(LevelData levelData)
    {
        isTagged = false;
        _Rigidbody.isKinematic = false;
        _Rigidbody.useGravity = true;
        _Rigidbody.velocity = Vector3.zero;
        _Rigidbody.angularVelocity = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localPosition = StartingPosition;
        isCollected = false;
        Graphics.SetActive(true);
    }

    public override void SaveData()
    {
        if (!isDataSaved)
        {
            isDataSaved = true;
            StartingPosition = transform.position;
        }
    }
}
