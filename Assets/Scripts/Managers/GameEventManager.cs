using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : Singleton<GameEventManager>
{
    public Action OnPickerMovementStop;
    public Action OnPickerMovementStart;
    public Action OnTaggedObjectsClear;
}
