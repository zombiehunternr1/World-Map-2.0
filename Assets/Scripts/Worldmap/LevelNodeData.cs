using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNodeData : MonoBehaviour
{
    public enum Direction { UP = 1, DOWN = 2, RIGHT = 3, LEFT = 4 }
    public List<Direction> AvailableConnectedPaths;

    public LevelData LevelInfo;
}
