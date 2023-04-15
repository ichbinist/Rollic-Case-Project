using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public GameObject Graphics;

    public bool IsScreenActive
    {
        get { return (Graphics.activeSelf); }
    }
    public virtual void Open(LevelData levelData)
    {
        Graphics.SetActive(true);
    }

    public virtual void Close(LevelData levelData)
    {
        Graphics.SetActive(false);
    }
}
