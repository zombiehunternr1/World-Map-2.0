using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Worldmap/LevelInfo")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public string levelName;
    public Vector3 position;
}
