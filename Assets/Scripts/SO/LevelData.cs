using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Worldmap/LevelInfo")]
public class LevelData : ScriptableObject
{
    public int LevelNumber;
    public string LevelName;
}
