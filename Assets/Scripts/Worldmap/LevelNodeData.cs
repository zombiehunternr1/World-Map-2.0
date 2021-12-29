using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNodeData : MonoBehaviour
{
    public enum Direction { UP = 1, DOWN = 2, RIGHT = 3, LEFT = 4 }
    public List<Direction> availableConnectedPaths;
    public List<PathLayout> connectedPaths;
    public List<Direction> isPreviousPath;
    public LevelData levelInfo;
    public Material levelNodeMat;

    private void OnEnable()
    {
        levelInfo.position = transform.position;
    }
}
