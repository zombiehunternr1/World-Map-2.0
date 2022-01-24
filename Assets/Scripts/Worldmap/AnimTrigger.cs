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
                if (pathNavigator.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Climbing") && !pathNavigator.isAnimOnLocation)
                {
                    pathNavigator.playerAnimator.Play("ClimbUpDone");
                }
                else
                {
                    pathNavigator.isRunningAnim = false;
                    pathNavigator.isAnimOnLocation = true;
                    //pathNavigator.positioningSpeedOnPath = 0.01f;
                    pathNavigator.playerAnimator.Play("Climbing");
                }
                break;
            case animationOptions.climbingDown:
                if (pathNavigator.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Climbing") && !pathNavigator.isAnimOnLocation)
                {
                    pathNavigator.playerAnimator.Play("ClimbDownDone");
                }
                else
                {
                    pathNavigator.isRunningAnim = false;
                    pathNavigator.isAnimOnLocation = true;
                    //pathNavigator.playerAnimator.Play("ClimbDown");
                }
                break;
        }

    }
}
