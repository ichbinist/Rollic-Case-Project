﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : GameplayObjectController
{
    [ReadOnly]
    public Vector3 StartingPosition;
    [ReadOnly]
    public bool isCollected;

    public override void ResetData(LevelData levelData)
    {
        transform.localPosition = StartingPosition;
        isCollected = false;
        Graphics.SetActive(true);
    }

    public override void SaveData()
    {
        StartingPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        PickerObjectController pickerObjectController = other.GetComponent<PickerObjectController>();
        
        if (pickerObjectController)
        {
            if(isCollected == false)
            {
                isCollected = true;
                pickerObjectController.SizeUp(Vector3.one * 0.35f);
                Graphics.SetActive(false);
            }
        }
    }
}
