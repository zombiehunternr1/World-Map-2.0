using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]
    private List<PathLayout> pathsInWorld;
    [SerializeField]
    private PathNavigator pathNavigator;
    private WorldData worldData;

    private void Start()
    {
        worldData = GameManager.sceneManagerInstance.worldData;
        GameManager.sceneManagerInstance.SetPathManager(this);
        GetAllPaths();
    }
    public void CheckUnlockingMovement()
    {
        if (pathsInWorld.Count == 0)
        {
            GetAllPaths();
        }
        pathNavigator.canMove = CheckFirstTimeUnlocked();
    }

    public void UpdateUnlockedStatus(bool firstTimeUnlocked, int pathIndex)
    {
        worldData.pathsInWorld[pathIndex].unlocked = true;
        worldData.pathsInWorld[pathIndex].firstTime = firstTimeUnlocked;
    }

    public void UpdateFirstTime(bool firstTimeUnlocked, PathData path)
    {
        for(int i = 0; i < worldData.pathsInWorld.Count; i++)
        {
            if(worldData.pathsInWorld[i] == path)
            {
                worldData.pathsInWorld[i].firstTime = firstTimeUnlocked;
                CheckUnlockingMovement();
            }
        }
    }

    private void GetAllPaths()
    {
        PathLayout[] allPaths = GetComponentsInChildren<PathLayout>();
        pathsInWorld = new List<PathLayout>();

        foreach (PathLayout path in allPaths)
        {
            pathsInWorld.Add(path);
        }
        SetFirstTime();
    }
    private void SetFirstTime()
    {
        if(pathsInWorld.Count != 0)
        {
            for (int i = 0; i < pathsInWorld.Count; i++)
            {
                for (int j = 0; j < worldData.pathsInWorld.Count; j++)
                {
                    if (pathsInWorld[i].pathInfo == worldData.pathsInWorld[j])
                    {
                        int index = worldData.pathsInWorld.IndexOf(pathsInWorld[i].pathInfo);
                        pathsInWorld[i].GetComponent<PathDecorator>().firstTime = worldData.pathsInWorld[index].firstTime;
                    }
                }
            }
        }
        UpdatePathList();
    }

    private void UpdatePathList()
    {
        for (int i = 0; i < pathsInWorld.Count; i++)
        {
            for(int j = 0; j < worldData.pathsInWorld.Count; j++)
            {
                if(pathsInWorld[i].pathInfo == worldData.pathsInWorld[j])
                {
                    if (worldData.pathsInWorld[j].unlocked)
                    {
                        int index = worldData.pathsInWorld.IndexOf(pathsInWorld[i].pathInfo);
                        pathsInWorld[i].unlocked = worldData.pathsInWorld[index].unlocked;
                    }
                }
            }
        }
       CheckAlreadyUnlockedPaths();
    }

    private void CheckAlreadyUnlockedPaths()
    {
        for(int i = 0; i < pathsInWorld.Count; i++)
        {
            for (int j = 0; j < worldData.pathsInWorld.Count; j++)
            {
                if(pathsInWorld[i].pathInfo == worldData.pathsInWorld[j])
                {
                    if (worldData.pathsInWorld[j].unlocked)
                    {
                        int worldDataIndex = worldData.pathsInWorld.IndexOf(pathsInWorld[i].pathInfo);
                        PathDecorator pathDecor = pathsInWorld[i].GetComponent<PathDecorator>();
                        if (pathsInWorld[i].GetComponent<PathDecorator>().firstTime == true)
                        {
                            pathDecor.firstTime = worldData.pathsInWorld[worldDataIndex].firstTime;
                            pathsInWorld[i].unlocked = worldData.pathsInWorld[worldDataIndex].unlocked;
                            pathDecor.StartCoroutine(pathDecor.DecoratePath());
                        }
                        else
                        {
                            pathsInWorld[i].unlocked = worldData.pathsInWorld[worldDataIndex].unlocked;
                            pathsInWorld[i].GetComponent<PathDecorator>().firstTime = worldData.pathsInWorld[worldDataIndex].firstTime;
                            pathDecor.StartCoroutine(pathDecor.DecoratePath());
                        }
                    }
                }             
            }
        }   
    }
    private bool CheckFirstTimeUnlocked()
    {
        for (int i = 0; i < pathsInWorld.Count; i++)
        {
            if (pathsInWorld[i].unlocked)
            {
                if (pathsInWorld[i].GetComponent<PathDecorator>().firstTime)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
