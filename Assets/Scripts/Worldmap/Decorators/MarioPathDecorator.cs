using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPathDecorator : MonoBehaviour
{
    public bool firstTime
    {
        get;
        set;
    }
    private int waitBeforePathDecorating = 3;

    [SerializeField]
    private LevelNodeData levelToUnlock;

    private Animator animatorsForPathDecor;
    private PathManager pathManager;
    private PathData pathInfo;

    private void OnEnable()
    {
        pathInfo = GetComponent<PathLayout>().pathInfo;
        pathManager = GetComponentInParent<PathManager>();
        animatorsForPathDecor = GetComponent<Animator>();
        levelToUnlock.levelNodeMat.color = Color.red;
    }

    public IEnumerator FirstTimeUnlocked()
    {
        yield return new WaitForSeconds(waitBeforePathDecorating);
        animatorsForPathDecor.Play("Unlocking");
    }

    public void AlreadyUnlocked()
    {
        levelToUnlock.levelNodeMat.color = Color.green;
        animatorsForPathDecor.Play("Unlocked");
    }

    public void DecorationComplete()
    {
        levelToUnlock.levelNodeMat.color = Color.green;
        firstTime = false;
        pathManager.UpdateFirstTime(firstTime, pathInfo);
    }
}
