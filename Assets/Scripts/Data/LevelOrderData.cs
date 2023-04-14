using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LevelOrderData", menuName = "Data/LevelOrderData", order = 2)]
public class LevelOrderData : ScriptableObject
{
    public List<LevelData> LevelOrder = new List<LevelData>();
}