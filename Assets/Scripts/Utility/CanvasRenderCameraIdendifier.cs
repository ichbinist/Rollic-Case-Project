using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///INFO
///->Usage of CanvasRenderCameraIdendifier script: 
///ENDINFO

public class CanvasRenderCameraIdendifier : MonoBehaviour
{
    #region Publics

    #endregion

    #region Privates

    #endregion

    #region Cached

    #endregion

    #region Events

    #endregion

    #region Monobehaviours
    private void Start()
    {
        GetComponent<Canvas>().worldCamera = UICameraDefiner.Instance.UICamera;
    }
    #endregion

    #region Functions

    #endregion

}
