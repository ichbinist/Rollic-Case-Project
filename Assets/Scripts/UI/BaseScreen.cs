using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public GameObject Graphics;

    protected virtual void Open()
    {
        Graphics.SetActive(true);
    }

    protected virtual void Close()
    {
        Graphics.SetActive(false);

    }
}
