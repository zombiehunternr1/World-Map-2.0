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
    public bool canMove
    {
        get;
        set;
    }
    [SerializeField]
    private PathLayout currentPath;
    [SerializeField]
    private Animator playerAnimator; //Not needed to implement until after cube tutorial explanation is done
    [SerializeField]
    private float enableMovementCooldown;
    [SerializeField]
    private LevelNodeData currentLevel;
    [SerializeField]
    private WorldData worldMapLevel;
    private Vector3 currentPathPosition;
    private Vector3 currentLevelPosition;
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
        //PositionPlayerOnCurve(); //Remove later in the tutorial once level node data SO's has been implemented
    }

    #region PlayerMovement
    //Remove later in the turial once level node data SO's has been implemented
    /*private void PositionPlayerOnCurve()
     {
         currentPosition = currentPath.GetPoint(progress);
         transform.localPosition = currentPosition;
     }*/

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
                currentPathPosition = currentPath.GetPoint(progress);
                transform.localPosition = currentPathPosition;
                transform.LookAt(currentPathPosition + currentPath.GetDirection(progress));
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
                currentPathPosition = currentPath.GetPoint(progress);
                transform.localPosition = currentPathPosition;
                transform.LookAt(currentPathPosition - currentPath.GetDirection(progress));
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
        playerAnimator.Play("Idle"); //Not needed to implement until after cube tutorial explanation is done
        yield return new WaitForSeconds(0.5f);
        progress = 0;
        canMove = true;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        StartCoroutine(PositionPlayerOnLevel(other.transform));
        currentLevel = other.GetComponent<LevelNodeData>();
        levelNumber.text = "Level: " + currentLevel.levelData.levelNumber;
        levelName.text = currentLevel.levelData.levelName;
    }

    private void OnTriggerExit()
    {
        levelNumber.text = "Level: ";
        levelName.text = "";
        SceneManager.sceneManagerInstance.ToggleEnterLevelInfo(false);
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
            SceneManager.sceneManagerInstance.SceneTransition(true);
            levelEnterInfo.text = "Now entering level " + currentLevel.levelData.levelNumber;
            worldMapLevel.currentLevel = currentLevel.levelData;
            SceneManager.sceneManagerInstance.ToggleEnterLevelInfo(true);
            playerAnimator.Play("Enter");
        }
    }
    #endregion
}
