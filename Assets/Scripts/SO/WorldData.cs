using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldData", menuName = "Worldmap/Worldinfo")]
public class WorldData : ScriptableObject
{
    public List<PathData> pathsInWorld;
}
