using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public enum animationOptions { Jump };
    public animationOptions animOptions;

    private void OnTriggerEnter(Collider other)
    {
        PathNavigator pathNavigator = other.GetComponent<PathNavigator>();
        if(animOptions == animationOptions.Jump)
        {
            pathNavigator.playerAnimator.Play("Jump");
        }
    }
}
