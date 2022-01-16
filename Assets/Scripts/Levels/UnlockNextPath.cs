using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockNextPath : MonoBehaviour
{
    [SerializeField]
    private PathData pathData;
    private void Start()
    {
        StartCoroutine(ReturnToWorldmap());
    }

    IEnumerator ReturnToWorldmap()
    {
        GameManager.sceneManagerInstance.LevelComplete(pathData);
        yield return new WaitForSeconds(5);
        GameManager.sceneManagerInstance.SceneTransition(true, null);
        StopAllCoroutines();
    }
}
