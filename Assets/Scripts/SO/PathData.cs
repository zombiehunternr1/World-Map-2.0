using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathData", menuName = "Worldmap/PathInfo")]
public class PathData : ScriptableObject
{
    public bool unlocked;
    public bool firstTime;
}
