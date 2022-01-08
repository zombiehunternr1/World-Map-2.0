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

    public void UpdateUnlockedStatus(PathData path, bool firstTimeUnlocked)
    {
        path.unlocked = true;
        path.firstTime = firstTimeUnlocked;
        CheckUnlockingMovement();
    }
    public void CheckUnlockingMovement()
    {
        pathNavigator.canMove = CheckFirstTimeUnlocked();
    }

    private void CheckAlreadyUnlockedPaths()
    {
        for(int i = 0; i < worldDataContainer.pathsInWorld.Count; i++)
        {
            if (worldDataContainer.pathsInWorld[i].unlocked)
            {
                if (worldDataContainer.pathsInWorld[i].firstTime == true)
                {
                    PathDecorator pathDecor = pathsInWorld[i].GetComponent<PathDecorator>();
                    pathDecor.firstTime = worldDataContainer.pathsInWorld[i].firstTime;
                    pathsInWorld[i].unlocked = worldDataContainer.pathsInWorld[i].unlocked;
                    pathDecor.StartCoroutine(pathDecor.DecoratePath());
                }
                else
                {
                    pathsInWorld[i].unlocked = worldDataContainer.pathsInWorld[i].unlocked;
                    pathsInWorld[i].GetComponent<PathDecorator>().firstTime = worldDataContainer.pathsInWorld[i].firstTime;
                }
            }
        }
        CheckFirstTimeUnlocked();
    }
    private bool CheckFirstTimeUnlocked()
    {
        for (int i = 0; i < pathsInWorld.Count; i++)
        {
            if (pathsInWorld[i].unlocked)
            {
                if (pathsInWorld[i].GetComponent<PathDecorator>().firstTime)
                {
                    worldDataContainer.pathsInWorld[i].unlocked = true;
                    return false;
                }
            }
        }
        return true;
    }
}
