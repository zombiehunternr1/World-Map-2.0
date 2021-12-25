using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDecoration : MonoBehaviour
{
    public int waitBeforePathDecorating = 3;
    public float waitDisplayNextPathDecoration;
    public int frequency;
    public int skipAmount;
    public float spacing = 1;
    public Transform pathDecorTransform;
    //[HideInInspector]
    public bool firstTime = true;
    private PathLayout pathToDecorate;
    [SerializeField]
    private LevelNodeData levelToUnlock;

    private void Start()
    {
        pathToDecorate = GetComponent<PathLayout>();
        if (pathToDecorate.unlocked)
        {
            StartCoroutine(DecoratePath());
        }
    }

    private IEnumerator DecoratePath()
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
                else if (p == frequency)
                {
                    Debug.Log("Last one");
                    if (firstTime)
                    {
                        PathNavigator.canMove = true;
                        firstTime = false;
                        levelToUnlock.levelNodeMat.color = Color.green;
                    }
                    StopAllCoroutines();
                }
                else
                {
                    Transform PathDecorationItem = Instantiate(pathDecorTransform) as Transform;
                    Vector3 Position = pathToDecorate.GetPoint(p * stepSize);
                    PathDecorationItem.transform.localPosition = Position;
                    PathDecorationItem.transform.LookAt(Position + pathToDecorate.GetDirection(p * stepSize));
                    PathDecorationItem.transform.parent = gameObject.transform;
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
}
