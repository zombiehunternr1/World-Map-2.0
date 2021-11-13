using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathNavigator : MonoBehaviour
{
    #region Variables
    //Navigation
    public PathLayout CurrentPath;
    public float Duration;
    public float Progress;
    public float PositioningSpeed;
    public bool GoingForward;

    private Vector3 CurrentPosition;

    //Player input
    public Vector2 DirectionInput;
    public bool Confirm;
    #endregion

    private void Start()
    {
        PositionPlayerOnCurve();
    }

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
        while(Progress < 1f)
        {
            Progress += Time.deltaTime / Duration;
            if(Progress > 1f)
            {
                Progress = 1f;
            }
            CurrentPosition = CurrentPath.GetPoint(Progress);
            transform.localPosition = CurrentPosition;
            transform.LookAt(CurrentPosition + CurrentPath.GetDirection(Progress));
            yield return Progress;
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

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(PositionPlayerOnLevel(other.transform));
        Debug.Log(other.GetComponent<LevelNodeData>().LevelInfo.LevelName);
        Debug.Log(other.GetComponent<LevelNodeData>().LevelInfo.LevelNumber);
    }

    #region Inputsystem
    public void OnDirection(InputAction.CallbackContext Context)
    {
        DirectionInput = Context.ReadValue<Vector2>();
        StartCoroutine(MovePlayer());
    }

    public void OnConfirm(InputAction.CallbackContext Context)
    {
        Confirm = Context.ReadValueAsButton();
    }
    #endregion
}
