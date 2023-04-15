using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

///INFO
///->Usage of SceneInitializationManager script: This script Loads the UI Scene and Gameplay Scene Asyncronly.
///ENDINFO

public class SceneInitializationManager : Singleton<SceneInitializationManager>
{
    #region Publics
    #endregion

    #region Privates

    #endregion

    #region Cached

    #endregion

    #region Events
    public Action OnGameplaySceneLoaded;
    #endregion

    #region Monobehaviours
    public void Start()
    {
        Application.targetFrameRate = 60;
        Initialization();
    }
    #endregion

    #region Functions
    private void Initialization()
    {
        StartGameplay();
    }

    public void StartGameplay()
    {
        StartCoroutine(DOLoadGameplay());
    }

    private IEnumerator DOLoadGameplay()
    {
        yield return StartCoroutine(DoLoadScene("UI"));
        yield return StartCoroutine(DoLoadScene("Gameplay"));
        OnGameplaySceneLoaded.Invoke();
    }

    private IEnumerator DoLoadScene(string SceneName)
    {
        if (!SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        yield return null;
    }

    private IEnumerator DoUnloadScene(string SceneName)
    {
        if (SceneManager.GetSceneByName(SceneName).isLoaded)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(SceneName);
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }
        yield return null;
    }
    #endregion
}