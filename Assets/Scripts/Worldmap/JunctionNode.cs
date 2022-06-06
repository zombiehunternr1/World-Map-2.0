using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionNode : MonoBehaviour
{
    public enum Direction { UP = 1, DOWN = 2, RIGHT = 3, LEFT = 4 }
    public List<Direction> allAvailableDirectionOptions;
    public List<Direction> allPreviousDirectionOptions;
    public List<PathLayout> connectedPaths;
    public Material junctionMat;
}
