using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PathNavigator : MonoBehaviour
{
    #region Variables
    //Speed variables
    [SerializeField]
    private float DefaultPathSpeed;
    [SerializeField]
    private float positioningSpeedOnLevelNode;

    //Reference and positioning variables
    public bool canMove
    {
        get;
        set;
    }

    public bool isAnimOnLocation
    {
        get;
        set;
    }

    public bool isRunningAnim
    {
        get;
        set;
    }

    public float positioningSpeedOnPath
    {
        get;
        set;
    }

    public Animator playerAnimator; //Not needed to implement until after cube tutorial explanation is done
    [SerializeField]
    private PathLayout currentPath;
    [SerializeField]
    private LevelNodeData currentLevel;
    [SerializeField]
    private WorldData worldMapLevel;
    private Vector3 currentPathPosition;
    private Vector3 currentLevelPosition;
    [SerializeField]
    private float progress;

    //Player input
    private Vector2 directionInput;
    #endregion

    private void Start()
    {
        positioningSpeedOnPath = DefaultPathSpeed;
        isRunningAnim = true;
        if(worldMapLevel.currentLevel == null)
        {
            transform.position = currentLevel.transform.position;
            currentLevelPosition = currentLevel.transform.position;
        }
        else
        {
            currentLevelPosition = worldMapLevel.currentLevel.position;
            transform.position = currentLevelPosition;
        }
    }

    private void ContinueClimbing()
    {
        Debug.Log("Hallo");
        isAnimOnLocation = false;
    }

    private void PerformedAnimation()
    {
        isRunningAnim = true;
    }

    #region PlayerMovement

    private void CheckDirection(Vector2 SelectedDirection)
    {
        //Up
        if (SelectedDirection.y == 1)
        {
            foreach (LevelNodeData.Direction i in currentLevel.allAvailableDirectionOptions)
            {
                if (i == LevelNodeData.Direction.UP)
                {
                    canMove = false;
                    int Index = currentLevel.allAvailableDirectionOptions.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.UP;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
                    if (currentLevel.allPreviousDirectionOptions.Count != 0)
                    {
                        foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
        else if (SelectedDirection.y == -1)
        {
            foreach (LevelNodeData.Direction i in currentLevel.allAvailableDirectionOptions)
            {
                if (i == LevelNodeData.Direction.DOWN)
                {
                    canMove = false;
                    int Index = currentLevel.allAvailableDirectionOptions.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.DOWN;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
                    if (currentLevel.allPreviousDirectionOptions.Count != 0)
                    {
                        foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
        else if (SelectedDirection.x == -1)
        {
            foreach (LevelNodeData.Direction i in currentLevel.allAvailableDirectionOptions)
            {
                if (i == LevelNodeData.Direction.LEFT)
                {
                    canMove = false;
                    int Index = currentLevel.allAvailableDirectionOptions.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.LEFT;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
                    if (currentLevel.allPreviousDirectionOptions.Count != 0)
                    {
                        foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
        else if (SelectedDirection.x == 1)
        {
            foreach (LevelNodeData.Direction i in currentLevel.allAvailableDirectionOptions)
            {
                if (i == LevelNodeData.Direction.RIGHT)
                {
                    canMove = false;
                    int Index = currentLevel.allAvailableDirectionOptions.IndexOf(i);
                    int DirectionValue = (int)LevelNodeData.Direction.RIGHT;
                    if (currentPath != currentLevel.connectedPaths[Index])
                    {
                        currentPath = currentLevel.connectedPaths[Index];
                        if (currentPath.unlocked)
                        {
                            foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
                    if (currentLevel.allPreviousDirectionOptions.Count != 0)
                    {
                        foreach (LevelNodeData.Direction j in currentLevel.allPreviousDirectionOptions)
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
        if (progress < 1)
        {
            while (progress < 1f)
            {
                if (!isAnimOnLocation)
                {
                    if (isRunningAnim)
                    {
                        playerAnimator.Play("Run");
                    }
                    progress += positioningSpeedOnPath * Time.deltaTime;
                    if (progress > 1f)
                    {
                        progress = 1f;
                    }
                    currentPathPosition = currentPath.GetPoint(progress);
                    transform.localPosition = currentPathPosition;
                    transform.LookAt(currentPathPosition + currentPath.GetDirection(progress));
                }
                yield return progress;
            }
        }
        else
        {
            while (progress > 0)
            {
                progress -= positioningSpeedOnPath * Time.deltaTime;
                if (progress < 0)
                {
                    progress = 0;
                }
                currentPathPosition = currentPath.GetPoint(progress);
                transform.localPosition = currentPathPosition;
                if (!isAnimOnLocation)
                {
                    transform.LookAt(currentPathPosition - currentPath.GetDirection(progress));
                }
                yield return progress;
            }
        }
    }

    private IEnumerator PositionPlayerOnLevel(Transform levelPosition)
    {
        while (transform.localPosition != levelPosition.position)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, levelPosition.position, Time.deltaTime * positioningSpeedOnLevelNode);
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
        //playerAnimator.Play("Idle");
        Debug.Log("Hallo");
        yield return new WaitForSeconds(0.5f);
        progress = 0;
        canMove = true;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LevelNodeData>())
        {
            StopAllCoroutines();
            StartCoroutine(PositionPlayerOnLevel(other.transform));
            currentLevel = other.GetComponent<LevelNodeData>();
            GameManager.sceneManagerInstance.SetLevelUIDataDisplay(currentLevel);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LevelNodeData>())
        {
            GameManager.sceneManagerInstance.ToggleEnterLevelInfo(false);
            GameManager.sceneManagerInstance.SetLevelUIDataDisplay(null);
        }
    }

    #region Inputsystem
    public void OnDirection(InputAction.CallbackContext Context)
    {
        directionInput = Context.ReadValue<Vector2>();
        if (canMove)
        {
            CheckDirection(directionInput);
        }
    }

    public void OnConfirm()
    {
        if (canMove)
        {
            canMove = false;
            GameManager.sceneManagerInstance.SceneTransition(true, currentLevel);
            worldMapLevel.currentLevel = currentLevel.levelData;
            GameManager.sceneManagerInstance.SetLevelUIDataEnter(currentLevel);
            GameManager.sceneManagerInstance.ToggleEnterLevelInfo(true);
            playerAnimator.Play("Enter");
        }
    }
    #endregion
}
