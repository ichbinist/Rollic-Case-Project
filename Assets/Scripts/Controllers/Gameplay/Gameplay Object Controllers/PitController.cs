using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class PitController : GameplayObjectController
{
    [ReadOnly]
    public Vector3 StartingPosition;
    [ReadOnly]
    public Vector3 LocalPlatformStartingPosition;
    [ReadOnly]
    public bool IsPitCountReached;
    public GameObject LocalPlatform;

    public float ResultWaitTime = 2.5f;

    private bool isPickerEntered = false;

    private List<GameObject> collectedCollectables = new List<GameObject>();

    private PitGameObjectComponent pitGameObjectComponent;
    public PitGameObjectComponent PitGameObjectComponent
    {
        get { return (pitGameObjectComponent == null) ? pitGameObjectComponent = GetComponent<PitGameObjectComponent>() : pitGameObjectComponent; }
    }

    public TMPro.TextMeshProUGUI CollectableCountText;
    public override void ResetData(LevelData levelData)
    {
        transform.localPosition = StartingPosition;
        IsPitCountReached = false;
        Graphics.SetActive(true);
        LocalPlatform.transform.localPosition = LocalPlatformStartingPosition;
        isPickerEntered = false;
        collectedCollectables.Clear();
        UpdateText();
    }

    public override void SaveData()
    {
        StartingPosition = transform.position;
        LocalPlatformStartingPosition = LocalPlatform.transform.localPosition;
        UpdateText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPickerEntered && other.GetComponent<PickerObjectController>())
        {
            GameEventManager.Instance.OnPickerMovementStop.Invoke();
            StartCoroutine(WaitForResultCoroutine());
            isPickerEntered = true;
        }

        if(other.GetComponent<CollectableController>() && !collectedCollectables.Contains(other.gameObject))
        {
            collectedCollectables.Add(other.gameObject);
            UpdateText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CollectableController>() && collectedCollectables.Contains(other.gameObject))
        {
            collectedCollectables.Remove(other.gameObject);
            UpdateText();
        }
    }

    private void UpdateText()
    {
        CollectableCountText.SetText(collectedCollectables.Count + "/" + PitGameObjectComponent.PitRequiredCount);
    }

    private IEnumerator WaitForResultCoroutine()
    {
        yield return new WaitForSeconds(ResultWaitTime);
        if(collectedCollectables.Count >= PitGameObjectComponent.PitRequiredCount)
        {
            //GameEventManager.Instance.OnPickerMovementStart.Invoke();
            GameEventManager.Instance.OnTaggedObjectsClear.Invoke();
            LocalPlatform.transform.DOMoveY(0f, 0.75f).SetEase(Ease.OutElastic).OnComplete(()=> { GameEventManager.Instance.OnPickerMovementStart.Invoke(); });
        }
        else
        {
            LevelInitializationManager.Instance.OnLevelFinalized.Invoke(false);
        }
    }
}
