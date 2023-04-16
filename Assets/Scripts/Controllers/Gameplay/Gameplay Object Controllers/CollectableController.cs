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

    private void OnEnable()
    {
        GameEventManager.Instance.OnTaggedObjectsClear += Dissappear;
    }

    private void OnDisable()
    {
        if (GameEventManager.Instance)
        {
            GameEventManager.Instance.OnTaggedObjectsClear -= Dissappear;
        }
    }

    private void Dissappear()
    {
        if (isTagged)
        {
            transform.localScale = Vector3.zero;
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
        StartingPosition = transform.position;
    }
}
