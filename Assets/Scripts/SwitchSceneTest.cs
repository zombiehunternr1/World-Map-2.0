using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ReturnToWorldmap());
    }

    IEnumerator ReturnToWorldmap()
    {
        yield return new WaitForSeconds(5);
        GameManager.sceneManagerInstance.SceneTransition(true, null);
        StopAllCoroutines();
    }
}
