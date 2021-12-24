using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PathNavigator : MonoBehaviour
{
    #region Variables
    //Navigation
    public PathLayout currentPath;
    public float duration;
    public float progress;
    public float positioningSpeed;
    public bool isMoving;

    private Vector3 currentPosition;
    public LevelNodeData currentLevel;

    //Player input
    public Vector2 directionInput;
    public bool confirm;
    #endregion

    private void Start()
    {
        PositionPlayerOnCurve();
    }

    #region PlayerMovement
    private void PositionPlayerOnCurve()
    {
        if (currentPath != null)
        {
            currentPosition = currentPath.GetPoint(progress);
            transform.localPosition = currentPosition;
        }
    }

    private void CheckDirection(Vector2 SelectedDirection)
    {
        //Up
        if (SelectedDirection.y == 1)
        {
            foreach (var i in currentLevel.availableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.UP)
                {
                    isMoving = true;
                    int Index = currentLevel.availableConnectedPaths.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.UP;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (var j in currentLevel.isPreviousPath)
                            {
                                if (((int)j) == DirectionValue)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    StartCoroutine(MovePlayer());
                                }
                            }
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                    if (currentLevel.isPreviousPath.Count != 0)
                    {
                        foreach (var j in currentLevel.isPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                if (currentPath.unlocked)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    isMoving = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentPath.unlocked)
                        {
                            StartCoroutine(MovePlayer());
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                }
            }
        }
        //Down
        if (SelectedDirection.y == -1)
        {
            foreach (LevelNodeData.Direction i in currentLevel.availableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.DOWN)
                {
                    isMoving = true;
                    int Index = currentLevel.availableConnectedPaths.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.DOWN;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (var j in currentLevel.isPreviousPath)
                            {
                                if (((int)j) == DirectionValue)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    StartCoroutine(MovePlayer());
                                }
                            }
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                    if (currentLevel.isPreviousPath.Count != 0)
                    {
                        foreach (var j in currentLevel.isPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                if (currentPath.unlocked)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    isMoving = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentPath.unlocked)
                        {
                            StartCoroutine(MovePlayer());
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                }
            }
        }
        //Left
        if (SelectedDirection.x == -1)
        {
            foreach (var i in currentLevel.availableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.LEFT)
                {
                    isMoving = true;
                    int Index = currentLevel.availableConnectedPaths.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.LEFT;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (var j in currentLevel.isPreviousPath)
                            {
                                if (((int)j) == DirectionValue)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    StartCoroutine(MovePlayer());
                                }
                            }
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                    if (currentLevel.isPreviousPath.Count != 0)
                    {
                        foreach (var j in currentLevel.isPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                if (currentPath.unlocked)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    isMoving = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentPath.unlocked)
                        {
                            StartCoroutine(MovePlayer());
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                }
            }
        }
        //Right
        if (SelectedDirection.x == 1)
        {
            foreach (var i in currentLevel.availableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.RIGHT)
                {
                    isMoving = true;
                    int Index = currentLevel.availableConnectedPaths.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.RIGHT;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (var j in currentLevel.isPreviousPath)
                            {
                                if (((int)j) == DirectionValue)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    StartCoroutine(MovePlayer());
                                }
                            }
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                    if (currentLevel.isPreviousPath.Count != 0)
                    {
                        foreach (var j in currentLevel.isPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                if (currentPath.unlocked)
                                {
                                    progress = 1;
                                    StartCoroutine(MovePlayer());
                                }
                                else
                                {
                                    isMoving = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentPath.unlocked)
                        {
                            StartCoroutine(MovePlayer());
                        }
                        else
                        {
                            isMoving = false;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator MovePlayer()
    {
        if (progress < 1)
        {
            while (progress < 1f)
            {
                progress += Time.deltaTime / duration;
                if (progress > 1f)
                {
                    progress = 1f;
                }
                currentPosition = currentPath.GetPoint(progress);
                transform.localPosition = currentPosition;
                transform.LookAt(currentPosition + currentPath.GetDirection(progress));
                yield return progress;
            }
        }
        else
        {
            while (progress > 0)
            {
                progress -= Time.deltaTime / duration;
                if (progress < 0)
                {
                    progress = 0;
                }
                currentPosition = currentPath.GetPoint(progress);
                transform.localPosition = currentPosition;
                transform.LookAt(currentPosition - currentPath.GetDirection(progress));
                yield return progress;
            }
        }
    }

    private IEnumerator PositionPlayerOnLevel(Transform LevelPosition)
    {
        while (transform.localPosition != LevelPosition.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, LevelPosition.position, Time.deltaTime * positioningSpeed);
            yield return transform.localPosition;
        }
        while (transform.localRotation != Quaternion.Euler(0, 180, 0))
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * positioningSpeed);
            yield return transform.localRotation;
        }
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        StopCoroutine(PositionPlayerOnLevel(LevelPosition));
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        StartCoroutine(PositionPlayerOnLevel(other.transform));
        currentLevel = other.GetComponent<LevelNodeData>();
        isMoving = false;
        progress = 0;
        Debug.Log(currentLevel.levelInfo.levelName);
        Debug.Log(currentLevel.levelInfo.levelNumber);
    }

    #region Inputsystem
    public void OnDirection(InputAction.CallbackContext Context)
    {
        directionInput = Context.ReadValue<Vector2>();
        if (!isMoving)
        {
            CheckDirection(directionInput);
        }
    }

    public void OnConfirm(InputAction.CallbackContext Context)
    {
        confirm = Context.ReadValueAsButton();
    }
    #endregion
}
