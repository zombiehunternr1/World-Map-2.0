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
        StartCoroutine(PositionPlayerOnLevel());
    }

    private void PositionPlayerOnCurve()
    {
        if (CurrentPath != null)
        {
            CurrentPosition = CurrentPath.GetPoint(Progress);
            transform.position = CurrentPosition;
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
            transform.position = CurrentPosition;
            transform.LookAt(CurrentPosition + CurrentPath.GetPoint(Progress));
            yield return Progress;
        }
    }

    private IEnumerator PositionPlayerOnLevel()
    {
        while (transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * PositioningSpeed);
            yield return transform.rotation;
        }
        transform.rotation = Quaternion.identity;
        StopCoroutine(PositionPlayerOnLevel());
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
