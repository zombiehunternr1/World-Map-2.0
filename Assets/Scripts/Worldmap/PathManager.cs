using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]
    private List<PathLayout> pathsInWorld;
    [SerializeField]
    private WorldData worldDataContainer;
    [SerializeField]
    private PathNavigator pathNavigator;

    private void OnEnable()
    {
        PathLayout[] allPaths = GetComponentsInChildren<PathLayout>();
        pathsInWorld = new List<PathLayout>();

        foreach(PathLayout path in allPaths)
        {
            pathsInWorld.Add(path);
        }
        CheckAlreadyUnlockedPaths();
    }

    private void CheckAlreadyUnlockedPaths()
    {
        for(int i = 0; i < worldDataContainer.pathsInWorld.Count; i++)
        {
            if (worldDataContainer.pathsInWorld[i].unlocked)
            {
                if (worldDataContainer.pathsInWorld[i].firstTime == false)
                {
                    pathsInWorld[i].unlocked = worldDataContainer.pathsInWorld[i].unlocked;
                    pathsInWorld[i].GetComponent<PathDecoration>().firstTime = worldDataContainer.pathsInWorld[i].firstTime;
                }
                else
                {
                    PathDecoration pathDecor = pathsInWorld[i].GetComponent<PathDecoration>();
                    pathDecor.firstTime = worldDataContainer.pathsInWorld[i].firstTime;
                    pathsInWorld[i].unlocked = worldDataContainer.pathsInWorld[i].unlocked;
                    pathDecor.StartCoroutine(pathDecor.DecoratePath());
                }
            }
        }
        CheckFirstTimeUnlocked();
    }
    private void CheckFirstTimeUnlocked()
    {
        for (int i = 0; i < pathsInWorld.Count; i++)
        {
            if (pathsInWorld[i].unlocked)
            {
                if (pathsInWorld[i].GetComponent<PathDecoration>().firstTime)
                {
                    pathNavigator.CanMove = false;
                    worldDataContainer.pathsInWorld[i].unlocked = true;
                    return;
                }
            }
        }
    }
    public void UpdateUnlockedStatus(PathData path, bool firstTimeUnlocked)
    {
        path.unlocked = true;
        path.firstTime = firstTimeUnlocked;
    }
}
