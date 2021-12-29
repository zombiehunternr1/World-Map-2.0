using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class PathNavigator : MonoBehaviour
{
    #region Variables
    //Speed variables
    [SerializeField]
    private float positioningSpeedOnPath;
    [SerializeField]
    private float positioningSpeedOnLevelNode;

    //Reference and positioning variables
    public static bool canMove;
    [SerializeField]
    private PathLayout currentPath;
    [SerializeField]
    private Animator playerAnimator; //Not needed to implement until after cube tutorial explanation is done
    [SerializeField]
    private float enableMovementCooldown;
    private Vector3 currentPosition;
    private LevelNodeData currentLevel;
    private float progress;

    //UI references
    [SerializeField]
    private TextMeshProUGUI levelNumber;
    [SerializeField]
    private TextMeshProUGUI levelName;
    [SerializeField]
    private TextMeshProUGUI levelEnterInfo;

    //Player input
    private Vector2 directionInput;
    #endregion

    private void Start()
    {
        PositionPlayerOnCurve();
    }

    #region PlayerMovement
    private void PositionPlayerOnCurve()
    {
        currentPosition = currentPath.GetPoint(progress);
        transform.localPosition = currentPosition;
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
                    canMove = false;
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
                            canMove = true;
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
                                    canMove = true;
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
                            canMove = true;
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
                    canMove = false;
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
                            canMove = true;
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
                                    canMove = true;
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
                            canMove = true;
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
                    canMove = false;
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
                            canMove = true;
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
                                    canMove = true;
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
                            canMove = true;
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
                    canMove = false;
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
                            canMove = false;
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
                                    canMove = true;
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
                            canMove = true;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator MovePlayer()
    {
        playerAnimator.Play("Run"); //Not needed to implement until after cube tutorial explanation is done
        if (progress < 1)
        {
            while (progress < 1f)
            {
                progress += Time.deltaTime / positioningSpeedOnPath;
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
                progress -= Time.deltaTime / positioningSpeedOnPath;
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
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, LevelPosition.position, Time.deltaTime * positioningSpeedOnLevelNode);
            yield return transform.localPosition;
        }
        if(transform.localRotation.y >= 0)
        {
            while (transform.localRotation != Quaternion.Euler(0, 180, 0))
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * positioningSpeedOnLevelNode);
                yield return transform.localRotation;
            }
        }
        if(transform.localRotation.y <= 0)
        {
            while (transform.localRotation != Quaternion.Euler(0, -180, 0))
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * positioningSpeedOnLevelNode);
                yield return transform.localRotation;
            }
        }
        playerAnimator.Play("Idle"); //Not needed to implement until after cube tutorial explanation is done
        yield return new WaitForSeconds(0.5f);
        progress = 0;
        canMove = true;
        StopCoroutine(PositionPlayerOnLevel(LevelPosition));
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        StartCoroutine(PositionPlayerOnLevel(other.transform));
        currentLevel = other.GetComponent<LevelNodeData>();
        levelNumber.text = "Level: " + currentLevel.levelInfo.levelNumber;
        levelName.text = currentLevel.levelInfo.levelName;
    }

    private void OnTriggerExit()
    {
        levelNumber.text = "Level: ";
        levelName.text = "";
        SceneManager.Instance.ToggleEnterLevelInfo(false);
    }

    #region Inputsystem
    public void OnDirection(InputAction.CallbackContext Context)
    {
        directionInput = Context.ReadValue<Vector2>();
        if (canMove && SceneManager.Instance.allowInteraction)
        {
            CheckDirection(directionInput);
        }
    }

    public void OnConfirm()
    {
        if (canMove && SceneManager.Instance.allowInteraction)
        {
            canMove = false;
            SceneManager.Instance.SceneTransition(true);
            if (transform.localRotation.y > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if(transform.localRotation.y < 0)
            {
                transform.localRotation = Quaternion.Euler(0, -180, 0);
            }
            levelEnterInfo.text = "Now entering level " + currentLevel.levelInfo.levelNumber;
            SceneManager.Instance.ToggleEnterLevelInfo(true);
            playerAnimator.Play("Enter");
        }
    }
    #endregion
}
