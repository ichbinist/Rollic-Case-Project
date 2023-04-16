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

    public override void ResetData(LevelData levelData)
    {
        _Rigidbody.isKinematic = true;
        _Rigidbody.useGravity = false;
        _Rigidbody.velocity = Vector3.zero;
        _Rigidbody.angularVelocity = Vector3.zero;
        transform.localPosition = StartingPosition;
        isCollected = false;
        Graphics.SetActive(true);
    }

    public override void SaveData()
    {
        StartingPosition = transform.position;
    }
}
