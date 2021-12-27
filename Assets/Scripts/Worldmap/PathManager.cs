using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]
    private List<PathLayout> pathsInWorld;

    private void OnEnable()
    {
        PathLayout[] allPaths = GetComponentsInChildren<PathLayout>();
        pathsInWorld = new List<PathLayout>();

        foreach(PathLayout path in allPaths)
        {
            pathsInWorld.Add(path);
        }
        CheckFirstTimeUnlocked();
    }

    private void CheckFirstTimeUnlocked()
    {
        for(int i = 0; i < pathsInWorld.Count; i++)
        {
            if (pathsInWorld[i].unlocked)
            {
                if (pathsInWorld[i].GetComponent<PathDecoration>().firstTime)
                {
                    PathNavigator.canMove = false;
                    return;
                }
            }
        }
    }
}
