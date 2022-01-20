using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public enum animationOptions { None, Jump };
    public animationOptions animOptions;

    private void OnTriggerEnter(Collider other)
    {
        PathNavigator pathNavigator = other.GetComponent<PathNavigator>();
        if(animOptions == animationOptions.None)
        {
            return;
        }
        else if(animOptions == animationOptions.Jump)
        {
            pathNavigator.playerAnimator.Play("Jump");
        }
    }
}
