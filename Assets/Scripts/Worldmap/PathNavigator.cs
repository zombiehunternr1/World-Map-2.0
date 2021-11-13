using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PathNavigator : MonoBehaviour
{
    #region Variables
    //Navigation
    public PathLayout CurrentPath;
    public float Duration;
    public float Progress;
    public float PositioningSpeed;
    public bool IsMoving;

    private Vector3 CurrentPosition;
    public LevelNodeData CurrentLevel;

    //Player input
    public Vector2 DirectionInput;
    public bool Confirm;
    #endregion

    private void Start()
    {
        PositionPlayerOnCurve();
    }

    #region PlayerMovement
    private void PositionPlayerOnCurve()
    {
        if (CurrentPath != null)
        {
            CurrentPosition = CurrentPath.GetPoint(Progress);
            transform.localPosition = CurrentPosition;
        }
    }

    IEnumerator MovePlayer()
    {
        if (Progress < 1)
        {
            while (Progress < 1f)
            {
                Progress += Time.deltaTime / Duration;
                if (Progress > 1f)
                {
                    Progress = 1f;
                }
                CurrentPosition = CurrentPath.GetPoint(Progress);
                transform.localPosition = CurrentPosition;
                transform.LookAt(CurrentPosition + CurrentPath.GetDirection(Progress));
                yield return Progress;
            }
        }
        else
        {
            while (Progress > 0)
            {
                Progress -= Time.deltaTime / Duration;
                if (Progress < 0)
                {
                    Progress = 0;
                }
                CurrentPosition = CurrentPath.GetPoint(Progress);
                transform.localPosition = CurrentPosition;
                transform.LookAt(CurrentPosition - CurrentPath.GetDirection(Progress));
                yield return Progress;
            }
        }
    }

    private IEnumerator PositionPlayerOnLevel(Transform LevelPosition)
    {
        while (transform.localPosition != LevelPosition.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, LevelPosition.position, Time.deltaTime * PositioningSpeed);
            yield return null;
        }
        while (transform.localRotation != Quaternion.Euler(0, 180, 0))
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * PositioningSpeed);
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
        CurrentLevel = other.GetComponent<LevelNodeData>();
        IsMoving = false;
        Progress = 0;
        Debug.Log(CurrentLevel.LevelInfo.LevelName);
        Debug.Log(CurrentLevel.LevelInfo.LevelNumber);
    }

    private void CheckDirection(Vector2 SelectedDirection)
    {
        //Up
        if (SelectedDirection.y == 1)
        {
            foreach (var i in CurrentLevel.AvailableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.UP)
                {
                    IsMoving = true;
                    int DirectionValue = (int)LevelNodeData.Direction.UP;
                    if (CurrentLevel.IsPreviousPath.Count != 0)
                    {
                        foreach (var j in CurrentLevel.IsPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                Progress = 1;
                                StartCoroutine(MovePlayer());
                            }
                        }
                    }
                    else
                    {
                        StartCoroutine(MovePlayer());
                    }
                }
            }
        }
        //Down
        if (SelectedDirection.y == -1)
        {
            foreach (LevelNodeData.Direction i in CurrentLevel.AvailableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.DOWN)
                {
                    IsMoving = true;
                    int DirectionValue = (int)LevelNodeData.Direction.DOWN;
                    if(CurrentLevel.IsPreviousPath.Count != 0)
                    {
                        foreach (var j in CurrentLevel.IsPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                Progress = 1;
                                StartCoroutine(MovePlayer());
                            }
                        }
                    }
                    else
                    {
                        StartCoroutine(MovePlayer());
                    }
                }
            }
        }
        //Left
        if (SelectedDirection.x == -1)
        {
            foreach (var i in CurrentLevel.AvailableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.LEFT)
                {
                    IsMoving = true;
                    int DirectionValue = (int)LevelNodeData.Direction.LEFT;
                    if (CurrentLevel.IsPreviousPath.Count != 0)
                    {
                        foreach (var j in CurrentLevel.IsPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                Progress = 1;
                                StartCoroutine(MovePlayer());
                            }
                        }
                    }
                    else
                    {
                        StartCoroutine(MovePlayer());
                    }
                }
            }
        }
        //Right
        if (SelectedDirection.x == 1)
        {
            foreach (var i in CurrentLevel.AvailableConnectedPaths)
            {
                if (i == LevelNodeData.Direction.RIGHT)
                {
                    IsMoving = true;
                    int DirectionValue = (int)LevelNodeData.Direction.RIGHT;
                    if (CurrentLevel.IsPreviousPath.Count != 0)
                    {
                        foreach (var j in CurrentLevel.IsPreviousPath)
                        {
                            if (((int)j) == DirectionValue)
                            {
                                Progress = 1;
                                StartCoroutine(MovePlayer());
                            }
                        }
                    }
                    else
                    {
                        StartCoroutine(MovePlayer());
                    }
                }
            }
        }
    }

    #region Inputsystem
    public void OnDirection(InputAction.CallbackContext Context)
    {
        DirectionInput = Context.ReadValue<Vector2>();
        if (!IsMoving)
        {
            CheckDirection(DirectionInput);
        }
    }

    public void OnConfirm(InputAction.CallbackContext Context)
    {
        Confirm = Context.ReadValueAsButton();
    }
    #endregion
}