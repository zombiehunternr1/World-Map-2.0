using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashPathDecorator : MonoBehaviour
{
    [SerializeField]
    private float waitDisplayNextPathDecoration;
    private float spacing = 1;
    [SerializeField]
    private int frequency;
    [SerializeField]
    private int skipAmount;
    private int waitBeforePathDecorating = 3;
    [SerializeField]
    private Transform pathDecorTransform;
    [SerializeField]
    private LevelNodeData levelToUnlock;
    private PathLayout pathToDecorate;
    private PathManager pathManager;

    public bool firstTime
    {
        get;
        set;
    }

    private void OnEnable()
    {
        pathToDecorate = GetComponent<PathLayout>();
        pathManager = GetComponentInParent<PathManager>();
        levelToUnlock.levelNodeMat.color = Color.red;
    }

    public IEnumerator DecoratePath()
    {
        if (frequency <= 0 || pathDecorTransform == null)
        {
            yield return null;
        }
        float stepSize = spacing / (frequency * spacing);
        if (firstTime)
        {
            yield return new WaitForSeconds(waitBeforePathDecorating);
        }
        for (int p = 0, f = 0; f < frequency; f++, p++)
        {
            if (p < skipAmount)
            {
                yield return null;
            }
            else if (p == frequency - 1)
            {
                PlacePathDecoration(p, stepSize);
                if (firstTime)
                {
                    yield return new WaitForSeconds(waitDisplayNextPathDecoration);
                    levelToUnlock.ChangeLevelColorToUnlocked();
                    firstTime = false;
                    yield return new WaitForSeconds(waitDisplayNextPathDecoration);
                    pathManager.UpdateFirstTime(firstTime, pathToDecorate.pathInfo);
                }
                else
                {
                    levelToUnlock.ChangeLevelColorToUnlocked();
                }
                StopAllCoroutines();
            }
            else
            {
                PlacePathDecoration(p, stepSize);
            }
            if (firstTime)
            {
                yield return new WaitForSeconds(waitDisplayNextPathDecoration);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void PlacePathDecoration(float pathPoint, float stepSize)
    {
        Transform pathDecorationItem = Instantiate(pathDecorTransform) as Transform;
        Vector3 position = pathToDecorate.GetPoint(pathPoint * stepSize);
        pathDecorationItem.transform.localPosition = position;
        pathDecorationItem.transform.LookAt(position + pathToDecorate.GetDirection(pathPoint * stepSize));
        pathDecorationItem.transform.parent = gameObject.transform;
    }
}
