using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNodeData : MonoBehaviour
{
    public enum Direction { UP = 1, DOWN = 2, RIGHT = 3, LEFT = 4 }
    public List<Direction> allAvailableDirectionOptions;
    public List<Direction> allPreviousDirectionOptions;
    public List<PathLayout> connectedPaths;
    public LevelData levelData;
    public Material levelNodeMat;

    private void OnEnable()
    {
        levelData.position = transform.position;
    }
}
