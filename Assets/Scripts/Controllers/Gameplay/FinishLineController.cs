using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishLineController : MonoBehaviour
{
    public float NewLevelForwardPosition = 2.5f;
    public void OnTriggerEnter(Collider other)
    {
        PickerMovementController pickerMovementController = other.GetComponent<PickerMovementController>();
        if (pickerMovementController)
        {
            GameEventManager.Instance.OnPickerMovementFinalized.Invoke(transform.position.z + NewLevelForwardPosition);
            pickerMovementController.gameObject.transform.DOMove(new Vector3(0f, 0f, transform.position.z + NewLevelForwardPosition), 1.25f).OnComplete(()=> { LevelInitializationManager.Instance.OnLevelFinalized.Invoke(true); });
        }
    }
}
