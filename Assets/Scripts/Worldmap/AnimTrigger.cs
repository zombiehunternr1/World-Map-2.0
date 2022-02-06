using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public enum animationOptions { Jump, ClimbingUp, climbingDown};
    public animationOptions animOptions;

    private void OnTriggerEnter(Collider other)
    {
        PathNavigator pathNavigator = other.GetComponent<PathNavigator>();
        switch (animOptions)
        {
            case animationOptions.Jump:
                pathNavigator.playerAnimator.Play("Jump");
                break;
            case animationOptions.ClimbingUp:
                if (pathNavigator.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ClimbingLadderDown") && !pathNavigator.isAnimOnLocation)
                {
                    pathNavigator.isVertical = false;
                    pathNavigator.isAnimOnLocation = true;
                    pathNavigator.playerAnimator.Play("ClimbDownDone");
                }
                else
                {
                    pathNavigator.isRunningAnim = false;
                    pathNavigator.isAnimOnLocation = true;
                    pathNavigator.isVertical = true;
                    pathNavigator.playerAnimator.Play("ClimbUpLadder");
                }
                break;
            case animationOptions.climbingDown:
                if (pathNavigator.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ClimbingLadderUp") && !pathNavigator.isAnimOnLocation)
                {
                    pathNavigator.isVertical = false;
                    pathNavigator.isAnimOnLocation = true;
                    pathNavigator.playerAnimator.Play("ClimbUpDone");
                }
                else
                {
                    pathNavigator.isRunningAnim = false;
                    pathNavigator.isAnimOnLocation = true;
                    pathNavigator.isVertical = true;
                    pathNavigator.playerAnimator.Play("ClimbDownLadder");
                }
                break;
        }

    }
}
