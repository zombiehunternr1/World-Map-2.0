using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDecoration : MonoBehaviour
{
    [HideInInspector]
    public bool firstTime = true;
    [SerializeField]
    private int waitBeforePathDecorating = 3;
    [SerializeField]
    private float waitDisplayNextPathDecoration;
    [SerializeField]
    private int frequency;
    [SerializeField]
    private int skipAmount;
    [SerializeField]
    private float spacing = 1;
    [SerializeField]
    private Transform pathDecorTransform;
    [SerializeField]
    private LevelNodeData levelToUnlock;
    private PathLayout pathToDecorate;
    private PathManager pathManager;
    private void Start()
    {
        levelToUnlock.levelNodeMat.color = Color.red;
        pathToDecorate = GetComponent<PathLayout>();
        pathManager = GetComponentInParent<PathManager>();
        if (pathToDecorate.unlocked)
        {
            StartCoroutine(DecoratePath());
        }
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
        for (int p = 0, f = 0; f < frequency; f++)
        {
            for (int i = 0; i < spacing; i++, p++)
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
                        levelToUnlock.levelNodeMat.color = Color.green;
                        firstTime = false;
                        yield return new WaitForSeconds(waitDisplayNextPathDecoration);
                        pathManager.UpdateUnlockedStatus(pathToDecorate.pathInfo, firstTime);
                    }
                    else
                    {
                        levelToUnlock.levelNodeMat.color = Color.green;
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
